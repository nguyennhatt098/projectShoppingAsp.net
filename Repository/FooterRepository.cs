using Model;
using Repository.DAL;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class FooterRepository : IRepository<Footer>
	{
		private DBEntityContext context;
		public FooterRepository(DBEntityContext context)
		{
			this.context =context;
		}
		public int Delete(int id)
		{
			context.Footers.Remove(GetById(id));
			return context.SaveChanges();
		}

		public IEnumerable<Footer> GetAll()
		{
			return context.Footers.ToList();
		}

		public Footer GetById(int id)
		{
			return context.Footers.Find(id);
		}

		public int Insert(Footer t)
		{
			context.Footers.Add(t);
			return context.SaveChanges();
		}

		public IEnumerable<Footer> Search(string searchString, int Page, int Pagesize)
		{
			throw new NotImplementedException();
		}

		public int Update(Footer t)
		{
			context.Entry(t).State = System.Data.Entity.EntityState.Modified;
			return context.SaveChanges();
		}
	}
}
