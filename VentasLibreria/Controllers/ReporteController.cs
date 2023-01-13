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
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ReporteProductos()
        {
            return View();
        }   
        public ActionResult ReporteVentas()
        {
            return View();
        }

        //***** REPORTE DE PRODUCTOS ******//
        #region Reporte Producos

        [HttpGet]
        public JsonResult ReporteObtenerProducto(int idtienda, string codigoproducto)
        {
            List<ReporteProducto> lista = CD_Reportes.Instance.ReporteProductoTienda(idtienda, codigoproducto);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        #endregion


        //***** REPORTE DE VENTAS ******//
        #region Reporte Ventas

        [HttpGet]
        public JsonResult ReporteObtenerVenta(string fechainicio, string fechafin, int idtienda)
        {
            List<ReporteVenta> lista = CD_Reportes.Instance.ReporteVenta(Convert.ToDateTime(fechainicio), Convert.ToDateTime(fechafin), idtienda);
          
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        #endregion


    }
}