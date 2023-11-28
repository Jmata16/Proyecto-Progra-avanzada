using MN_API.Entities;
using MN_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace MN_API.Controllers
{
    [Authorize]
    public class CarritoController : ApiController
    {

        [HttpPost]
        [Route("api/AgregarProductoCarrito")]
        public int AgregarProductoCarrito(CarritoEnt entidad)
        {
            using (var bd = new GO_ProyectoEntities())
            {
                var existeEnCarrito = (from x in bd.Carrito
                                       where x.IdUsuario == entidad.IdUsuario
                                          && x.IdProducto == entidad.IdProducto
                                       select x).ToList();

                var existeEnCompras = (from x in bd.Compra
                                       where x.IdUsuario == entidad.IdUsuario
                                          && x.IdProducto == entidad.IdProducto
                                       select x).ToList();

                if (existeEnCarrito.Count > 0 || existeEnCompras.Count > 0)
                {
                    return 0;
                }

                Carrito tabla = new Carrito();
                tabla.IdUsuario = entidad.IdUsuario;
                tabla.IdProducto = entidad.IdProducto;
                tabla.FechaRegistro = entidad.FechaRegistro;

                bd.Carrito.Add(tabla);
                return bd.SaveChanges();
            }
        }

        [HttpDelete]
        [Route("api/RemoverProductoCarrito")]
        public int RemoverProductoCarrito(long q)
        {
            using (var bd = new GO_ProyectoEntities())
            {
                var Producto = (from x in bd.Carrito
                             where x.IdCarrito == q
                             select x).FirstOrDefault();

                if (Producto != null)
                {
                    bd.Carrito.Remove(Producto);
                    return bd.SaveChanges();
                }

                return 0;
            }
        }

        [HttpGet]
        [Route("api/ConsultarCarrito")]
        public List<CarritoEnt> ConsultarCarrito(long q)
        {
            using (var bd = new GO_ProyectoEntities())
            {
                var datos = (from x in bd.Carrito
                             join r in bd.Producto on x.IdProducto equals r.IdProducto
                             where x.IdUsuario == q
                             select new
                             {
                                 x.IdCarrito,
                                 x.IdUsuario,
                                 x.IdProducto,
                                 x.FechaRegistro,
                                 r.Precio,
                                 r.Nombre,
                                 r.Stock
                             }).ToList();

                List<CarritoEnt> resp = new List<CarritoEnt>();

                if (datos.Count > 0)
                {
                    foreach (var item in datos)
                    {
                        resp.Add(new CarritoEnt
                        {
                            IdCarrito = item.IdCarrito,
                            IdUsuario = item.IdUsuario,
                            IdProducto = item.IdProducto,
                            FechaRegistro = item.FechaRegistro,
                            Precio = item.Precio,
                            Nombre = item.Nombre,
                            Stock = item.Stock,
                            Impuesto = item.Precio * 0.13M
                        });
                    }
                }

                return resp;
            }
        }

        [HttpGet]
        [Route("api/ConsultarCompras")]
        public List<CarritoEnt> ConsultarCompras(long q)
        {
            using (var bd = new GO_ProyectoEntities())
            {
                var datos = (from x in bd.Compra
                             join r in bd.Producto on x.IdProducto equals r.IdProducto
                             where x.IdUsuario == q
                             select new
                             {
                                 x.IdCompra,
                                 x.IdUsuario,
                                 x.IdProducto,
                                 x.FechaCompra,
                                 x.PrecioPagado,
                                 r.Nombre,
                                 r.Stock
                             }).ToList();

                List<CarritoEnt> resp = new List<CarritoEnt>();

                if (datos.Count > 0)
                {
                    foreach (var item in datos)
                    {
                        resp.Add(new CarritoEnt
                        {
                            IdCompra = item.IdCompra,
                            IdUsuario = item.IdUsuario,
                            IdProducto = item.IdProducto,
                            FechaCompra = item.FechaCompra,
                            PrecioPagado = item.PrecioPagado,
                            Nombre = item.Nombre,
                            Stock = item.Stock,
                            Impuesto = item.PrecioPagado * 0.13M
                        });
                    }
                }

                return resp;
            }
        }

        [HttpPost]
        [Route("api/ConfirmarPago")]
        public int ConfirmarPago(CarritoEnt entidad)
        {
            using (var bd = new GO_ProyectoEntities())
            {
                //Tomar los productos del carrito para meterlos en la tabla de compras
                var datos = (from x in bd.Carrito
                             join y in bd.Producto on x.IdProducto equals y.IdProducto
                             where x.IdUsuario == entidad.IdUsuario
                             select new { 
                                x.IdProducto,
                                x.IdUsuario,
                                y.Precio
                             }).ToList();

                if(datos.Count > 0)
                {
                    foreach (var item in datos)
                    {
                        Compra comp = new Compra();
                        comp.IdProducto = item.IdProducto;
                        comp.IdUsuario = item.IdUsuario;
                        comp.FechaCompra = DateTime.Now;
                        comp.PrecioPagado = item.Precio;
                        bd.Compra.Add(comp);
                    }

                    //Tomar los productos del carrito para borrarlos
                    var carrito = (from x in bd.Carrito
                                 where x.IdUsuario == entidad.IdUsuario
                                 select x).ToList();

                    if (carrito.Count > 0)
                    {
                        foreach (var item in carrito)
                        {
                            bd.Carrito.Remove(item);
                        }
                    }

                    return bd.SaveChanges();
                }

                return 0;
            }
        }

    }
}
