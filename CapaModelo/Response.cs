using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class Response<T>
    {
        public T respuesta { get; set; }
        public bool estado { get; set; }
        public string mensaje { get; set; } = "";

    }
}
