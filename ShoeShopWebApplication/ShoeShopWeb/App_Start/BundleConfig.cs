using System.Web;
using System.Web.Optimization;

namespace ShoeShopWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Template/plugins/jQuery/jquery-2.2.3.min.js"));

            bundles.Add(new StyleBundle("~/TemplateHeader/css").Include(
                          ("~/Template/bootstrap/css/bootstrap.min.css"),
                          ("~/Template/dist/css/font-awesome.min.css"),
                          ("~/Template/dist/css/ionicons.min.css"),
                          ("~/Template/dist/css/AdminLTE.min.css"),
                          ("~/Template/dist/css/skins/_all-skins.min.css")
                          ));

            bundles.Add(new ScriptBundle("~/TemplateFooter/js").Include(                           
                            ("~/Template/bootstrap/js/bootstrap.min.js"),
                            ("~/Template/plugins/slimScroll/jquery.slimscroll.min.js"),
                            ("~/Template/plugins/fastclick/fastclick.js"),
                            ("~/Template/dist/js/app.min.js"),
                            ("~/Template/dist/js/demo.js")
                            ));
        }
    }
}
