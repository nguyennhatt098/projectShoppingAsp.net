using System.Web.Mvc;

namespace ShopingCart.Areas.Admin.Controllers
{
    public class HomeAdminController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Error()
        {

	        return View();
        }

	}
}