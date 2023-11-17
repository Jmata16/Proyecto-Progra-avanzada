using ProyectoProgra.Entities;
using ProyectoProgra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoProgra.Controllers
{
    public class LoginController : Controller
    {
        UsuarioModel modelUsuario = new UsuarioModel();
        [HttpGet]
        public ActionResult RegistrarCuenta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarCuenta(UsuarioEnt entidad)
        {
            string respuesta = modelUsuario.RegistrarCuenta(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("RegistrarCuenta", "Login");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido registrar su información";
                return View();
            }
        }
    }
}