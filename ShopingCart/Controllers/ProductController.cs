using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Model;
using Newtonsoft.Json;
using Model.ViewModel;

namespace ShopingCart.Controllers
{
	public class ProductController : Controller
	{
		private ProductService productService;
		private CategoryService categoryService;
		private CommentService commentService;
		private ReviewProductService reviewProductService;

		public ProductController()
		{
			categoryService = new CategoryService();
			productService = new ProductService();
			commentService = new CommentService();
			reviewProductService = new ReviewProductService();
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

		public ActionResult CategoryViewDetail(int id)
		{
			var data=categoryService.Search("", 1, 1).ToList();
			ViewBag.ListCategory = CreateVM(null, data);
			var category = categoryService.GetById(id);
			ViewBag.Category = category;
			return View();
		}

		[HttpGet]
		public JsonResult GetProduct(int id, int page = 1, int pageSize = 6)
		{
			var total = productService.Count(id);
			var model = productService.ListProductGetByCategory(id, page, pageSize);
			var data = new SearchResponse<ProductViewModel> { items = model, TotalRecords = total };
			return Json(data, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult GetCategoryById(int id)
		{
			var category = categoryService.GetById(id);
			return Json(category, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Detail(int id, int page = 1, int pageSize = 5, int pageRv = 1, int pageSizeRv = 5)
		{
			//var user = (User)Session["User"];
			//if (user != null) ViewBag.wishList = wishListService.GetById(user.UserId).ToList();
			var product = productService.GetProductById(id);
			//var totalPage = (product.Comments.Count + pageSize - 1) / pageSize;
			////if (page <= 0) page = 1;
			////else page = (page > totalPage && totalPage > 0) ? totalPage : page;
			//var model = commentService.Search(id, page, pageSize);
			//ViewBag.Page = page;
			//ViewBag.TotalPage = totalPage;
			////ViewBag.CommentList = model;
			////ViewBag.AnswerComment = commentService.answerComments();
			//// review product list
			//var totalPageRv = (product.ReviewProducts.Count + pageSizeRv - 1) / pageSizeRv;
			////if (pageRv <= 0) pageRv = 1;
			////else pageRv = (pageRv > totalPageRv && totalPageRv > 0) ? totalPageRv : pageRv;
			//var modelRv = reviewProductService.GetReviewProductsByProductId(id, pageRv, pageSizeRv).ToList();
			//ViewBag.PageRv = pageRv;
			//ViewBag.TotalPageRv = totalPageRv;
			//ViewBag.ReviewList = modelRv;
			//ViewBag.AnserReview = reviewProductService.AnswerReviews();
			//ViewBag.CalculateReview = reviewProductService.CalculateRate(id);
			//detail
			//ViewBag.CountWish = wishListService.CountByProductId(id);
			//ViewBag.ReviewStarList = reviewProductService.GetAll();
			//ViewBag.ListProductOther = productService.ListProductSale().ToList();
			//var product = productService.GetById(id);
			return View(product);
		}

		[HttpGet]
		public JsonResult GetComments(int id, int page = 1, int pageSize = 6)
		{
			var data = commentService.Search(id, page, pageSize);
			return Json(data, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult GetReviews(int id, int pageRv = 1, int pageSizeRv = 5)
		{
			var data = reviewProductService.GetReviewProductsByProductId(id, pageRv, pageSizeRv);
			return Json(data, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult Comment(string content, int productId)
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
				ModifyDate = DateTime.Now
			};
			commentService.Insert(coment);
			return Json(new
			{
				status = true
			});
		}

		[HttpPost]
		public ActionResult CommentAnswer(string content, int commentId)
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
				CommentId = commentId,
				Content = content,
				CreatedDate = DateTime.Now,
				UserId = user.UserId,
				Status = 1
			};
			commentService.InsertAnswer(answer);

			return Json(new
			{
				status = true
			});
		}

		[HttpPost]
		public ActionResult CommentReview(string data)
		{
			var item = JsonConvert.DeserializeObject<AnswerReview>(data);
			var user = (User)Session["User"];

			if (user == null)
			{
				return Json(new
				{
					status = false
				});
			}

			var answerReview = new AnswerReview
			{
				ReviewId = item.ReviewId,
				Content = item.Content,
				CreatedDate = DateTime.Now,
				UserId = user.UserId,
				Status = 1
			};
			reviewProductService.InsertAnswerReview(answerReview);

			return Json(new
			{
				status = true
			});
		}
	}
}