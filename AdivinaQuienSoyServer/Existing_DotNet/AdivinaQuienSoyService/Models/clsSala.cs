using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdivinaQuienSoyService.Models
{
    public class clsSala
    {
        public int id { get; set; }
        public String nombre { get; set; }
        public int usuariosConectados { get; set; }
    }
}