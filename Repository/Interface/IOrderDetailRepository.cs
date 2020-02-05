using Model;
using System.Collections.Generic;

namespace Repository.Interface
{
	public interface IOrderDetailRepository
	{
		IList<OrderDetail> GetListOrderDetailById(int id);
		int Update(OrderDetail t);
		int Inserts(Order order, List<OrderDetail> orderDetails);
		IEnumerable<Order> GetListOrderById(int userId, int Page, int Pagesize);
		Order GetOrderById(string verifyCode);
        IEnumerable<OrderDetail> GetAll();
    }
}
