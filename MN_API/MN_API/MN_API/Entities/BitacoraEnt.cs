using MN_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MN_API.Entities
{
    public class BitacoraEnt
    {
        public string Origen { get; set; }
        public string Mensaje { get; set; }
        public long IdUsuario { get; set; }
    }
}