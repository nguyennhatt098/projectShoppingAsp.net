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
		private CommentService commentService;

		public ProductController()
        {
            categoryService = new CategoryService();
            productService = new ProductService();
			wishListService=new WishListService();
			commentService = new CommentService();
        }
        public ActionResult CategoryViewDetail(int id, int page = 1, int pageSize = 6)
        {
			var user = (User)Session["User"];
			if (user != null) ViewBag.wishList = wishListService.GetById(user.UserId).ToList();
	        ViewBag.ListCategory = categoryService.Search("", page, pageSize);
			var category = categoryService.GetById(id);
	        ViewBag.Category = category;
			var total = productService.Count(id);
			var totalPage = (int)Math.Ceiling((double)(total / pageSize));
			if (total % pageSize != 0) totalPage += 1;
			if (page <= 0) page = 1;
			else page = (page > totalPage && totalPage > 0) ? totalPage : page;
			var model = productService.ListProductGetByCategory(id, page, pageSize).ToList();
			ViewBag.CountProduct = total;
			ViewBag.Page = page;
            ViewBag.TotalPage = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }
        public ActionResult Detail(int id, int page = 1, int pageSize = 5)
        {
	        var user = (User)Session["User"];
			if (user != null) ViewBag.wishList = wishListService.GetById(user.UserId).ToList();
			var total = commentService.Count(id);
			var totalPage = (int)Math.Ceiling((double)(total / pageSize));
			if (page <= 0) page = 1;
			else page = (page > totalPage && totalPage > 0) ? totalPage : page;
			var model = commentService.Search(id, page, pageSize).ToList();
			ViewBag.Page = page;
			ViewBag.TotalPage = totalPage;
			ViewBag.Next = page + 1;
			ViewBag.Prev = page - 1;
			ViewBag.CommentList = model;
			//detail
			ViewBag.AnswerComment = commentService.answerComments();
			ViewBag.ListProductOther = productService.ListProductSale();
            var product = productService.GetById(id);
            ViewBag.Category = categoryService.GetById(product.Category_ID);
			return View(product);
        }
		[HttpPost]
		public ActionResult Comment(string content,int productId)
		{
			if (string.IsNullOrWhiteSpace(content)) return Json(new
			{
				status = true
			});

			var user = (User)Session["User"];
			if (user == null)
			{
				return Json(new
				{
					status = false
				});
			}
			var coment = new Comment
			{
				Question = content,
				CreatedDate = DateTime.Now,
				ProductId = productId,
				UserId = user.UserId,
				Status = true,
				ModifyDate=DateTime.Now
			};
			commentService.Insert(coment);
			return Json(new
			{
				status = true
			});
		}
		[HttpPost]
		public ActionResult CommentAnswer(string content,int commentId)
		{
			if (string.IsNullOrWhiteSpace(content)) return Json(new
			{
				status = true
			});

			var user = (User)Session["User"];

			if (user == null)
			{
				return Json(new
				{
					status = false
				});
			}

			var answer = new AnswerComment
			{
				CommentId=commentId,
				Content=content,
				CreatedDate=DateTime.Now,
				UserId=user.UserId,
				Status=1
			};
			commentService.InsertAnswer(answer);

			return Json(new
			{
				status = true
			});
		}
    }
}