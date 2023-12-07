using GO_WEB.Entities;
using GO_WEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MN_WEB.Controllers
{
    public class ProductoController : Controller
    {
        ProductoModel model = new ProductoModel();
        CarritoModel modelCarrito = new CarritoModel();

        [HttpGet]
        public ActionResult ConsultarMantProductos()
        {
            var datos = model.ConsultarProductos();
            return View(datos);
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            var Categoria = model.ConsultarCategoria();
            var listaCategoria = new List<SelectListItem>();

            foreach (var item in Categoria)
            {
                listaCategoria.Add(new SelectListItem { Text = item.NombreCategoria, Value = item.IdCategoria.ToString() });
            }

            ViewBag.ListaCategoria = listaCategoria;

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
            var Categoria = model.ConsultarCategoria();
            var listaCategoria = new List<SelectListItem>();

            foreach (var item in Categoria)
            {
                listaCategoria.Add(new SelectListItem { Text = item.NombreCategoria, Value = item.IdCategoria.ToString() });
            }

            ViewBag.ListaCategoria = listaCategoria;

            return View(datos);
        }


        [HttpPost]
        public ActionResult EditarProducto(ProductoEnt entidad, HttpPostedFileBase imagenProducto)
        {
            if (imagenProducto != null && imagenProducto.ContentLength > 0)
            {
                if (!string.IsNullOrEmpty(entidad.Imagen))
                {
                    System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + entidad.Imagen);
                }

                entidad.Imagen = string.Empty;


                model.ActualizarProducto(entidad);


                string extensionImagen = Path.GetExtension(Path.GetFileName(imagenProducto.FileName));
                string rutaGuardarImagenes = AppDomain.CurrentDomain.BaseDirectory + "images\\" + entidad.IdProducto + extensionImagen;
                imagenProducto.SaveAs(rutaGuardarImagenes);

                entidad.Imagen = "\\images\\" + entidad.IdProducto + extensionImagen;
                model.ActualizarRuta(entidad);
            }
            else
            {
                model.ActualizarProducto(entidad);
            }

            return RedirectToAction("ConsultarMantProductos", "Producto");
        }

        [HttpGet]
        public ActionResult Mouse()
        {
            var datos = modelCarrito.ConsultarCarrito(long.Parse(Session["IdSesion"].ToString()));
            Session["Cantidad"] = datos.Count();
            Session["SubTotal"] = datos.Sum(x => x.Precio);
            Session["Total"] = datos.Sum(x => x.Precio) + (datos.Sum(x => x.Precio) * 0.13M);

            var Productos = model.ConsultarProductos();
            return View(Productos);
        }

        [HttpGet]
        public ActionResult Audífonos()
        {
            var datos = modelCarrito.ConsultarCarrito(long.Parse(Session["IdSesion"].ToString()));
            Session["Cantidad"] = datos.Count();
            Session["SubTotal"] = datos.Sum(x => x.Precio);
            Session["Total"] = datos.Sum(x => x.Precio) + (datos.Sum(x => x.Precio) * 0.13M);

            var Productos = model.ConsultarProductos();
            return View(Productos);
        }


        [HttpGet]
        public ActionResult Monitores()
        {
            var datos = modelCarrito.ConsultarCarrito(long.Parse(Session["IdSesion"].ToString()));
            Session["Cantidad"] = datos.Count();
            Session["SubTotal"] = datos.Sum(x => x.Precio);
            Session["Total"] = datos.Sum(x => x.Precio) + (datos.Sum(x => x.Precio) * 0.13M);

            var Productos = model.ConsultarProductos();
            return View(Productos);
        }


        [HttpGet]
        public ActionResult Teclados()
        {
            var datos = modelCarrito.ConsultarCarrito(long.Parse(Session["IdSesion"].ToString()));
            Session["Cantidad"] = datos.Count();
            Session["SubTotal"] = datos.Sum(x => x.Precio);
            Session["Total"] = datos.Sum(x => x.Precio) + (datos.Sum(x => x.Precio) * 0.13M);

            var Productos = model.ConsultarProductos();
            return View(Productos);
        }


    }
}