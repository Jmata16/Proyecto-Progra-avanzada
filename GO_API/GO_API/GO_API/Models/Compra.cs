//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GO_API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Compra
    {
        public long IdCompra { get; set; }
        public long IdUsuario { get; set; }
        public long IdProducto { get; set; }
        public System.DateTime FechaCompra { get; set; }
        public decimal PrecioPagado { get; set; }
    
        public virtual Producto Producto { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
