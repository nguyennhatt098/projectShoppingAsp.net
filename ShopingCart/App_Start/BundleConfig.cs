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
				"~/Scripts/bootstrap.js"));
			bundles.Add(new StyleBundle("~/bundles/css-core").Include(
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
			BundleTable.EnableOptimizations = true;
		}
	}
}
