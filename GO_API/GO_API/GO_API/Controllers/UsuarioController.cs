using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web.Http;
using MN_API.Entities;
using MN_API.Models;
using System.Web.Security;
using MN_API.App_Start;

namespace MN_API.Controllers
{
    [Authorize]
    public class UsuarioController : ApiController
    {
        generales model = new generales();
        TokenGenerator tok = new TokenGenerator();

        [HttpPost]
        [Route("api/IniciarSesion")]
        [AllowAnonymous]
        public UsuarioEnt IniciarSesion(UsuarioEnt entidad)
        {
            using (var bd = new GO_ProyectoEntities())
            {
                var datos = (from x in bd.Usuario
                             join r in bd.Rol on x.IdRol equals r.IdRol
                             where x.CorreoElectronico == entidad.CorreoElectronico
                             && x.Contrasenna == entidad.Contrasenna
                             && x.Estado == true
                             select new { 
                                x.UsaClaveTemporal, 
                                x.FechaCaducidad,
                                x.CorreoElectronico,
                                x.Nombre,
                                x.Identificacion,
                                x.Estado,
                                x.IdRol,
                                r.NombreRol,
                                x.IdUsuario
                             }).FirstOrDefault();

                if (datos != null)
                {
                    if (datos.UsaClaveTemporal && datos.FechaCaducidad < DateTime.Now)
                        return null;

                    UsuarioEnt resp = new UsuarioEnt();
                    resp.CorreoElectronico = datos.CorreoElectronico;
                    resp.Nombre = datos.Nombre;
                    resp.Identificacion = datos.Identificacion;
                    resp.Estado = datos.Estado;
                    resp.IdRol = datos.IdRol;
                    resp.NombreRol = datos.NombreRol;
                    resp.IdUsuario = datos.IdUsuario;
                    resp.Token = tok.GenerateTokenJwt(datos.CorreoElectronico);
                    return resp;
                }

                return null;
            }

            /*using (var bd = new GO_ProyectoEntities())
            {
                return bd.IniciarSesion(entidad.CorreoElectronico, entidad.Contrasenna).FirstOrDefault();
            }*/
        }

        [HttpPost]
        [Route("api/Registrarse")]
        [AllowAnonymous]
        public int Registrarse(UsuarioEnt entidad)
        {
            using (var bd = new GO_ProyectoEntities())
            {
                Usuario tabla = new Usuario();
                tabla.CorreoElectronico = entidad.CorreoElectronico;
                tabla.Contrasenna = entidad.Contrasenna;
                tabla.Identificacion = entidad.Identificacion;
                tabla.Nombre = entidad.Nombre;
                tabla.Estado = entidad.Estado;
                tabla.IdRol = entidad.IdRol;

                bd.Usuario.Add(tabla);
                return bd.SaveChanges();
            }

            /*using (var bd = new GO_ProyectoEntities())
            {
                return bd.Registrarse(entidad.CorreoElectronico, entidad.Contrasenna, entidad.Identificacion,
                    entidad.Nombre, entidad.Estado, entidad.IdRol);
            }*/
        }

        [HttpPost]
        [Route("api/RecuperarContrasenna")]
        [AllowAnonymous]
        public bool RecuperarContrasenna(UsuarioEnt entidad)
        {
            using (var bd = new GO_ProyectoEntities())
            {
                //el usuario es válido?
                var datos = (from x in bd.Usuario
                             where x.CorreoElectronico == entidad.CorreoElectronico
                             && x.Estado == true
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    //generar una contraseña temporal
                    string password = model.CreatePassword();

                    //actualizar la contraseña temporal en la bd
                    datos.Contrasenna = password;
                    datos.UsaClaveTemporal = true;
                    datos.FechaCaducidad = DateTime.Now.AddMinutes(30);
                    bd.SaveChanges();

                    //armar el correo
                    StringBuilder mensaje = new StringBuilder();
                    mensaje.Append("<html>");
                    mensaje.Append("<head>");
                    mensaje.Append("</head>");
                    mensaje.Append("<body style='font-family: Arial, sans-serif;'>");
                    mensaje.Append("<div style='text-align: center;'>");
                    mensaje.Append("<h2>Recuperación de Contraseña</h2>");
                    mensaje.Append("</div>");
                    mensaje.Append("<p>Estimado(a) " + datos.Nombre + ",</p>");
                    mensaje.Append("<p>Le informamos que se ha generado un código temporal para restablecer su contraseña en el sistema:</p>");
                    mensaje.Append("<p style='font-size: 18px;'><b>" + password + "</b></p>");
                    mensaje.Append("<p>Este código tiene una validez de 30 minutos. Por favor, ingrese al sistema para establecer su nueva contraseña.</p>");
                    mensaje.Append("<p>Este correo ha sido generado de manera automática por el sistema.</p>");
                    mensaje.Append("<br/>");
                    mensaje.Append("<p>Atentamente,</p>");
                    mensaje.Append("<p>Equipo de Soporte</p>");
                    mensaje.Append("<br/>");
                    mensaje.Append("<p>Puede acceder al sistema haciendo clic en el siguiente enlace:</p>");
                    mensaje.Append("<a href='https://localhost:44350/'>Sistema Game Over Web</a>");
                    mensaje.Append("</body>");
                    mensaje.Append("</html>");



                    //enviar el correo
                    model.SendEmail(datos.CorreoElectronico,"Recuperar Contraseña", mensaje.ToString());
                    return true;
                }

            }
            return false;
        }

