using Model;
using Service;
using ShopingCart.Common;
using System.Web.Mvc;

namespace ShopingCart.Areas.Admin.Controllers
{
	public class LoginController : Controller
	{
		private LoginService loginService;
        private RoleService roleService;
		public LoginController()
		{
			roleService=new RoleService();
			loginService = new LoginService();
        }

		// GET: Admin/Login
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Login(LoginModel model)
		{
            if (!ModelState.IsValid) return View("Index");
            var res = loginService.Login(model.UserName, Encryptor.MD5Hash(model.Password));
            if (res)
            {
                var user = loginService.GetByUserName(model.UserName);
                if (!user.Status)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn hiện đang bị khóa");
                    return View("Index"); 
                }

                if (roleService.GetById(user.RoleId).RoleName.ToLower().Equals("member"))
                {
                    ModelState.AddModelError("", "Tài khoản của bạn không có quyền truy cập vào trang admin");
                    return View("Index");
                }
                Session.Add(CommonConstants.SESSION_CREDENTIALS, loginService.GetListAction(user.UserName));
                var userSession = new LoginModel
                {
                    UserName = user.UserName,
                };
                Session.Add(CommonConstants.USER_SESSION, user);

                return RedirectToAction("Index", "HomeAdmin");
            }

            ModelState.AddModelError("", "Login sai");
            return View("Index");
		}
		public ActionResult LogOut()
		{
			Session[CommonConstants.USER_SESSION] = null;
			return Redirect("/Admin/Login");
		}
		
	}
}