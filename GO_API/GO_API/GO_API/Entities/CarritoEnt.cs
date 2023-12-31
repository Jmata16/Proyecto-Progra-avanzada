﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GO_API.Entities
{
    public class CarritoEnt
    {
        public long IdCarrito { get; set; }
        public long IdCompra { get; set; }
        public long IdUsuario { get; set; }
        public long IdProducto { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioPagado { get; set; }
        public int Stock { get; set; }
        public string Nombre { get; set; }
        public decimal Impuesto { get; set; }
    }
}