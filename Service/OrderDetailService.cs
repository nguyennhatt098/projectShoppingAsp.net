using Repository.DAL;
using Model;
using Model.ViewModel;
using Repository;
using Service.Interface;
using System.Collections.Generic;

namespace Service
{
	public class OrderDetailService : IOrderDetailService
	{
		private OrderDetailRepository repository;
		public OrderDetailService()
		{
			repository = new OrderDetailRepository(new DBEntityContext());
		}
		public IList<OrderDetail> GetListOrderDetailById(int id)
		{
			return repository.GetListOrderDetailById(id);
		}

		public IEnumerable<Order> GetListOrderById(int userId, int Page, int Pagesize)
		{
			return repository.GetListOrderById(userId, Page, Pagesize);
		}

		public Order GetOrderById(string verifyCode)
		{
			return repository.GetOrderById(verifyCode);
		}

		public int Inserts(Order order, List<OrderDetail> orderDetails)
		{
			return repository.Inserts(order, orderDetails);
		}

		public int Update(OrderDetail t)
		{
			return repository.Update(t);
		}

		
	}
}
