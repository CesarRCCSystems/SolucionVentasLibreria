using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaModelo;
using CapaDatos;
using System.Globalization;

namespace VentasLibreria.Controllers
{
    [Authorize]
    public class VentaController : Controller
    {
        private static Usuario SessionUsuario;

        // GET: Venta
        public ActionResult Index()
        {
            return View();
        } 
        public ActionResult Clientes()
        {
            return View();
        }  
        public ActionResult RegistrarVenta()
        {
            SessionUsuario = (Usuario)Session["Usuario"];

            return View();
        } 
        public ActionResult ConsultarVenta()
        {
            return View();
        }

        //******** CLIENTE ********//
        #region Cliente

        [HttpGet]
        public JsonResult ListarClientes()
        {

            List<Cliente> lista = CD_Cliente.Instance.listar();

            
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost] 
        public JsonResult GuardarCliente(Cliente obj)
        {
            object respuesta;
            string mensaje = string.Empty;

            if(obj.IdCliente == 0)
            {
                respuesta = CD_Cliente.Instance.RegistrarCliente(obj, out mensaje);
            }
            else
            {
                respuesta = CD_Cliente.Instance.ModificarCliente(obj, out mensaje);
            }         

            return Json(new { resultado = respuesta, mensaje= mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EliminarCliente(int id)
        {
            bool respuesta = false;

            string mensaje = string.Empty;

            respuesta = CD_Cliente.Instance.Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion


        //******** REGISTRAR VENTA ********//
        #region Registrar Venta
       
        [HttpGet]
        public JsonResult ObtenerUsuario()
        {
            string mensaje = string.Empty;
            Response<Usuario>  resp = CD_Usuario.Instance.ObtenerDetalleUsuario(SessionUsuario.IdUsuario);
         
            return Json( new { resultado = resp.respuesta, mensaje = resp.mensaje }, JsonRequestBehavior.AllowGet);
        }
       
        [HttpGet]
        public JsonResult ObtenerProductoPorTienda(int IdTienda)
        {

            List<ProductoTienda> oListaProductoTienda = CD_ProductoTienda.Instance.ObtenerProductoTienda();
            oListaProductoTienda = oListaProductoTienda.Where(x => x.oTienda.IdTienda == IdTienda && x.Stock > 0).ToList();


            return Json(new { data = oListaProductoTienda }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ControlarStock(int idproducto, int idtienda, int cantidad, bool restar)
        {  
            Response<bool> result = CD_ProductoTienda.Instance.ControlarStock(idproducto, idtienda, cantidad, restar);
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarVenta(string xml)
        {            
            xml = xml.Replace("!idusuario¡", SessionUsuario.IdUsuario.ToString());

            Response<int> rsp = CD_Venta.Instance.RegistrarVenta(xml);
          
            return Json(rsp, JsonRequestBehavior.AllowGet);       
        }

        #endregion

        //******** CONSULTAR VENTA ********//
        #region Consultar Venta

        [HttpGet]
        public JsonResult Obtener(string codigo, string fechainicio, string fechafin, string numerodocumento, string nombres)
        {
            List<Venta> lista = CD_Venta.Instance.ObtenerListaVenta(codigo, Convert.ToDateTime(fechainicio), Convert.ToDateTime(fechafin), numerodocumento, nombres);

            if (lista == null)
                lista = new List<Venta>();

            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
        #endregion
      
        public ActionResult VentaDocumento(int IdVenta = 0)
        {
            Venta oVenta = CD_Venta.Instance.ObtenerDetalleVenta(IdVenta);

            NumberFormatInfo formato = new CultureInfo("es-PE").NumberFormat;
            formato.CurrencyGroupSeparator = ".";


            if (oVenta == null)
                oVenta = new Venta();
            else
            {

                oVenta.oListaDetalleVenta = (from dv in oVenta.oListaDetalleVenta
                                             select new DetalleVenta()
                                             {
                                                 Cantidad = dv.Cantidad,
                                                 NombreProducto = dv.NombreProducto,
                                                 PrecioUnidad = dv.PrecioUnidad,
                                                 TextoPrecioUnidad = dv.PrecioUnidad.ToString("N", formato), //numero.ToString("C", formato)
                                                 ImporteTotal = dv.ImporteTotal,
                                                 TextoImporteTotal = dv.ImporteTotal.ToString("N", formato)
                                             }).ToList();

                oVenta.TextoImporteRecibido = oVenta.ImporteRecibido.ToString("N", formato);
                oVenta.TextoImporteCambio = oVenta.ImporteCambio.ToString("N", formato);
                oVenta.TextoTotalCosto = oVenta.TotalCosto.ToString("N", formato);
            }


            return View(oVenta);
        }
   
    }
}