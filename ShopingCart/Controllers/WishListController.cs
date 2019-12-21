using Service;
using System.Web.Mvc;

namespace ShopingCart.Controllers
{
	public class WishListController : Controller
	{
		private WishListService wishListService;
		// GET: WishList
		public WishListController()
		{
			wishListService = new WishListService();
		}
		public ActionResult Index()
		{
			return View(wishListService.GetAll());
		}
		//[HttpPost]
		//public ActionResult Remove(WishList c)
		//{
		//	if (Session["User"] == null)
		//	{
		//		var wishList = (List<int>)Session[Common.CommonConstants.DATA_WISH];
		//		wishList.Remove(c.ProductID);
		//		Session[Common.CommonConstants.DATA_WISH] = wishList;
		//	}
		//	if (Session["User"] != null)
		//	{
		//		var data = (User)Session["User"];
		//		c.UserID = data.UserId;
		//		wishListService.Delete(c);
		//		return Json(new
		//		{
		//			status = false
		//		});
		//	}

		//	return Json(new
		//	{
		//		status = false

		//	});
		//}

	}
}