using Model;
using Repository.DAL;
using Service;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;

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
		private ReviewProductService reviewProductService;
		public HomeController()
		{
			newsService = new NewsService();
			sliderService = new SliderService();
			productService = new ProductService();
			menuService = new MenuService();
			categoryService = new CategoryService();
			wishListService = new WishListService();
			notifyService = new NotifyService();
			reviewProductService = new ReviewProductService();
		}
		public ActionResult Index()
		{
			//var user = (User)Session["User"];
			//if (user != null) ViewBag.wishList = wishListService.GetById(user.UserId).ToList();
			//ViewBag.ListProductNew = productService.ListProductNew().ToList();
			//ViewBag.ListNews = newsService.GetAll().ToList();
			//ViewBag.ReviewList = reviewProductService.GetAll();
			return View();
		}
		[HttpGet]
		public string GetProductNew()
		{
			var data = JsonConvert.SerializeObject(productService.ListProductNew().ToList(), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
			return data;
		}

		[HttpGet]
		public string GetProductHot()
		{
			var data = JsonConvert.SerializeObject(productService.ListProductHot().ToList(), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
			return data;
		}

		[HttpGet]
		public string GetBlog()
		{
			var data = JsonConvert.SerializeObject(newsService.GetAll().ToList(), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
			return data;
		}

		[ChildActionOnly]
		public ActionResult MainMenu()
		{
			ViewBag.Categories = categoryService.Search("", 1, 1).ToList();
			ViewBag.Sliders=sliderService.GetAll();
			var currentUser = (User)Session["User"];
			ViewBag.notifies = currentUser != null ? notifyService.GetById(currentUser.UserId) : new List<Notify>();
			var lst = categoryService.Search("", 1, 1).ToList();
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
			//List<Category> lst = new List<Category>();
			//using (var context = new DBEntityContext())
			//{
			//	lst = context.Categories.Where(x => x.Status).ToList();
			//}

			ViewBag.Categories = categoryService.Search("",1,1).ToList();
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
		public ActionResult CreateWishListIndex(string data)
		{
			var items = JsonConvert.DeserializeObject<List<WishList>>(data);
			var user = (Model.User)Session["User"];

			if (user != null)
			{
				var wishLists = wishListService.GetById(user.UserId).ToList();
				foreach (var item in items)
				{
					var currentItem = wishLists.FirstOrDefault(x => x.ProductID == item.ProductID);
					if (currentItem != null && item.ProductID == currentItem.ProductID)
					{
						wishListService.Delete(currentItem);
					}
					else
					{
						var w = new WishList
						{
							UserID = user.UserId,
							ProductID = item.ProductID
						};

						wishListService.Insert(w);
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