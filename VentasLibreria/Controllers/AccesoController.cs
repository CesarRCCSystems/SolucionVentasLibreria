using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaModelo;
using CapaDatos;
using System.Web.Security;
using VentasLibreria.Models;
using System.IO;
using Newtonsoft.Json;
using System.Drawing;

namespace VentasLibreria.Controllers
{
    [Authorize]
    public class AccesoController : Controller
    {
        private Usuario UserInfo = Util.GeUserInfo();

        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }  
        public ActionResult Tiendas()
        {
            return View();
        } 
        public ActionResult Rol()
        {
            return View();
        } 
        public ActionResult Permisos()
        {
            return View();
        } 
     
        public ActionResult Usuarios()
        {
            return View();
        }

        //******** PERMISOS *********

        #region PERMISOS

        [HttpGet]
        public JsonResult ListarPermisos(int id)
        {
            List<Permisos> olista = CD_Permisos.Instance.ObtenerPermisos(id);

            return Json(olista, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(string xml)
        {
            bool Respuesta = CD_Permisos.Instance.ActualizarPermisos(xml);
            
            if(Respuesta)           
            Session["Usuario"] = CD_Usuario.Instance.ObtenerDetalleUsuario(UserInfo.IdUsuario).respuesta;

            return Json(new { resultado = Respuesta }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        //******** TIENDA *********

        #region Tienda

        [HttpGet]
        public JsonResult ListarTiendas()
        {
            List<Tienda> lista = CD_Tienda.Instance.listar();

            return Json(new {data = lista}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarTienda(Tienda obj)
        {
            object respuesta;
            string mensaje = string.Empty; 
            
            if(obj.IdTienda == 0)
            {
                respuesta = CD_Tienda.Instance.Guardar(obj, out mensaje);
            }
            else
            {
                respuesta = CD_Tienda.Instance.Editar(obj, out mensaje);
            }

            JsonResult json = new JsonResult()
            {
                Data = new { resultado = respuesta, mensaje = mensaje },
                ContentType = "application/json",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
         
            return json;
        }

        [HttpPost]
        public JsonResult EliminarTienda(int id)
        {
            bool respuesta = false;

            string mensaje = string.Empty;

            respuesta = CD_Tienda.Instance.Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        //******** USUARIOS *********

        #region Usuarios
        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<Usuario> usuarios = CD_Usuario.Instance.ObtenerUsuarios();
            var json =Json(new { data = usuarios }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }

        [HttpPost]
        public JsonResult GuardarUsuario(string obj, HttpPostedFileBase archivoImagen)
        {
            // object respuesta;
            int IdUsuarioGenerado = 0;
            string mensaje = string.Empty;
            bool operacion_exitosa = true;
            bool guardar_image_exito = true;
            Usuario oUsuario = new Usuario();
            oUsuario = JsonConvert.DeserializeObject<Usuario>(obj);
            
            if(oUsuario.IdUsuario == 0)
            {
                IdUsuarioGenerado = CD_Usuario.Instance.RegistrarUsuario(oUsuario, out mensaje);
                operacion_exitosa = IdUsuarioGenerado != 0 ? true : false;
                oUsuario.IdUsuario = IdUsuarioGenerado;
            }
            else
            {
                operacion_exitosa = CD_Usuario.Instance.ModificarUsuario(oUsuario, out mensaje);
            }

            if (operacion_exitosa)
            {
                if(archivoImagen != null)
                {
                    string rutaGuardar = Server.MapPath("/Files/Users/Images");
                    string extension = Path.GetExtension(archivoImagen.FileName);
                    string nombreImagen = string.Concat(oUsuario.IdUsuario, extension);

                    try
                    {
                        //reducir imagen

                        //string displayedImg = Server.MapPath("~") + "/Testing.jpg";

                        //string displayedImgThumb = Server.MapPath("~") + "/Thumb/";

                        //string imgFileName = Path.GetFileName(displayedImg);

                        //Image myimg = Image.FromFile(displayedImg);

                        //myimg = myimg.GetThumbnailImage(100, 100, null, IntPtr.Zero);

                        //myimg.Save(displayedImgThumb + imgFileName, myimg.RawFormat);

                        archivoImagen.SaveAs(Path.Combine(rutaGuardar, nombreImagen));
                    }
                    catch (Exception ex)
                    {
                        string msg = Conexion.etag + ex.Message;
                        guardar_image_exito = false;
                    }

                    if (guardar_image_exito)
                    {
                        oUsuario.RutaImagen = rutaGuardar;
                        oUsuario.NombreImagen = nombreImagen;
                        bool rspta =  CD_Usuario.Instance.GurdarDatosImagenUsu(oUsuario, out mensaje);
                        Session["Usuario"] = CD_Usuario.Instance.ObtenerDetalleUsuario(UserInfo.IdUsuario).respuesta;

                    }
                    else
                    {
                        mensaje = "Se guardo el producto pero hubo problemas con la imagen";
                    }
                }
            }

            JsonResult json = new JsonResult() { 
            Data = new { operacionExitosa = operacion_exitosa, IdUsuario = IdUsuarioGenerado, mensaje = mensaje },
            ContentType="application/json",
            JsonRequestBehavior=JsonRequestBehavior.AllowGet    
            };
            
            return json;
        }


        [HttpPost]
        public JsonResult ImageUsuario(int id)
        {
            bool conversion;

            Usuario oUsuario = CD_Usuario.Instance.ObtenerDetalleUsuario(id).respuesta;

             string textoBase64 = Util.ConvertirBase64(Path.Combine(oUsuario.RutaImagen, oUsuario.NombreImagen), out conversion);

            //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //serializer.MaxJsonLength = 500000000;

            //var json = Json(listaColaborador, JsonRequestBehavior.AllowGet);
            //json.MaxJsonLength = 500000000;
            //return json; 

            var json = Json(new
            {
                conversion = conversion,
                textoBase64 = textoBase64,
                extension = Path.GetExtension(oUsuario.NombreImagen)
            }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;

            return json;
        }
        
        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            bool respuesta = false;

            string mensaje = string.Empty;

            respuesta = CD_Usuario.Instance.Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        //******** ROL *********

        #region Rol

        [HttpGet]
        public JsonResult ListarRoles()
        {
            List<Rol> rol = CD_Rol.Instance.listar();

            return Json(new {data= rol}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarRol(Rol obj)
        {
            string mensaje = string.Empty;
            object respuesta;

            if(obj.IdRol == 0)
            {
                respuesta = CD_Rol.Instance.Registrar(obj, out mensaje);
            }
            else
            {
                respuesta = CD_Rol.Instance.Editar(obj, out mensaje);
            }

            JsonResult json = new JsonResult()
            {
                Data = new {resultado = respuesta, mensaje = mensaje},
                ContentType = "application/json",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            return json;
        }

        [HttpPost]
        public JsonResult EliminarRol(int id)
        {
            bool respuesta = false;

            string mensaje = string.Empty;

            respuesta = CD_Rol.Instance.Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion



    }
}