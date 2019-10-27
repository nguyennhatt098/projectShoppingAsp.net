using Model;
using Repository.DAL;
using Service;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopingCart.Controllers
{
    public class HomeController : Controller
	{
		private const string CartSession = "CartSession";
		private MenuService menuService;
		private ProductService productService;
		private SliderService sliderService;
		private CategoryService categoryService;
		private WishListService wishListService;
        private NewsService newsService;
		private NotifyService notifyService;
		public HomeController()
		{
            newsService = new NewsService();
            sliderService = new SliderService();
			productService = new ProductService();
			menuService = new MenuService();
			categoryService = new CategoryService();
			wishListService = new WishListService();
			notifyService = new NotifyService();
		}
		public ActionResult Index()
		{
			var user = (User)Session["User"];
			if (user != null) ViewBag.wishList = wishListService.GetById(user.UserId).ToList();
			if (user == null) ViewBag.ListNotInUser = Session[Common.CommonConstants.DATA_WISH];
			//ViewBag.ListProductHot = productService.ListProductHot();
			ViewBag.ListProductNew = productService.ListProductNew();
            ViewBag.ListNews = newsService.GetAll();
			ViewBag.ListProductSale = productService.ListProductSale();
			return View(productService.ListProductHot());
		}

		[ChildActionOnly]
		public ActionResult MainMenu()
		{
			var currentUser = (User)Session["User"];
			ViewBag.notifies = currentUser != null ? notifyService.GetById(currentUser.UserId) : new List<Notify>();
			var lst=categoryService.Search("",1,1).ToList();
			return PartialView(lst);
		}
        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = new Footer();
            using (var context = new DBEntityContext())
            {
                 model = context.Footers.FirstOrDefault(x => x.Status);
            }
            return PartialView(model);
        }

		[ChildActionOnly]
		public ActionResult Slider()
		{
			List<Category> lst = new List<Category>();
			using (var context = new DBEntityContext())
			{
				lst = context.Categories.ToList();
			}

			ViewBag.Categories = lst;
			return PartialView(sliderService.GetAll());
		}

		[ChildActionOnly]
		public ActionResult HeaderCart()
		{
			var cart = Session[CartSession];
			var list = new List<CartItem>();
			if (cart != null)
			{
				list = (List<CartItem>)cart;
			}
			return PartialView(list);
		}
		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}
		[HttpPost]
		public ActionResult Create(WishList c)
		{
			if (Session["User"] == null)
			{

				if (Session[Common.CommonConstants.DATA_WISH] == null)
				{
					var listWish = new List<int>();
					listWish.Add(c.ProductID);
					Session[Common.CommonConstants.DATA_WISH] = listWish;
				}
				else
				{
					var listWishs = (List<int>)Session[Common.CommonConstants.DATA_WISH];
					listWishs.Add(c.ProductID);
					Session[Common.CommonConstants.DATA_WISH] = listWishs;
				}
			}
			if (Session["User"] != null)
			{
				var data = (Model.User)Session["User"];
				c.UserID = data.UserId;
				wishListService.Insert(c);
			}

			return Json(new
			{
				status = false

			});
		}

		[HttpPost]
		public ActionResult CreateWishListIndex(WishList c)
		{
			var user = (Model.User)Session["User"];
			var listWishs = (List<int>)Session[Common.CommonConstants.DATA_WISH]==null ? new List<int>(): (List<int>)Session[Common.CommonConstants.DATA_WISH];
			if (user == null)
			{

				if (listWishs.Count==0)
				{
					var listWish = new List<int>();
					listWish.Add(c.ProductID);
					Session[Common.CommonConstants.DATA_WISH] = listWish;
					return Json(new
					{
						status = true
					});
				}
				else
				{
					
					foreach (var item in listWishs)
					{
						if (item == c.ProductID)
						{
							listWishs.Remove(c.ProductID);
							Session[Common.CommonConstants.DATA_WISH] = listWishs;
							return Json(new
							{
								status = false
							});
						}
						else
						{
							listWishs.Add(c.ProductID);
							Session[Common.CommonConstants.DATA_WISH] = listWishs;
							return Json(new
							{
								status = true
							});
						}
					}
					
					Session[Common.CommonConstants.DATA_WISH] = listWishs;
				}
			}
			if (user != null)
			{
				
				
				var wishLists = wishListService.GetById(user.UserId).ToList();
				foreach (var item in wishLists)
				{
					if (item.ProductID == c.ProductID)
					{
						wishListService.Delete(c);
						return Json(new
						{
							status = false
						});
					}
					else
					{
						c.UserID = user.UserId;
						wishListService.Insert(c);
						return Json(new
						{
							status = true
						});
					}
				}
				
			}

			return Json(new
			{
				status = false

			});
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
		public ActionResult Error404()
		{
			return View();
		}
		public ActionResult Error500()
		{
			return View();
		}
	}
}