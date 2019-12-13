using Model;
using Model.ViewModel;
using PagedList;
using Repository.DAL;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class OrderDetailRepository :  IOrderDetailRepository
	{
		private DBEntityContext context;
		public OrderDetailRepository(DBEntityContext context)
		{
			this.context = context;
		}

		public IList<OrderDetail> GetListOrderDetailById(int id)
		{
			var result = context.OrderDetails.Where(x=>x.OrderId == id).ToList();
			return result;
		}

		public IEnumerable<Order> GetListOrderById(int userId, int Page, int Pagesize)
		{
			var model = context.Orders.Where(x => x.UserId == userId);
			return model.OrderByDescending(x => x.Created).ToPagedList(Page, Pagesize);
		}

		public Order GetOrderById(string verifyCode)
		{
			return context.Orders.FirstOrDefault(x => x.VerifyCode == verifyCode);
		}

        public IEnumerable<OrderDetail> GetAll()
        {
            return context.OrderDetails;
        }

        public int Inserts(Order order, List<OrderDetail> orderDetails)
		{
			using (var transaction = context.Database.BeginTransaction())
			{
				try
				{
					order.Created = DateTime.Now;
					context.Orders.Add(order);
					context.SaveChanges();
					var firstOrder = context.Orders.OrderByDescending(x => x.Created).FirstOrDefault();
					foreach (var item in orderDetails)
					{
						item.OrderId = firstOrder.ID;
						context.OrderDetails.Add(item);
						context.SaveChanges();
					}


					transaction.Commit();
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					return 0;
				}
			}
			return 1;
		}

		public int Update(OrderDetail t)
		{
			context.Entry(t).State = System.Data.Entity.EntityState.Modified;
			return context.SaveChanges();
		}
	}
}
