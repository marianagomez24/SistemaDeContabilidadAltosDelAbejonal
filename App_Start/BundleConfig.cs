using System.Web.Optimization;

namespace SistemaContabilidadAltosDelAbejonal
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios.  De esta manera estará
            //// para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/js").Include(//JAVASCRIPT!!
                                                           //Principal
                "~/assets/js/pages/layout.js",
                "~/assets/libs/jquery/jquery.min.js",
                "~/assets/libs/bootstrap/js/bootstrap.bundle.min.js",
                "~/assets/libs/metismenu/metisMenu.min.js",
                "~/assets/libs/simplebar/simplebar.min.js",
                "~/assets/libs/node-waves/waves.min.js",
                "~/assets/js/app.js",
                 "~/assets/libs/parsleyjs/parsley.min.js",
                          "~/assets/js/pages/form-validation.init.js",


                //libs/datables.net
                "~/assets/libs/datatables.net/js/jquery.dataTables.min.js",
                "~/assets/libs/datatables.net-bs5/js/dataTables.bootstrap5.min.js",
                "~/assets/libs/datatables.net-responsive/js/dataTables.responsive.min.js",
                "~/assets/libs/datatables.net-responsive-bs5/js/responsive.bootstrap5.min.js",

                //libs/js/pages
                "~/assets/js/pages/dashboard.init.js",
                "~/assets/js/pages/datatables-base.init.js",

                //libs/apexcharts    
                "~/assets/libs/apexcharts/apexcharts.min.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(//CSS!
                                                                 //Principal
                "~/assets/css/bootstrap.min.css",
                "~/assets/css/icons.min.css",
                "~/assets/libs/simplebar/simplebar.min.css",
                "~/assets/css/app.min.css",              
                "~/assets/libs/datatables.net-bs5/css/dataTables.bootstrap5.min.css",
                "~/assets/libs/datatables.net-responsive-bs5/css/responsive.bootstrap5.min.css",
                "~/Content/Chart.min.css",
                "~/Content/bootstrap-utilities.css"
                ));

            bundles.Add(new StyleBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/Chart.min.js"
                ));
        }
    }
}
