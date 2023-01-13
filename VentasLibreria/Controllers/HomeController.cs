using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaDatos;
using CapaModelo;

namespace VentasLibreria.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Usuario usuario = null;
            //try
            //{

            // usuario = CD_Usuario.Instance.ObtenerDetalleUsuario(1);
             
            //}
            //catch (Exception)
            //{
            // usuario = new Usuario();
            //    throw;
            //}


            return View();
        }

        public ActionResult FirstDashboard()
        {
            


            return View();
        }
        public ActionResult SecondDashboard()
        {



            return View();
        }
        public ActionResult ThirdDashboard()
        {



            return View();
        }

        #region General
        
        [HttpGet]
        public JsonResult ObtenerVentaporAño()
        {

            return Json(new { }, JsonRequestBehavior.AllowGet);
        } 
        [HttpGet]
        public JsonResult ObtenerCifras()
        {
            Response<Dashboard> resp = CD_Dashboard.Instance.GetDasboard_amounts();

            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Compras

        #endregion

        #region Ventas

        #endregion



    }
}