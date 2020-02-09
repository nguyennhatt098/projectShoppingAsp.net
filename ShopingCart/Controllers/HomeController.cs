using Model;
using Service;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Model.ViewModel;
using Newtonsoft.Json;

namespace ShopingCart.Controllers
{
	public class HomeController : Controller
	{
		private const string CartSession = "CartSession";
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
			categoryService = new CategoryService();
			wishListService = new WishListService();
			notifyService = new NotifyService();
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
		public JsonResult GetProductNew()
		{
			//var data = JsonConvert.SerializeObject(productService.ListProductNew().ToList(), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
			//return data;
			var data = productService.ListProductNew().ToList();
			return Json(data, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult GetProductHot()
		{
			var data = productService.ListProductHot().ToList();
			return Json(data, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public string GetBlog()
		{
			var data = JsonConvert.SerializeObject(newsService.GetAll().ToList());
			return data;
		}

		[ChildActionOnly]
		public ActionResult MainMenu()
		{
			var currentUser = (User)Session["User"];
			ViewBag.notifies = currentUser != null ? notifyService.GetById(currentUser.UserId) : new List<Notify>();
			var lst = categoryService.Search("", 1, 1).ToList();
			var categories= CreateVM(null, lst);
			return PartialView(categories);
		}
		[ChildActionOnly]
		public ActionResult Footer()
		{
			return PartialView();
		}

		public IEnumerable<MenuViewModel> CreateVM(int? parentid, List<Category> source)
		{
			return from men in source
				where men.ParentID == parentid
				select new MenuViewModel()
				{
					MenuId = men.ID,
					Name = men.Name,
					Slug = men.Slug,
					Children = CreateVM(men.ID, source).ToList()
				};
		}

		[ChildActionOnly]
		public ActionResult Slider()
		{
			var categories = categoryService.Search("", 1, 1).ToList();
			ViewBag.Categories = CreateVM( null,categories);
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