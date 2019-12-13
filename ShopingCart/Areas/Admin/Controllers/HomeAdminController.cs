using System.Linq;
using System.Web.Mvc;
using Service;

namespace ShopingCart.Areas.Admin.Controllers
{
    public class HomeAdminController : BaseController
    {
        private OrderService _orderService;
        private UserService _userService;
        private ProductService _productService;
        private OrderDetailService _orderDetailService;
        private WishListService _wishListService;
        public HomeAdminController()
        {
            _wishListService=new WishListService();
            _orderDetailService=new OrderDetailService();
            _orderService=new OrderService();
            _userService=new UserService();
            _productService=new ProductService();
        }
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.ProdutList = _productService.GetAll().Take(4).ToList();
            ViewBag.OrderDetailList = _orderDetailService.GetAll().Take(8).ToList();
            ViewBag.UserList = _userService.GetAll().Take(8).ToList();
            ViewBag.countUser = _userService.GetAll().Count();
            ViewBag.countOrder = _orderService.GetAll().Count();
            ViewBag.wishList = _wishListService.GetAll().Count();
            return View();
        }
        public ActionResult Error()
        {

	        return View();
        }

	}
}