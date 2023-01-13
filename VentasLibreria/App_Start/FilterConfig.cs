using System.Web;
using System.Web.Mvc;
using VentasLibreria.Filters;

namespace VentasLibreria
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            filters.Add(new VerificarSession());
        }
    }
}
