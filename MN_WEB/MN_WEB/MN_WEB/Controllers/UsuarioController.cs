using MN_WEB.Entities;
using MN_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MN_WEB.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioModel model = new UsuarioModel();



        [HttpGet]
        public ActionResult ConsultarUsuarios()
        {
            var datos = model.ConsultarUsuarios();
            return View(datos);
        }



        [HttpGet]
        public ActionResult Cambiar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CambiarContrasenna(UsuarioEnt entidad)
        {
            //Comprobar la actual
            entidad.IdUsuario = long.Parse(Session["IdSesion"].ToString());
            entidad.CorreoElectronico = Session["CorreoSesion"].ToString();
            var respValidar = model.IniciarSesion(entidad);

            //Comprobar la contraseña actual
            if (respValidar == null)
            {
                ViewBag.MsjPantalla = "La contraseña actual es incorrecta";
                return View("Cambiar");
            }

            //Comprobar la nueva y la confirmación
            if (entidad.ContrasennaNueva != entidad.ConfirmarContrasenna)
            {
                ViewBag.MsjPantalla = "Las contraseñas no coinciden";
                return View("Cambiar");
            }

            //Comprobar la actual sea diferente a la nueva
            if (entidad.Contrasenna == entidad.ContrasennaNueva)
            {
                ViewBag.MsjPantalla = "Debe ingresar una contraseña diferente a la actual";
                return View("Cambiar");
            }

            //Cambiar contraseña correctamente
            var respActualizar = model.CambiarContrasenna(entidad);

            if (respActualizar > 0)
                return RedirectToAction("Principal", "Home");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar su contraseña";
                return View("Cambiar");
            }
        }



        [HttpGet]
        public ActionResult CambiarEstado(long q)
        {
            UsuarioEnt entidad = new UsuarioEnt();
            entidad.IdUsuario = q;

            var resp = model.CambiarEstado(entidad);

            if (resp > 0)
                return RedirectToAction("ConsultarUsuarios", "Usuario");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar el estado";
                return View("ConsultarUsuarios");
            }
        }



        [HttpGet]
        public ActionResult Editar(long q)
        {
            var datos = model.ConsultarUsuario(q);
            var roles = model.ConsultarRoles();

            var listaRoles = new List<SelectListItem>();
            foreach (var item in roles)
            {
                listaRoles.Add(new SelectListItem { Text = item.NombreRol, Value = item.IdRol.ToString() });
            }

            ViewBag.ListaRoles = listaRoles;
            return View(datos);
        }

        [HttpPost]
        public ActionResult EditarUsuario(UsuarioEnt entidad)
        {
            var resp = model.EditarUsuario(entidad);

            if (resp > 0)
                return RedirectToAction("ConsultarUsuarios", "Usuario");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar la información del usuario";
                return View("Editar");
            }
        }



        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }



    }
}