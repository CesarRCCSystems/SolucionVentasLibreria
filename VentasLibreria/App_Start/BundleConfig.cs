using System.Web;
using System.Web.Optimization;

namespace VentasLibreria
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new Bundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new Bundle("~/bundles/Complementos").Include(
            //          "~/Scripts/bootstrap.js"));   

            //bundles.Add(new Bundle("~/bundles/bootstrap").Include(                   
            //          "~/Scripts/bootstrap.js"));  

            //--------------- Admin ELT ---------------//

            bundles.Add(new Bundle("~/bundles/Utils").Include(
                   "~/Scripts/Utils/Chart-Utils.js",
                   "~/Scripts/Utils/constants.js",
                   "~/Scripts/Utils/functions.js"
              ));

            bundles.Add(new Bundle("~/bundles/Complementos").Include(          
               "~/Scripts/loadingoverlay/loadingoverlay.min.js",

                //******  JQuery Validate  *****//
               "~/AdminLTE/plugins/jquery-validation/jquery.validate.min.js",
               "~/AdminLTE/plugins/jquery-validation/additional-methods.min.js",

                //****** JQuery UI ******//
                "~/AdminLTE/plugins/jquery-ui/jquery-ui.min.js"
               ));

            bundles.Add(new Bundle("~/bundles/AdminLTE").Include(                                   

                      //****** Popper ******//
                      //"~/Scripts/esm/popper.min.js",

                      //****** JQuery ******//
                      "~/AdminLTE/plugins/jquery/jquery.min.js",

                       //****** Bootstrap 4 ******//
                       //"~/AdminLTE/plugins/bootstrap/js/bootstrap.min.js",
                       //"~/AdminLTE/plugins/bootstrap/js/bootstrap.bundle.min.js",
                
                      ////****** Bootstrap 5 ******////
                      "~/Scripts/bootstrap.bundle.min.js",

                      //****** Popper ******//
                      //"~/Scripts/esm/popper.min.js",

                      //******  Datatables  ******//
                      "~/AdminLTE/plugins/datatables/jquery.dataTables.min.js",
                      "~/AdminLTE/plugins/datatables-responsive/js/dataTables.responsive.min.js",
                      "~/AdminLTE/plugins/datatables-buttons/js/dataTables.buttons.min.js",

                      "~/AdminLTE/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js",
                      "~/AdminLTE/plugins/datatables-responsive/js/responsive.bootstrap4.min.js",
                      "~/AdminLTE/plugins/datatables-buttons/js/buttons.bootstrap4.min.js",

                      "~/AdminLTE/plugins/datatables-buttons/js/buttons.html5.min.js",
                      "~/AdminLTE/plugins/datatables-buttons/js/buttons.print.min.js",
                      "~/AdminLTE/plugins/datatables-buttons/js/buttons.colVis.min.js",

    
                      //********* Select2 *********//
                      "~/AdminLTE/plugins/select2/js/select2.full.min.js",

                      //********* bs-custom-file-input *********//
                      "~/AdminLTE/plugins/bs-custom-file-input/bs-custom-file-input.min.js",

                      //*** Crear y editar archivos .zip  ***//
                      "~/AdminLTE/plugins/jszip/jszip.min.js",

                      //****** Crear PDFs ******//
                      "~/AdminLTE/plugins/pdfmake/vfs_fonts.js",
                      "~/AdminLTE/plugins/pdfmake/pdfmake.min.js",

                      //******  Sweetalert2  *****//
                      "~/AdminLTE/plugins/sweetalert2/sweetalert2.all.min.js",

                      //****** Toastr *****//
                      "~/AdminLTE/plugins/toastr/toastr.min.js",

                      //********* AdminLTE **********//
                      "~/AdminLTE/dist/js/adminlte.min.js"

                      //"~/AdminLTE/plugins/jquery-validation/jquery.validate.min.js",
                      //"~/AdminLTE/plugins/jquery-validation/additional-methods.min.js",
                      //"~/AdminLTE/plugins/jquery-validation/localization/messages_es_PE.min.js"

                      ));

            bundles.Add(new StyleBundle("~/AdminLTE/css").Include(
          

                      //****** FontAwesome *******//
                      "~/AdminLTE/plugins/fontawesome-free/css/all.min.css",

                      //****** Datatables ******//      
                      "~/AdminLTE/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css",  
                      "~/AdminLTE/plugins/datatables-responsive/css/responsive.bootstrap4.min.css",
                      "~/AdminLTE/plugins/datatables-buttons/css/buttons.bootstrap4.min.css",

                      //****** JQuery UI ******//
                      "~/AdminLTE/plugins/jquery-ui/jquery-ui.min.css",
                      "~/AdminLTE/plugins/jquery-ui/jquery-ui.structure.min.css",
                      "~/AdminLTE/plugins/jquery-ui/jquery-ui.theme.min.css",

                      //********* Select2 *********//
                      "~/AdminLTE/plugins/select2/css/select2.min.css",
                     // "~/AdminLTE/plugins/select2-bootstrap4-theme/select2-bootstrap4.min.css",
                      "~/Content/select2-bootstrap-5-theme/select2-bootstrap-5-theme.min.css",
                      //****** Mensajes ******//
                      //"~/AdminLTE/plugins/sweetalert2/sweetalert2.min.css",
                      "~/AdminLTE/plugins/toastr/toastr.min.css",

                      //******* AdminLTE *******//
                      "~/AdminLTE/dist/css/adminlte.min.css"

                      ));

            //************ Admin ELT **************//

            bundles.Add(new StyleBundle("~/Content/css").Include(
              ////****** Bootstrap 5 ******////
              "~/Content/bootstrap.min.css",

              "~/Content/site.css"
              ));
                

        }
    }
}
