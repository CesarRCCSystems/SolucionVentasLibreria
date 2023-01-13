using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VentasLibreria.Models;
using CapaModelo;
using CapaDatos;

namespace VentasLibreria.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Autenticacion()
        {
            Usuario usu =  Session["Usuario"] as Usuario;

            if(usu != null)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {

                return View();
            }
        }

        [HttpPost]
        public ActionResult Autenticacion(string usuario, string password)
        {        
            Response<int> resp = CD_Usuario.Instance.LoginUsuario(usuario, password);

            if (resp.respuesta == 0)
            {
                ViewBag.Error = "Usuario o contraseña incorrecta";
                ViewBag.Message = resp.mensaje;
                Util.ConsoleLog(Response, resp.mensaje);
            }
            else
            {
                Response<Usuario> detalleUsu = CD_Usuario.Instance.ObtenerDetalleUsuario(resp.respuesta);
                Session["Usuario"] = detalleUsu.respuesta;

                FormsAuthentication.SetAuthCookie(detalleUsu.respuesta.Correo, false);
                ViewBag.Error = null;
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            Session["Usuario"] = null;
            return RedirectToAction("Autenticacion", "Login");
        }


        public ActionResult VerificarSession()
        {
            if (Session["Usuario"] == null)
                return Content("1");

            return Content("0");
        }

    }
}