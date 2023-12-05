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
    public class ProductoNoLogController : ApiController
    {
        [HttpGet]
        [Route("api/ConsultarProductosNoLog")]
        public List<ProductoEnt> ConsultarProductosNoLog()
        {
            using (var bd = new GO_ProyectoEntities())
            {
                var datos = (from x in bd.Producto
                             join r in bd.Categoria on x.IdCategoria equals r.IdCategoria
                             select new
                             {
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
    }
}
