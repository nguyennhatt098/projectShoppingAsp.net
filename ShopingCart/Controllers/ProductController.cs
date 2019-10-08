using Service;
using System;
using System.Linq;
using System.Web.Mvc;
using Model;
using PagedList;

namespace ShopingCart.Controllers
{
    public class ProductController : Controller
    {
        private ProductService productService;
        private CategoryService categoryService;
        private WishListService wishListService;


		public ProductController()
        {
            categoryService = new CategoryService();
            productService = new ProductService();
			wishListService=new WishListService();
        }
        // GET: Product
        public ActionResult Index(string searchString, int Page = 1, int PageSize = 10)
        {
	        var user = (User)Session["User"];
	        if (user != null) ViewBag.wishList = wishListService.GetById(user.UserId).ToList();
	        if (user == null) ViewBag.ListNotInUser = Session[Common.CommonConstants.DATA_WISH];
			ViewBag.searchString = searchString;
			ViewBag.ListCategory = categoryService.GetAll();
            ViewBag.ListProduct = productService.Search(searchString,Page,PageSize);
            return View(productService.Search(searchString, Page, PageSize).Where(x=>x.Status==true).ToPagedList(Page, PageSize));
        }
        public ActionResult CategoryViewDetail(int id, int pageIndex = 1, int pageSize = 10)
        {
	        var index = Request.Params["page"];
	        double value;
	        ViewBag.ListCategory = categoryService.GetAll();
	        var category = categoryService.GetById(id);
	        ViewBag.Category = category;
			if (!double.TryParse(index, out value)&&index!=null) return Redirect("https://localhost:44347/danh-muc-san-pham/4321-"+TempData["categoryId"]+"?page="+ TempData["GoBack"]);
			TempData["GoBack"] = index;
			TempData["categoryId"] = id;
			var totalPage = 0;
			var total = productService.Count();
	        totalPage = (int)Math.Ceiling((double)(total / pageSize));
			var convertIndex = 0;
			if (index == null || double.Parse(index) <= 0) convertIndex = 1;
			else convertIndex = (double.Parse(index) > totalPage) ? totalPage : int.Parse(index);
			var model = productService.ListProductGetByCategory(id, convertIndex, pageSize).ToList();
            ViewBag.Page = convertIndex;
            ViewBag.TotalPage = totalPage;
            ViewBag.Next = convertIndex + 1;
            ViewBag.Prev = convertIndex - 1;
            return View(model);
        }
        public ActionResult Detail(int id)
        {
            ViewBag.ListProductOther = productService.ListProductSale();
            var product = productService.GetById(id);
            ViewBag.Category = productService.GetById(product.Category_ID);
            return View(product);
        }

    }
}