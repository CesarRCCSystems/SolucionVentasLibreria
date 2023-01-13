using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;

namespace CapaModelo
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public int IdTienda { get; set; }
        public Tienda oTienda { get; set; }
        public int IdRol { get; set; }
        public Rol oRol { get; set; }
        public List<Menu> oListaMenu { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }

        public string RutaImagen { get; set; }
        public string NombreImagen { get; set; }
       
        public string Imagen { get { return ImageBase64(out _); } set { }} 

        public string ImageBase64(out string error)
        {
            error = string.Empty;          
            string base64;
            try
            {     
                byte[] bytes = File.ReadAllBytes(RutaImagen + "\\" + NombreImagen);
                base64 = Convert.ToBase64String(bytes);       
                return string.Concat("data:image/", Path.GetExtension(NombreImagen), ";base64,", base64);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return "";
            }
              
        }


    }
}
