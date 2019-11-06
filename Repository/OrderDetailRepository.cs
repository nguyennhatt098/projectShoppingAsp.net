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
    public class OrderDetailRepository :  IOrderDetailRepository,IRepository<OrderDetail>
	{
		private DBEntityContext context;
		public OrderDetailRepository(DBEntityContext context)
		{
			this.context = context;
		}

		public int Delete(int id)
		{
			throw new NotImplementedException();
		}

		public IList<OrderDetailDTO> GetAll(int id)
		{
			var result = (from c in context.OrderDetails
						  join p in context.Products on c.ProductId equals p.Id
						  where c.OrderId == id
						  select new OrderDetailDTO
						  {
							  Images = p.Images,
							  NameProduct = p.Name,
							  Oder_ID = c.OrderId,
							  Price = c.Price,
							  Quantity = c.Quantity,
							  Product_Id = p.Id,
						  }).ToList();
		
			return result;
		}

		public IEnumerable<OrderDetail> GetAll()
		{
			throw new NotImplementedException();
		}

		public OrderDetail GetById(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Order> GetListOrderById(int userId, int Page, int Pagesize)
		{
			var model = context.Orders.Where(x => x.UserId == userId);
			return model.OrderByDescending(x => x.Created).ToPagedList(Page, Pagesize);
		}

		public int Insert(OrderDetail t)
		{
			context.OrderDetails.Add(t);
			return context.SaveChanges();
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

		public IEnumerable<OrderDetail> Search(string searchString, int Page, int Pagesize)
		{
			throw new NotImplementedException();
		}

		public int Update(OrderDetail t)
		{
			context.Entry(t).State = System.Data.Entity.EntityState.Modified;
			return context.SaveChanges();
		}
	}
}
