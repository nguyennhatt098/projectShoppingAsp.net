using System.Web;
using System.Web.Optimization;

namespace ShopingCart
{
	public class BundleConfig
	{
		// For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/core").Include(
				"~/Asset/FrontEnd/js/vendor/modernizr-2.8.3.min.js",
				"~/Asset/FrontEnd/js/vendor/jquery-1.12.0.min.js",
				"~/Asset/FrontEnd/js/bootstrap.min.js",
				"~/Asset/FrontEnd/js/ajax-mail.js",
				"~/Asset/FrontEnd/js/owl.carousel.min.js",
				"~/Asset/FrontEnd/js/jquery.nivo.slider.pack.js",
				"~/Asset/FrontEnd/js/plugins.js",
				"~/Asset/FrontEnd/js/main.js",
				"~/Asset/FrontEnd/js/custom.js",
				"~/Asset/FrontEnd/js/menu-mobile.js"));
			bundles.Add(new ScriptBundle("~/bundles/admin-js").Include(
				"~/Asset/Backend/bower_components/jquery/dist/jquery.min.js",
				"~/Asset/Backend/bower_components/bootstrap/dist/js/bootstrap.min.js",
				"~/Asset/Backend/bower_components/jquery-slimscroll/jquery.slimscroll.min.js",
				"~/Asset/Backend/dist/js/adminlte.min.js",
				"~/Asset/Backend/dist/js/demo.js",
				"~/Asset/Backend/vendor/tinymce/tinymce.min.js",
				"~/Asset/Backend/vendor/tinymce/config.js",
				"~/Asset/Backend/bower_components/datatables.net/js/jquery.dataTables.min.js",
				"~/Asset/Backend/js/custom.js"));
			bundles.Add(new StyleBundle("~/bundles/css-core").Include(
				"~/Asset/FrontEnd/css/bootstrap.min.css",
				"~/Asset/FrontEnd/css/font-awesome.min.css",
				"~/Asset/FrontEnd/css/pe-icon-7-stroke.css",
				"~/Asset/FrontEnd/css/nivo-slider.css",
				"~/Asset/FrontEnd/css/magnific-popup.css",
				"~/Asset/FrontEnd/css/plugins/animate.css",
				"~/Asset/FrontEnd/css/plugins/owl.carousel.css",
				"~/Asset/FrontEnd/css/plugins/jquery-ui.min.css",
				"~/Asset/FrontEnd/css/plugins/slick.css",
				"~/Asset/FrontEnd/css/plugins/meanmenu.min.css",
				"~/Asset/FrontEnd/css/shortcode/header.css",
				"~/Asset/FrontEnd/css/shortcode/slider.css",
				"~/Asset/FrontEnd/css/shortcode/footer.css",
				"~/Asset/FrontEnd/css/shortcode/blog.css",
				"~/Asset/FrontEnd/css/shortcode/default.css",
				"~/Asset/FrontEnd/css/shortcode/product.css",
				"~/Asset/FrontEnd/css/shortcode/testimonial.css",
				"~/Asset/FrontEnd/css/shortcode/modal.css",
				"~/Asset/FrontEnd/css/shortcode/banner.css",
				"~/Asset/FrontEnd/style.css",
				"~/Asset/FrontEnd/css/responsive.css",
				"~/Asset/FrontEnd/css/custom.css",
				"~/Asset/FrontEnd/css/menu.css",
				"~/Asset/FrontEnd/css/menu-product.css",
				"~/Asset/FrontEnd/css/menu-mobile.css",
				"~/Asset/FrontEnd/css/process-bar.css"));
			bundles.Add(new StyleBundle("~/bundles/admin-css").Include(
				"~/Asset/Backend/bower_components/bootstrap/dist/css/bootstrap.min.css",
				//"~/Asset/Backend/bower_components/font-awesome/font-awesome.min.css",
				//"~/Asset/Backend/bower_components/Ionicons/css/ionicons.min.css",
				"~/Asset/Backend/dist/css/AdminLTE.min.css",
				"~/Asset/Backend/dist/css/skins/_all-skins.min.css"
				));
			BundleTable.EnableOptimizations = true;
		}
	}
}