        [HttpPut]
        [Route("api/CambiarContrasenna")]
        public int CambiarContrasenna(UsuarioEnt entidad)
        {
            using (var bd = new GO_ProyectoEntities())
            {
                var datos = (from x in bd.Usuario
                             where x.IdUsuario == entidad.IdUsuario
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    datos.Contrasenna = entidad.ContrasennaNueva;
                    datos.UsaClaveTemporal = false;
                    datos.FechaCaducidad = DateTime.Now;
                    return bd.SaveChanges();
                }
                return 0;
            }
        }

        [HttpGet]
        [Route("api/ConsultarUsuarios")]
        public List<UsuarioEnt> ConsultarUsuarios()
        {
            using (var bd = new GO_ProyectoEntities())
            {
                var datos = (from x in bd.Usuario
                             join r in bd.Rol on x.IdRol equals r.IdRol
                             select new { 
                                x.CorreoElectronico,
                                x.Nombre,
                                x.Identificacion,
                                x.Estado,
                                x.IdRol,
                                r.NombreRol,
                                x.IdUsuario
                             }).ToList();

                List<UsuarioEnt> resp = new List<UsuarioEnt>();

                if (datos.Count > 0)
                {
                    foreach (var item in datos)
                    {
                        resp.Add(new UsuarioEnt
                        {
                            CorreoElectronico = item.CorreoElectronico,
                            Nombre = item.Nombre,
                            Identificacion = item.Identificacion,
                            Estado = item.Estado,
                            IdRol = item.IdRol,
                            NombreRol = item.NombreRol,
                            IdUsuario = item.IdUsuario
                        });
                    }
                }

                return resp;
            }
        }

        [HttpGet]
        [Route("api/ConsultarUsuario")]
        public UsuarioEnt ConsultarUsuarios(long q)
        {
            using (var bd = new GO_ProyectoEntities())
            {
                var datos = (from x in bd.Usuario
                             where x.IdUsuario == q
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    UsuarioEnt resp = new UsuarioEnt();
                    resp.CorreoElectronico = datos.CorreoElectronico;
                    resp.Nombre = datos.Nombre;
                    resp.Identificacion = datos.Identificacion;
                    resp.Estado = datos.Estado;
                    resp.IdRol = datos.IdRol;
                    resp.IdUsuario = datos.IdUsuario;
                    return resp;
                }

                return null;
            }
        }

        [HttpPut]
        [Route("api/CambiarEstado")]
        public int CambiarEstado(UsuarioEnt entidad)
        {
            using (var bd = new GO_ProyectoEntities())
            {
                var datos = (from x in bd.Usuario
                             where x.IdUsuario == entidad.IdUsuario
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    var estadoActual = datos.Estado;

                    datos.Estado = (estadoActual == true ? false : true);
                    return bd.SaveChanges();
                }
                return 0;
            }
        }

        [HttpGet]
        [Route("api/ConsultarRoles")]
        public List<RolEnt> ConsultarRoles()
        {
            using (var bd = new GO_ProyectoEntities())
            {
                var datos = (from x in bd.Rol
                select x).ToList();

                List<RolEnt> resp = new List<RolEnt>();

                if (datos.Count > 0)
                {
                    foreach (var item in datos)
                    {
                        resp.Add(new RolEnt
                        {
                            IdRol = item.IdRol,
                            NombreRol = item.NombreRol
                        });
                    }
                }

                return resp;
            }
        }

        [HttpPut]
        [Route("api/EditarUsuario")]
        public int EditarUsuario(UsuarioEnt entidad)
        {
            using (var bd = new GO_ProyectoEntities())
            {
                var datos = (from x in bd.Usuario
                             where x.IdUsuario == entidad.IdUsuario
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    datos.CorreoElectronico = entidad.CorreoElectronico;
                    datos.IdRol = entidad.IdRol;
                    return bd.SaveChanges();
                }
                return 0;
            }
        }
        
    }
}
