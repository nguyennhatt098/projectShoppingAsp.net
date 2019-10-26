using BotDetect.Web.Mvc;
using Model;
using Model.ViewModel;
using Repository.DAL;
using Service;
using ShopingCart.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace ShopingCart.Controllers
{
    public class LoginController : Controller
    {
		private UserService _userService;
		private WishListService wishListService;
		public LoginController()
		{
			_userService = new UserService();
			wishListService=new WishListService();
		}


        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [CaptchaValidationActionFilter("CaptchaCode", "registerCaptcha", "Mã Xác Nhận Không Đúng!")]
        public ActionResult Register(RegisterUser r)
        {
            if (ModelState.IsValid)
            {
                User u = new User();
    
                u.UserName = r.UserName;
                u.FullName = r.FullName;
                u.Email = r.Email;
                u.Phone = r.Phone;
                u.Password = Encryptor.MD5Hash(r.PassWord);
                u.Address = r.Address;
                _userService.Insert(u);
                return RedirectToAction("Login");

            }
            return View();
        }
		[HttpPost]
		public ActionResult Index(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				var res = _userService.Login(model.UserName, Encryptor.MD5Hash(model.Password));
				if (res)
				{
					var user = _userService.GetByUserName(model.UserName);
					if (!user.Status)
					{
						ModelState.AddModelError("", "Tài khoản của bạn hiện đang bị khóa");
						return View("Index");
					}

					if (Session[Common.CommonConstants.DATA_WISH] != null)
					{
						var wishList = new List<WishList>();
						var productList = (List<int>)Session[Common.CommonConstants.DATA_WISH];
						foreach (var item in productList)
						{
							wishList.Add(new WishList
							{
								UserID = user.UserId,
								ProductID = item
							});
						}

						var currentWishList = wishListService.GetById(user.UserId);
						var checkExist = wishList.Where(x => !currentWishList.Any(w => w.ProductID.Equals(x.ProductID))).ToList();
						Session[Common.CommonConstants.DATA_WISH] = null;
						wishListService.AddMutiple(checkExist);
					}
					Session["User"] = user;
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Login sai");
				}
			}
			return View("Index");
		}
		public ActionResult LogOut()
		{
			Session["User"] = null;
			return Redirect("/");
		}
        public ActionResult ForgotPassWord()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassWord(string EmailAddress)
        {
            string mesage = "";
            bool status = false;
            using (DBEntityContext db = new DBEntityContext())
            {
                var acc = db.Users.Where(x => x.Email == EmailAddress).FirstOrDefault();
                if (acc!=null)
                {
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(acc.Email, resetCode, "ResetPassword");
                    acc.RessetPasswordCode = resetCode;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    mesage = "Chúng tôi đã gửi 1 đường link về tài khoản của bạn để đặt lại mật khẩu";
                }
                else
                {
                    mesage = "Không tìm thấy tài khoản";
                }
            }
            ViewBag.Message = mesage;
            return View();
        }
        [NonAction]
        public void SendVerificationLinkEmail(string EmailAddress, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/Login/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("nguyennhatt098@gmail.com", "Shop Fashe");
            var toEmail = new MailAddress(EmailAddress);
            var fromEmailPassword = "anhyeuem123"; // Replace with actual password

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Your account is successfully created!";
                body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                    " successfully created. Please click on the below link to verify your account" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/>br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a>";
            }


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Timeout = 10000,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
        public ActionResult ResetPassword(string id)
        {
            using(DBEntityContext db = new DBEntityContext())
            {
                var user = db.Users.Where(s => s.RessetPasswordCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPassWord model = new ResetPassWord();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPassWord model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (DBEntityContext db = new DBEntityContext())
                {
                    var user = db.Users.Where(s => s.RessetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user !=null)
                    {
                        user.Password = Encryptor.MD5Hash(model.NewPassword);
                        user.RessetPasswordCode = "";
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                        message = "Cập Nhập Mật Khẩu Thành Công";
                    }
                }
            }
            else
            {
                message = "Cập Nhập Mật Khẩu Thất Bại";
            }
            ViewBag.Message = message;
            return View(model);
        }


    }
}