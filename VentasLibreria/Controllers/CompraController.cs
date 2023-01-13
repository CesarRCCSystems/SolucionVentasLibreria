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
    public class CompraController : Controller
    {
        private static Usuario SesionUsuario;
        // GET: Compra
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Proveedores()
        {
            return View();
        } 
        public ActionResult RegistrarCompra()
        {
            SesionUsuario = (Usuario)Session["Usuario"];
            return View();
        }

        public ActionResult ConsultarCompra()
        {
            return View();
        } 
        
        public ActionResult AsignarProducto()
        {
            return View();
        }



        //******** PROVEEDOR *********
        #region Proveedor

        [HttpGet]
        public JsonResult ListarProveedores()
        {

            List<Proveedor> proveedores = CD_Proveedores.Instance.listar().ToList();
                
            JsonResult result = new JsonResult();
            
            result.Data = new { data  = proveedores };
            result.ContentType = "application/json";
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;


            return result;
        }

        [HttpPost]
        public JsonResult GuardarProveedor(Proveedor obj)
        {
            string mensaje = string.Empty;
            object resultado;
            
           if(obj.IdProveedor == 0)
            {
                resultado = CD_Proveedores.Instance.Registar(obj, out mensaje);
            }
            else
            {
                resultado = CD_Proveedores.Instance.Editar(obj, out mensaje);
            }

            JsonResult result = new JsonResult()
            {
                Data = new { resultado = resultado,  mensaje = mensaje},
                ContentType = "application/json",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
                 
            return result; 
        }

        [HttpPost]
        public JsonResult EliminarProveedor(int id)
        {
            bool respuesta = false;

            string mensaje = string.Empty;

            respuesta = CD_Proveedores.Instance.Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje}, JsonRequestBehavior.AllowGet);
        }

        #endregion

        //******** REGISTRAR COMPRA *********
        #region Registrar Compra

        [HttpPost]
        public JsonResult GuardarCompra(string xml)
        {
            string mensaje = string.Empty;
            xml = xml.Replace("!idusuario¡", SesionUsuario.IdUsuario.ToString());

            bool respuesta = CD_Compra.Instance.RegistrarCompra(xml, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarProductoPorTienda(int IdTienda)
        {

            List<Producto> oListaProducto = CD_Producto.Instance.Listar();
            List<ProductoTienda> oListaProductoTienda = CD_ProductoTienda.Instance.ObtenerProductoTienda();

            oListaProducto = oListaProducto.Where(x => x.Activo == true).ToList();
            if (IdTienda != 0)
            {
                oListaProductoTienda = oListaProductoTienda.Where(x => x.oTienda.IdTienda == IdTienda).ToList();
                oListaProducto = (from producto in oListaProducto
                                  join productotienda in oListaProductoTienda on producto.IdProducto equals productotienda.oProducto.IdProducto
                                  where productotienda.oTienda.IdTienda == IdTienda
                                  select producto).ToList();
            }

            return Json(new { data = oListaProducto }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        //******** CONSULTAR COMPRA *********
        #region Consultar Compra

        [HttpGet]     
        public JsonResult ListarCompras(string fechainicio, string fechafin, int idproveedor, int idtienda)
        {
            List<Compra> lista = CD_Compra.Instance.ObtenerListaCompra(Convert.ToDateTime(fechainicio), Convert.ToDateTime(fechafin), idproveedor, idtienda);
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CompraDocumento(int idcompra = 0)
        {
            Compra oCompra = CD_Compra.Instance.ObtenerDetalleCompra(idcompra);

            if (oCompra == null)
            {
                oCompra = new Compra();
            }
            return View(oCompra);
        }

        #endregion

        //******** ASIGNAR PRODUCTO *********
        #region Asignar Producto

        [HttpGet]
        public JsonResult ListarAsignaciones()
        {
            List<ProductoTienda> lista = CD_ProductoTienda.Instance.ObtenerProductoTienda();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrarProductoTienda(ProductoTienda objeto)
        {
            string mensaje = string.Empty;
            bool respuesta = CD_ProductoTienda.Instance.RegistrarProductoTienda(objeto, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EliminarProductoTienda(int id)
        {
            bool respuesta = CD_ProductoTienda.Instance.EliminarProductoTienda(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }


        #endregion
    }
}