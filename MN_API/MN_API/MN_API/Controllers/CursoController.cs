using MN_API.Entities;
using MN_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MN_API.Controllers
{
    [Authorize]
    public class ProductoController : ApiController
    {

        [HttpGet]
        [Route("api/ConsultarProductos")]
        public List<ProductoEnt> ConsultarProductos()
        {
            using (var bd = new GO_ProyectoEntities())
            {
                var datos = (from x in bd.Producto
                             select x).ToList();

                List<ProductoEnt> resp = new List<ProductoEnt>();

                if (datos.Count > 0)
                {
                    foreach (var item in datos)
                    {
                        resp.Add(new ProductoEnt
                        {
                            IdProducto = item.IdProducto,
                            Nombre = item.Nombre,
                            Descripcion = item.Descripcion,
                            Precio = item.Precio,
                            Instructor = item.Instructor,
                            Imagen = item.Imagen,
                            Video = item.Video
                        });
                    }
                }

                return resp;
            }
        }

        [HttpGet]
        [Route("api/ConsultarProducto")]
        public ProductoEnt ConsultarProducto(long q)
        {
            using (var bd = new GO_ProyectoEntities())
            {
                var datos = (from x in bd.Producto
                             where x.IdProducto == q
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    ProductoEnt resp = new ProductoEnt();
                    resp.IdProducto = datos.IdProducto;
                    resp.Nombre = datos.Nombre;
                    resp.Descripcion = datos.Descripcion;
                    resp.Instructor = datos.Instructor;
                    resp.Precio = datos.Precio;
                    resp.Imagen = datos.Imagen;
                    resp.Video = datos.Video;
                    return resp;
                }

                return null;
            }
        }

        [HttpPost]
        [Route("api/RegistrarProducto")]
        public long RegistrarProducto(ProductoEnt entidad)
        {
            using (var bd = new GO_ProyectoEntities())
            {
                Producto tabla = new Producto();
                tabla.Nombre = entidad.Nombre;
                tabla.Descripcion = entidad.Descripcion;
                tabla.Instructor = entidad.Instructor;
                tabla.Precio = entidad.Precio;
                tabla.Imagen = entidad.Imagen;
                tabla.Video = entidad.Video;

                bd.Producto.Add(tabla);
                bd.SaveChanges();

                return tabla.IdProducto;
            }
        }

        [HttpPut]
        [Route("api/ActualizarRuta")]
        public void ActualizarRuta(ProductoEnt entidad)
        {
            using (var bd = new GO_ProyectoEntities())
            {
                var datos = (from x in bd.Producto
                             where x.IdProducto == entidad.IdProducto
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    datos.Imagen = entidad.Imagen;
                    bd.SaveChanges();
                }
            }
        }

        [HttpPut]
        [Route("api/ActualizarProducto")]
        public int ActualizarProducto(ProductoEnt entidad)
        {
            using (var bd = new GO_ProyectoEntities())
            {
                var datos = (from x in bd.Producto
                             where x.IdProducto == entidad.IdProducto
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    datos.Nombre = entidad.Nombre;
                    datos.Descripcion = entidad.Descripcion;
                    datos.Instructor = entidad.Instructor;
                    datos.Precio = entidad.Precio;
                    datos.Imagen = entidad.Imagen;
                    datos.Video = entidad.Video;
                    return bd.SaveChanges();
                }

                return 0;
            }
        }

    }
}
