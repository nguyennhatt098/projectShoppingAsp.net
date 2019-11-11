using Model;
using Service;
using System.Web.Mvc;
using BotDetect.Web.Mvc;
using ShopingCart.Common;
using System.IO;
using System.Web;

namespace ShopingCart.Controllers
{
	public class ResgiterController : Controller
	{
		private UserService userService;
		private LoginService loginService;
		public ResgiterController()
		{
			userService = new UserService();
			loginService = new LoginService();
		}
		// GET: Resgiter
		public ActionResult Index()
		{
			return View();
		}
		[HttpPost]
		[CaptchaValidationActionFilter("CaptchaCode", "registerCaptcha", "Mã Xác Nhận Không Đúng!")]
		public ActionResult Index(User user, HttpPostedFileBase Image)
		{
			if (ModelState.IsValid)
			{
				user.Password = Encryptor.MD5Hash(user.Password);
				if (Image != null)
				{
					string pic = Path.GetFileName(Image.FileName);
					string path = Path.Combine(Server.MapPath("~/Contents/Uploads"), pic);
					// file is uploaded
					Image.SaveAs(path);
					user.Image = "/Contents/Uploads/" + Image.FileName;
				}
				
				var result = userService.Insert(user);
				if (result > 0)
				{
					TempData["message"] = "Added";
					TempData["DataSuccess"] = "Đăng ký thành công";
				}
				else if (result == -1)
				{
					ModelState.AddModelError("Username", "Tài Khoản Đã Tồn Tại");
					return View();
				}
				else if (result == -2)
				{
					ModelState.AddModelError("Email", "Email Đã Tồn Tại");
					return View();
				}
				else if (result == -3)
				{
					ModelState.AddModelError("Phone", "Số điện thoại Đã Tồn Tại");
					return View();
				}
				else
				{
					TempData["message"] = "false";
				}
				return RedirectToAction("Index","Home");
			}
			return View();
			
		}
		
		public ActionResult UpdateUser()
		{
			if (Session["User"] != null)
			{
				var currentUser = (User)Session["User"];
				var user = userService.GetById(currentUser.UserId);
				return View(user);
			}
			return View();
		}
		[HttpPost]
		public ActionResult UpdateUser(User u, HttpPostedFileBase Image)
		{
			if (Session["User"] != null)
			{
				var currentUser = (User)Session["User"];
				if (Image != null)
				{
					string pic = Path.GetFileName(Image.FileName);
					string path = Path.Combine(Server.MapPath("~/Contents/Uploads"), pic);
					// file is uploaded
					Image.SaveAs(path);
					u.Image = "/Contents/Uploads/" + Image.FileName;
				}
				u.UserId = currentUser.UserId;
				var result = loginService.UpdateUserCustomer(u);
				if (result > 0)
				{
					TempData["message"] = "Added";
					TempData["DataSuccess"] = "Sửa thông tin thành công";
				}
				else if (result == -2)
				{
					ModelState.AddModelError("Email", "Email Đã Tồn Tại");
					return View();
				}
				else if (result == -3)
				{
					ModelState.AddModelError("Phone", "Số điện thoại Đã Tồn Tại");
					return View();
				}
				else
				{
					TempData["message"] = "false";
				}
				return RedirectToAction("Index", "Home");
			}
			return View();
			
		}
	}
}