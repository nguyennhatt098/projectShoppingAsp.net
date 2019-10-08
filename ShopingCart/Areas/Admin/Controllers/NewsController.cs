﻿using Model;
using Service;
using System.Web.Mvc;
using ShopingCart.Common;

namespace ShopingCart.Areas.Admin.Controllers
{
	public class NewsController : Controller
	{
		private NewsService newsService;
		public NewsController()
		{
			newsService = new NewsService();
		}
		// GET: Admin/News
		[HasCredential(ActionId = 31)]
		public ActionResult Index()
		{
			return View(newsService.GetAll());
		}
		public ActionResult GetById(int id)
		{
			return View(newsService.GetById(id));
		}
		[HttpGet]
		[HasCredential(ActionId = 32)]
		public ActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[HasCredential(ActionId = 32)]
		public ActionResult Create(News n)
		{
			if (ModelState.IsValid)
			{
				var result = newsService.Insert(n);
				if (result > 0)
				{
					TempData["message"] = "Added";
				}
				else
				{
					TempData["message"] = "false";
				}
			}
			return RedirectToAction("Index");
		}
		[HasCredential(ActionId = 33)]
		public ActionResult Edit(int id)
		{
			return View(newsService.GetById(id));
		}
		[HasCredential(ActionId = 33)]
		public ActionResult Edit(News n)
		{
			if (ModelState.IsValid)
			{
				var result = newsService.Update(n);
				if (result > 0)
				{
					TempData["message"] = "Added";
				}
				else
				{
					TempData["message"] = "false";
				}
			}
			return RedirectToAction("Index");
		}
		[HasCredential(ActionId = 34)]
		public ActionResult Delete(int id)
		{
			var result = newsService.Delete(id);
			if (result > 0)
			{
				TempData["message"] = "Added";
			}
			else
			{
				TempData["message"] = "false";
			}
			return RedirectToAction("Index");
		}

	}
}