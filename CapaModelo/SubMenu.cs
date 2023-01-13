using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class SubMenu
    {
        public int IdSubMenu { get; set; }
        public int IdMenu { get; set; }
        public string Nombre { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Icono { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
