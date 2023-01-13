using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CapaModelo;
using System.IO;

namespace VentasLibreria.Models
{
    public class Util
    {

        public static void ConsoleLog(HttpResponseBase resp, string mensaje)
        {           
            resp.Write(string.Format("<script>console.log('{0}')</script>", mensaje.Replace("'","*")));         
        }

        public static Usuario GeUserInfo()
        {
            return HttpContext.Current.Session["Usuario"] as Usuario;
        }

        public static string ConvertirBase64(string ruta, out bool conversion)
        {
            string textoBase64 = "";
            conversion = true;

            try
            {
                byte[] bytes = File.ReadAllBytes(ruta);
                textoBase64 = Convert.ToBase64String(bytes);
            }
            catch
            {
                conversion = false;
            }

            return textoBase64;
        }



    }
}