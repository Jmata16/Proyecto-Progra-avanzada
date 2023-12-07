using GO_WEB.Entities;
using GO_WEB.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GO_WEB.Controllers
{
    public class HomeController : Controller
    {
        UsuarioModel model = new UsuarioModel();
        ProductoModel modelProductos = new ProductoModel();
        CarritoModel modelCarrito = new CarritoModel();
        ProductoNoLogModel modelProductoNoLogModel = new ProductoNoLogModel();


        [HttpGet]
        public ActionResult Index()
        {
            var productos = modelProductoNoLogModel.ConsultarProductosNOLogIn();
            return View(productos);
        }

        // Productos que se ven sin Log in
        [HttpGet]
        public ActionResult ShowProductos()
        {
            var productos = modelProductoNoLogModel.ConsultarProductosNOLogIn();
            return View(productos);
        }
        [HttpGet]
        public ActionResult ShowMouse()
        {
            var productos = modelProductoNoLogModel.ConsultarProductosNOLogIn();
            return View(productos);
        }
        [HttpGet]
        public ActionResult ShowAudífonos()
        {
            var productos = modelProductoNoLogModel.ConsultarProductosNOLogIn();
            return View(productos);
        }
        [HttpGet]
        public ActionResult ShowMonitores()
        {
            var productos = modelProductoNoLogModel.ConsultarProductosNOLogIn();
            return View(productos);
        }
        [HttpGet]
        public ActionResult ShowTeclados()
        {
            var productos = modelProductoNoLogModel.ConsultarProductosNOLogIn();
            return View(productos);
        }

        // Fin Productos que se ven sin Log in

        [HttpGet]
        public ActionResult IniciarSesion()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IniciarSesion(UsuarioEnt entidad)
        {
            try
            {
                var resp = model.IniciarSesion(entidad);

                if (resp != null)
                {
                    Session["IdSesion"] = resp.IdUsuario.ToString();
                    Session["CorreoSesion"] = resp.CorreoElectronico;
                    Session["NombreSesion"] = resp.Nombre;
                    Session["RolSesion"] = resp.NombreRol;
                    Session["IdRolSesion"] = resp.IdRol;
                    Session["TokenSesion"] = resp.Token;

                    return RedirectToAction("Principal", "Home");
                }
                else
                {
                    ViewBag.MsjPantalla = "No se ha podido validar su información";
                    return View("IniciarSesion");
                }
            }
            catch (Exception ex)
            {
                ViewBag.MsjPantalla = "Consulta con el administrador del sistema: " + ex.Message;
                return View("IniciarSesion");
            }
        }




        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrarse(UsuarioEnt entidad)
        {
            entidad.IdRol = 2;
            entidad.Estado = true;

            var resp = model.Registrarse(entidad);

            if (resp > 0)
                return RedirectToAction("IniciarSesion", "Home");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido registrar su información";
                return View("Registro");
            }
        }



        [HttpGet]
        public ActionResult Recuperar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarContrasenna(UsuarioEnt entidad)
        {
            var resp = model.RecuperarContrasenna(entidad);

            if (resp)
                return RedirectToAction("Index", "Home");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido recuperar su información";
                return View("Recuperar");
            }
        }



        [HttpGet]
        public ActionResult Principal()
        {
            var datos = modelCarrito.ConsultarCarrito(long.Parse(Session["IdSesion"].ToString()));
            Session["Cantidad"] = datos.Count();
            Session["SubTotal"] = datos.Sum(x => x.Precio);
            Session["Total"] = datos.Sum(x => x.Precio) + (datos.Sum(x => x.Precio) * 0.13M);

            var Productos = modelProductos.ConsultarProductos();
            return View(Productos);
        }


    }
}