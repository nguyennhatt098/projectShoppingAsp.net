using System;
using System.Linq;
using System.Web.Mvc;
using Model;
using Service;
using ShopingCart.Common;

namespace ShopingCart.Areas.Admin.Controllers
{
    public class OrdersController : BaseController
    {
		private OrderService _orderService;
		private OrderDetailService _orderDetailService;
		private NotifyService _notifyService;
		public OrdersController()
		{
			_orderService = new OrderService();
			_orderDetailService = new OrderDetailService();
			_notifyService=new NotifyService();
		}

		// GET: Admin/Orders
		[HasCredential(ActionId = 35)]
		public ActionResult Index(string searchString, int Page = 1, int PageSize = 10)
        {
	        ViewBag.searchString = searchString;
			return View(_orderService.Search(searchString,Page,PageSize));
        }
		[HttpPost]
		[HasCredential(ActionId = 36)]
		public ActionResult Details(Order order)
		{
			int result=0;
			string randomLink = Guid.NewGuid().ToString();
			if (order.Status == 3)
            {
                var orderDetail = _orderService.GetById(order.ID).OdersDetail.ToList();
				var notify = new Notify
				{
					Status = 1,
					Content = "Đơn hàng mã số " +order.ID+" đã được giao.Bạn có thể đánh giá về sản phẩm",
					CreatedDate = DateTime.Now,
					UserId = order.UserId,
					Link = "/Order/ReViewOrder/" + randomLink,
                    Image = orderDetail[0].Product.Images
				};

				var rs=_notifyService.Insert(notify);
				if (rs > 0)
				{
					order.VerifyCode = randomLink;
					result= _orderService.Update(order);
				}
			}
			else
			{
				result = _orderService.Update(order);
			}
			
			if (result > 0)
			{
				TempData["message"] = "Added";
			}
			else
			{
				TempData["message"] = "false";
			}
			return RedirectToAction("Details");
		}
		// GET: Admin/Orders/Details/5
		[HasCredential(ActionId = 36)]
		public ActionResult Details(int id)
        {  
           var orderDetail = _orderDetailService.GetListOrderDetailById(id);
			ViewBag.Order = _orderService.GetById(orderDetail[0].OrderId);
			return View(orderDetail);
        }
    }
}
