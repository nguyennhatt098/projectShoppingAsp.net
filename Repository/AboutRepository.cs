using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Model;
using Repository.Interface;
using Repository.DAL;

namespace Repository
{
	public class AboutRepository:IRepository<About>
	{
		private DBEntityContext context;
		public AboutRepository(DBEntityContext context)
		{
			this.context = context;
		}
		public IEnumerable<About> GetAll()
		{
			return context.Abouts.OrderByDescending(x=>x.CreatedDate).ToList();
		}

		public IEnumerable<About> Search(string searchString, int Page, int Pagesize)
		{
			throw new NotImplementedException();
		}

		public int Insert(About t)
		{
			t.CreatedDate=DateTime.Now;
			 context.Abouts.Add(t);
			 return context.SaveChanges();
		}

		public int Update(About t)
		{
			var currentItem = GetById(t.ID);
			if (currentItem != null)
			{
				currentItem.ModifileDate=DateTime.Now;
				context.Entry(t).State = EntityState.Modified;
			}
			
			return context.SaveChanges();
		}

		public int Delete(int id)
		{
			context.Abouts.Remove(GetById(id));
			return context.SaveChanges();
		}

		public About GetById(int id)
		{
			return context.Abouts.Find(id);
		}
	}
}
