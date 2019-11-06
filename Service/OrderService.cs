using Model;
using Repository;
using Repository.DAL;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;

namespace Service
{
    public class OrderService : IServices<Order>
	{
		private IRepository<Order> repository;
		public OrderService()
		{
			repository = new OrderRepository(new DBEntityContext());
		}
		public int Delete(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Order> GetAll()
		{
			return repository.GetAll();
		}

		public Order GetById(int id)
		{
			return repository.GetById(id);
		}

        public int Insert(Order t)
		{
			return repository.Insert(t);
		}

		public IEnumerable<Order> Search(string searchString, int Page, int Pagesize)
		{
			return repository.Search(searchString, Page, Pagesize);
		}

		public int Update(Order t)
		{
			return repository.Update(t);
		}
	}
}
