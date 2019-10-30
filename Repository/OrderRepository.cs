using Model;
using Repository.DAL;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;

namespace Repository
{
	public class OrderRepository : IRepository<Order>, IDisposable
	{
		private DBEntityContext context;
		public OrderRepository(DBEntityContext context)
		{
			this.context = context;
		}
		public int Delete(int id)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
		}

		public IEnumerable<Order> Filter(Order t)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Order> GetAll()
		{
			return context.Orders.OrderByDescending(x=>x.Created).ToList();
		}

		public Order GetById(int id)
		{
			return context.Orders.OrderByDescending(x=>x.Created).FirstOrDefault(x => x.ID == id);
		}

		public Order GetByUserName(string UserName)
		{
			throw new NotImplementedException();
		}

		public Contact GetContact()
		{
			throw new NotImplementedException();
		}

		public int Insert(Order t)
		{
			context.Orders.Add(t);
			return context.SaveChanges();
		}
		public bool Login(string username, string password)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Order> Search(string searchString, int Page, int Pagesize)
		{
			var model = context.Orders.ToList();
			if (!string.IsNullOrWhiteSpace(searchString))
			{
				model = model.Where(x => x.Name.Contains(searchString)).ToList();
			}
			return model.OrderByDescending(x => x.Created).ToPagedList(Page, Pagesize);
		}

		public int Update(Order t)
		{
			var currentItem = context.Orders.FirstOrDefault(s => s.ID == t.ID);
			if (currentItem == null) return 0;
			currentItem.ID = t.ID;
			currentItem.Name = t.Name;
			currentItem.Phone = t.Phone;
			currentItem.Status = t.Status;
			currentItem.Email = t.Email;
			currentItem.Created = t.Created;
			currentItem.Address = t.Address;
			if (t.Status == 3)
			{
				var notify = new Notify
				{
					Status = 1,
					Content = "Đơn hàng "+ currentItem.ID+ "của bạn đã được giao thành công! Vui lòng kiểm tra lại email",
					CreatedDate = DateTime.Now,
					UserId = currentItem.User_ID
				};
				context.Notifies.Add(notify);
				context.SaveChanges();
			}
			context.Entry(currentItem).State = System.Data.Entity.EntityState.Modified;
			return context.SaveChanges();
		}
	}
}
