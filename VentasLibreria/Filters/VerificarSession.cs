using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VentasLibreria.Controllers;

using CapaModelo;

namespace VentasLibreria.Filters
{
    public class VerificarSession : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Usuario oUsuario = (Usuario)Activator.CreateInstance(typeof(Usuario));
            Usuario oUsuario = (Usuario)HttpContext.Current.Session["Usuario"];

            if (oUsuario == null)
            {
                if(filterContext.Controller is LoginController == false )
                {
                    filterContext.HttpContext.Response.Redirect("~/ /Autenticacion");
                }

            }
                
            base.OnActionExecuting(filterContext);
        }



        
            

    }
}