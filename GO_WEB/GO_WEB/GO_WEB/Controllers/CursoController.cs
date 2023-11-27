using MN_WEB.Entities;
using MN_WEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MN_WEB.Controllers
{
    public class ProductoController : Controller
    {
        ProductoModel model = new ProductoModel();

        [HttpGet]
        public ActionResult ConsultarMantProductos()
        {
            var datos = model.ConsultarProductos();
            return View(datos);
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AgregarProducto(ProductoEnt entidad, HttpPostedFileBase imagenProducto)
        {
            entidad.Imagen = string.Empty;
            var IdProducto = model.RegistrarProducto(entidad);

            string extensionImagen = Path.GetExtension(Path.GetFileName(imagenProducto.FileName));
            string rutaGuardarImagenes = AppDomain.CurrentDomain.BaseDirectory + "images\\" + IdProducto + extensionImagen;
            imagenProducto.SaveAs(rutaGuardarImagenes);

            entidad.IdProducto = IdProducto;
            entidad.Imagen = "\\images\\" + IdProducto + extensionImagen;
            model.ActualizarRuta(entidad);

            return RedirectToAction("ConsultarMantProductos", "Producto");
        }

        [HttpGet]
        public ActionResult Editar(long q)
        {
            var datos = model.ConsultarProducto(q);
            return View(datos);
        }

        [HttpPost]
        public ActionResult EditarProducto(ProductoEnt entidad, HttpPostedFileBase imagenProducto)
        {
            System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + entidad.Imagen);

            entidad.Imagen = string.Empty;
            model.ActualizarProducto(entidad);

            string extensionImagen = Path.GetExtension(Path.GetFileName(imagenProducto.FileName));
            string rutaGuardarImagenes = AppDomain.CurrentDomain.BaseDirectory + "images\\" + entidad.IdProducto + extensionImagen;
            imagenProducto.SaveAs(rutaGuardarImagenes);

            entidad.Imagen = "\\images\\" + entidad.IdProducto + extensionImagen;
            model.ActualizarRuta(entidad);

            return RedirectToAction("ConsultarMantProductos", "Producto");
        }

    }
}