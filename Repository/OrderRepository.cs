﻿using Model;
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

		public int Insert(Order t)
		{
			context.Orders.Add(t);
			return context.SaveChanges();
		}

		public IEnumerable<Order> Search(string searchString, int Page, int Pagesize)
		{
			var model = context.Orders.ToList();
			if (!string.IsNullOrWhiteSpace(searchString))
			{
				model = model.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
			}
			return model.OrderByDescending(x => x.ID).ToPagedList(Page, Pagesize);
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
			currentItem.VerifyCode = t.VerifyCode;
			context.Entry(currentItem).State = System.Data.Entity.EntityState.Modified;
			return context.SaveChanges();
		}
	}
}
