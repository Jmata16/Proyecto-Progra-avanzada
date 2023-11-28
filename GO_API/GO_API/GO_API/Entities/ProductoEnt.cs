using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MN_API.Entities
{
    public class ProductoEnt
    {
        public long IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Imagen { get; set; }
        public byte IdCategoria { get; set; }

        public string NombreCategoria { get; set; }

    }
}