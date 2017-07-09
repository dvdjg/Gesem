using System.Web;
using System.Web.Optimization;

namespace WebGestion
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui.min.js",
                        "~/Scripts/js.cookie.js",
                        "~/Scripts/jquery.dataTables.js",
                        "~/Scripts/jquery.signalR-2.2.0.js",
                        "~/Scripts/jquery.fancytree-all.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/dataTables.bootstrap.js",
                        "~/Scripts/knockout-{version}.js",
                        "~/Scripts/knockout.mapping-latest.js",
                        "~/Scripts/perpetuum.knockout.js",
                        "~/Scripts/respond.min.js",
                        "~/Scripts/dataTables.scroller.min.js",
                        "~/Scripts/dataTables.colReorder.min.js",
                        "~/Scripts/dataTables.buttons.min.js",
                        "~/Scripts/dataTables.responsive.min.js",
                        "~/Scripts/dataTables.select.min.js",
                        "~/Scripts/moment-with-locales.min.js",
                        "~/Scripts/bootstrap-datetimepicker.min.js",
                        "~/Scripts/buttons.dataTables.css",
                        "~/Scripts/buttons.print.min.js",
                        "~/Scripts/buttons.html5.min.js",
                        "~/Scripts/buttons.colVis.min.js",
                        "~/Scripts/buttons.bootstrap.min.js",
                        "~/Scripts/pdfmake.min.js",
                        "~/Scripts/vfs_fonts.js",
                        "~/Scripts/jszip.min.js",
                        "~/Scripts/chosen.jquery.min.js",
                        "~/Scripts/mediator.min.js",
                        "~/Scripts/sweetalert.min.js",
                        "~/Scripts/waves.min.js",
                        "~/Scripts/pnotify.js",
                        "~/Scripts/pnotify.animate.js",
                        "~/Scripts/pnotify.buttons.js",
                        "~/Scripts/pnotify.confirm.js",
                        "~/Scripts/pnotify.callbacks.js",
                        "~/Scripts/bootstrap-dynamic-tabs.js",
                        "~/Scripts/sammy.js",
                        "~/Scripts/Gesem/Routing.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.min.js",
            //          "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/bower_components/metisMenu/dist/metisMenu.min.css",
                      //"~/Content/site.css",
                      "~/Content/jquery.dataTables.min.css",
                      "~/Content/dataTables.bootstrap.css",
                      "~/Content/dataTables.scroller.min.css",
                      "~/Content/responsive.bootstrap.min.css",
                      "~/Content/colReorder.bootstrap.min.css",
                      "~/Content/select.dataTables.min.css",
                      "~/Content/keyTable.bootstrap.min.css",
                      "~/Content/buttons.dataTables.min.css",
                      "~/Content/buttons.bootstrap.min.css",
                      "~/Content/responsive.bootstrap.min.css",
                      "~/Content/bootstrap-datetimepicker.min.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/ui.fancytree.css",
                      "~/Content/sb-admin-2.css",
                      "~/Content/chosen.min.css",
                      "~/Content/sweetalert.css",
                      "~/Content/waves.css",
                      "~/Content/animate.css",
                      "~/Content/pnotify.css",
                      "~/Content/pnotify.brighttheme.css",
                      "~/Content/pnotify.buttons.css",
                      "~/Content/pnotify.history.css",
                      "~/Content/bootstrap-dynamic-tabs.css"));
        }
    }
}
