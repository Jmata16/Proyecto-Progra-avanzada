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
                             join r in bd.Categoria on x.IdCategoria equals r.IdCategoria
                             select new{
                                x.IdProducto,
                                x.Nombre,
                                x.Descripcion,
                                x.Precio,
                                x.Stock,
                                x.IdCategoria,
                                r.NombreCategoria,
                                x.Imagen
                             }).ToList();

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
                            Stock = item.Stock,
                            Imagen = item.Imagen,
                            IdCategoria = item.IdCategoria,
                            NombreCategoria = item.NombreCategoria,
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
                    resp.Stock = datos.Stock;
                    resp.Precio = datos.Precio;
                    resp.Imagen = datos.Imagen;
                    resp.IdCategoria = datos.IdCategoria;
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
                tabla.Stock = entidad.Stock;
                tabla.Precio = entidad.Precio;
                tabla.Imagen = entidad.Imagen;
                tabla.IdCategoria = entidad.IdCategoria;

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

        [HttpGet]
        [Route("api/ConsultarCategoria")]
        public List<CategoriaEnt> ConsultarCategoria()
        {
            using (var bd = new GO_ProyectoEntities())
            {
                var datos = (from x in bd.Categoria
                             select x).ToList();

                List<CategoriaEnt> resp = new List<CategoriaEnt>();

                if (datos.Count > 0)
                {
                    foreach (var item in datos)
                    {
                        resp.Add(new CategoriaEnt
                        {
                            IdCategoria = item.IdCategoria,
                            NombreCategoria = item.NombreCategoria
                        });
                    }
                }

                return resp;
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
                    datos.Stock = entidad.Stock;
                    datos.Precio = entidad.Precio;
                    datos.Imagen = entidad.Imagen;
                    datos.IdCategoria= entidad.IdCategoria;
                    return bd.SaveChanges();
                }

                return 0;
            }
        }

    }
}
