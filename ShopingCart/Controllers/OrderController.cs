using Model;
using Newtonsoft.Json;
using Service;
using ShopingCart.Common;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Action = System.Action;

namespace ShopingCart.Controllers
{
    public class OrderController : Controller
    {
		private UserService _userService;
		private OrderService _orderService;
		private OrderDetailService _orderDetailService;
		private ReviewProductService _reviewProductService;
		private NotifyService _notifyService;
		public OrderController()
		{
			_userService = new UserService();
			_orderService = new OrderService();
			_orderDetailService = new OrderDetailService();
			_reviewProductService = new ReviewProductService();
			_notifyService = new NotifyService();
		}
        // GET: Order
        public ActionResult Index()
        {
	        if (Session["User"] != null)
			{
				var currentUser =(User)Session["User"];
				var user =_userService.GetById(currentUser.UserId);
				ViewBag.User = user;
			}
            return View();
        }
		[HttpPost]
		public ActionResult Index(Order order)
		{
			if (Session["User"] != null)
			{
				var currentUser = (User)Session["User"];
				var user = _userService.GetById(currentUser.UserId);
				ViewBag.User = user;
			}

			if (!ModelState.IsValid) return View();
			{
				var currentUser = (User)Session["User"];
               
				var cart =(List<CartItem>) Session["CartSession"];
				var orderDetails = new List<OrderDetail>();
				double totalQuantity=0;
				foreach (var item in cart)
				{
					var currentPrice = (item.Product.Sale_Price != null && item.Product.Sale_Price < item.Product.Price)
						? float.Parse(item.Product.Sale_Price.ToString())
						: float.Parse(item.Product.Price.ToString());
					totalQuantity += item.Quantity * currentPrice;
					var orderDetail = new OrderDetail {
						Price =currentPrice,
						ProductId=item.Product.Id,
						Quantity=item.Quantity,
					
					};
					orderDetails.Add(orderDetail);
                }
				var result=	_orderDetailService.Inserts(order,orderDetails);
				if (result > 0)
				{
					string header = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Start/header.txt"));
					string footer = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Start/footer.txt"));
					string main = String.Format(@"<h2 class='title'>ĐƠN HÀNG Fashion</h2>
                <p>
					<b>Họ tên người nhận</b>
					<span>{0}</span>
				</p>
				<p>
					<b>SĐT</b>
					<span>{1}</span>
				</p>
				<p>
					<b>Địa chỉ</b>
					<span>{2}</span>
				</p>
				<p>
					<b>Ngày mua</b>
					<span>{3}</span>
				</p>
				<p>
					<b>Tổng tiền</b>
					<span>{4} VND</span>
				</p>", order.Name, order.Phone, order.Address, DateTime.Now, String.Format("{0:n0}", totalQuantity));
					main += @"<table class='table text-center'>
					<thead>
						<tr>
							<th>Sản phẩm</th>
							<th>Đơn giá</th>
							<th>Số lượng</th>
                            <th>Tổng Tiền </th>
						</tr>
					</thead>
					<tbody>";
					foreach (var item in cart)
					{
						var currentPrice = (item.Product.Sale_Price != null && item.Product.Sale_Price < item.Product.Price)
							? float.Parse(item.Product.Sale_Price.ToString())
							: float.Parse(item.Product.Price.ToString());
						main += "<tr>";
						main += "	<td>" + item.Product.Name + "</td>";
						main += "    <td>" + String.Format("{0:n0}", currentPrice) + " VND</td>";
						main += "    <td>" + item.Quantity + "</td>";
						main += "    <td>" + String.Format("{0:n0}", (item.Quantity * currentPrice)) + " VND </td>";
						main += "</tr>";
					}
					main += @"</tbody>
				</table>";
					HelpMail.SendEmail(currentUser.Email, "donking2510@gmail.com", "anhyeuem123", "[CASTAR]_Đơn hàng", header + main + footer);

					TempData["message"] = "Added";
					TempData["DataSuccess"] = "Đặt hàng thành công";
					Session["CartSession"] = null;
				}
				else
				{
					TempData["message"] = "false";
				}
				return RedirectToAction("Index","Home");
			}
		}

		public ActionResult OrderHistory(int Page = 1, int pageSize = 5)
		{
			if (Session["User"] == null) return View();
			var currentUser = (User)Session["User"];
			return View(_orderDetailService.GetListOrderById(currentUser.UserId, Page, pageSize));
		}
		
		public ActionResult ReviewOrder(string id)
		{
			var orderItem = _orderDetailService.GetOrderById(id);
			if (orderItem == null) return Redirect("/Home/Error404");
			ViewBag.OrderDetailList = _orderDetailService.GetListOrderDetailById(orderItem.ID);
			return View();
		}
		[HttpPost]
		public ActionResult InsertReviewOrder(string data)
		{
			var item = JsonConvert.DeserializeObject<List<ReviewProduct>>(data);
			var result = _reviewProductService.InsertMultipleReviewProduct(item);
			if (result > 0)
			{
				var orderItem = _orderService.GetById(item[0].OrderId);
				_notifyService.GetNotifyByLink(orderItem.VerifyCode);
				orderItem.VerifyCode = "";
				_orderService.Update(orderItem);
				TempData["message"] = "Added";
				TempData["DataSuccess"] = "Đánh giá thành công";
			}
			else
			{
				TempData["message"] = "false";
			}
			return Json(new
			{
				status = true
			});
		}

	}
}