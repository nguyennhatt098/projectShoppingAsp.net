using Model;
using Service;
using System.Linq;
using System.Web.Mvc;
using ShopingCart.Common;
using Service.Interface;

namespace ShopingCart.Areas.Admin.Controllers
{
	public class CategoryController : BaseController
	{
		private IServices<Category> _categoryService;
		private CommentService commentService;
		public CategoryController(IServices<Category> categoryService)
		{
			commentService = new CommentService();
			_categoryService = categoryService;
		}
		// GET: Admin/_categoryService
		[HasCredential(ActionId = 1)]
		public ActionResult Index()
		{
			return View(commentService.Categories());
		}
		[HttpGet]
		[HasCredential(ActionId = 2)]
		public ActionResult Create()
		{
			ViewBag.ParentID = new SelectList(_categoryService.GetAll(), "ID", "Name");
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[HasCredential(ActionId = 2)]
		public ActionResult Create(Category c)
		{
			if (ModelState.IsValid)
			{
				var result = _categoryService.Insert(c);
				if (result > 0)
				{
					TempData["message"] = "Added";
				}
				else if (result == -2)
				{
					ModelState.AddModelError("Name", "_categoryService Name Đã Tồn Tại");
					ViewBag.ParentID = new SelectList(_categoryService.GetAll(), "ID", "Name");
					return View();
				}
				else
				{
					TempData["message"] = "false";
				}
				return RedirectToAction("Index");
			}
			ViewBag.ParentID = new SelectList(_categoryService.GetAll(), "ID", "Name");
			return View();
		}
		[HttpGet]
		[HasCredential(ActionId = 3)]
		public ActionResult Edit(int id)
		{
			
			return View(_categoryService.GetById(id));
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[HasCredential(ActionId = 3)]
		public ActionResult Edit(Category c)
		{
			if (ModelState.IsValid)
			{
				var result = _categoryService.Update(c);
				if (result > 0)
				{
					TempData["message"] = "Added";
				}
				else if (result == -2)
				{
					ModelState.AddModelError("Name", "_categoryService Name Đã Tồn Tại");
					ViewBag.ParentID = new SelectList(_categoryService.GetAll().Where(s => s.ParentID == null), "ID", "Name", _categoryService.GetById(c.ID).ParentID);
					return View();
				}
				else
				{
					TempData["message"] = "false";
				}
				return RedirectToAction("Index");
			}

			return View();

		}
		[HasCredential(ActionId = 4)]
		public ActionResult Delete(int id)
		{
           
			var result = _categoryService.Delete(id);
			if (result > 0)
			{
				TempData["message"] = "Added";
			}
			else if (result == -1)
			{
				TempData["message"] = "Ex";
				TempData["data"] = "Danh mục đang được sử dụng! Bạn không thể xóa";
			}
			else
			{
				TempData["message"] = "false";
			}
			return RedirectToAction("Index");
		}

	}
}