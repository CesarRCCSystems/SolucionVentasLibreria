using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaModelo;
using CapaDatos;

namespace VentasLibreria.Controllers
{
    [Authorize]
    public class AlmacenController : Controller
    {
        // GET: Almacen
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Marca()
        {
            return View();
        }

        public ActionResult Categorias()
        {
            return View();
        } 
        
        public ActionResult Productos()
        {
            return View();
        }

        /*********** MARCA ************/

        #region Marca

        [HttpGet]
        public JsonResult ListarMarcas()
        {
            List<Marca> marcas = CD_Marca.Instance.Listar().ToList();

            var result = new JsonResult();
            result.Data = new { data = marcas };
            result.ContentType = "application/json";
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        [HttpPost]
        public JsonResult GuardarMarca(Marca obj)
        {
            object resultado;    
            string mensaje = string.Empty;

            if (obj.IdMarca == 0)
            {
                resultado =  CD_Marca.Instance.Registrar(obj, out mensaje);
            }
            else
            {
                resultado = CD_Marca.Instance.Editar(obj, out mensaje);
            }   
                
            return Json(new { resultado = resultado, mensaje = mensaje}, JsonRequestBehavior.AllowGet);    
        }  
        
        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = CD_Marca.Instance.Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);    
        }

        #endregion

        /*********** CATEGORIAS ************/

        #region Categoria

        [HttpGet]
        public JsonResult ListarCategorias()
        {
            List<Categoria> marcas = CD_Categoria.Instance.Listar().ToList();

            var result = new JsonResult();
            result.Data = new { data = marcas };
            result.ContentType = "application/json";
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        [HttpPost]
        public JsonResult GuardarCategoria(Categoria obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.IdCategoria == 0)
            {
                resultado = CD_Categoria.Instance.Registrar(obj, out mensaje);
            }
            else
            {
                resultado = CD_Categoria.Instance.Editar(obj, out mensaje);
            }


            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = CD_Categoria.Instance.Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        /*********** PRODUCTOS ************/

        #region Producto

        [HttpGet]
        public JsonResult ListarProductos()
        {
            List<Producto> productos = CD_Producto.Instance.Listar().ToList();

            var result = new JsonResult();
            result.Data = new { data = productos };
            result.ContentType = "application/json";
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }
   
        [HttpPost]
        public JsonResult GuardarProducto(Producto obj)
        {
            string mensaje = string.Empty;
            object respuesta;

            if (obj.IdProducto == 0)
            {

                respuesta = CD_Producto.Instance.RegistrarProducto(obj, out mensaje);
            }
            else
            {
                respuesta = CD_Producto.Instance.ModificarProducto(obj, out mensaje);
            }

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            string mensaje = string.Empty;

            bool respuesta = CD_Producto.Instance.Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }




        #endregion
    }
}