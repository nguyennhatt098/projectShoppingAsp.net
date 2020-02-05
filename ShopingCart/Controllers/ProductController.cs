using Service;
using System;
using System.Linq;
using System.Web.Mvc;
using Model;
using Newtonsoft.Json;

namespace ShopingCart.Controllers
{
    public class ProductController : Controller
    {
        private ProductService productService;
        private CategoryService categoryService;
        private WishListService wishListService;
        private CommentService commentService;
        private ReviewProductService reviewProductService;

        public ProductController()
        {
            categoryService = new CategoryService();
            productService = new ProductService();
            wishListService = new WishListService();
            commentService = new CommentService();
            reviewProductService = new ReviewProductService();
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
            ViewBag.ReviewList = reviewProductService.GetAll();
            ViewBag.CountProduct = total;
            ViewBag.Page = page;
            ViewBag.TotalPage = totalPage;
            return View(model);
        }

        public ActionResult Detail(int id, int page = 1, int pageSize = 5, int pageRv = 1, int pageSizeRv = 5)
        {
            //var user = (User)Session["User"];
            //if (user != null) ViewBag.wishList = wishListService.GetById(user.UserId).ToList();
            var totalPage = (commentService.Count(id) + pageSize - 1) / pageSize;
            if (page <= 0) page = 1;
            else page = (page > totalPage && totalPage > 0) ? totalPage : page;
            var model = commentService.Search(id, page, pageSize).ToList();
            ViewBag.Page = page;
            ViewBag.TotalPage = totalPage;
            ViewBag.CommentList = model;
            //ViewBag.AnswerComment = commentService.answerComments();
            // review product list
            var totalPageRv = (reviewProductService.CountReviewProductById(id) + pageSizeRv - 1) / pageSizeRv;
            if (pageRv <= 0) pageRv = 1;
            else pageRv = (pageRv > totalPageRv && totalPageRv > 0) ? totalPageRv : pageRv;
            var modelRv = reviewProductService.GetReviewProductsByProductId(id, pageRv, pageSizeRv).ToList();
            ViewBag.PageRv = pageRv;
            ViewBag.TotalPageRv = totalPageRv;
            ViewBag.ReviewList = modelRv;
            //ViewBag.AnserReview = reviewProductService.AnswerReviews();
            //ViewBag.CalculateReview = reviewProductService.CalculateRate(id);
            //detail
            //ViewBag.CountWish = wishListService.CountByProductId(id);
            //ViewBag.ReviewStarList = reviewProductService.GetAll();
            //ViewBag.ListProductOther = productService.ListProductSale().ToList();
            //var product = productService.GetById(id);
            return View(productService.GetById(id));
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