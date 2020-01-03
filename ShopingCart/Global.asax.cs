using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ShopingCart.Common;

namespace ShopingCart
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
		}
		protected void Application_Error(object sender, EventArgs e)
		{
			var errors = Server.GetLastError();

			var error = Server.GetLastError() as HttpException;
			if (errors != null || error != null && !string.IsNullOrWhiteSpace(error.Message))
			{
				if (error != null && error.GetHttpCode() == 404)
				{
					Server.ClearError();
					Context.Response.Redirect("/Home/Error404");
				}
				else
				{
					Context.ClearError();
					Context.Response.Redirect("/Home/Error500");
				}
			}
		}
	}
}
