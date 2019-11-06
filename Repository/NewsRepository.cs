using Model;
using Repository.DAL;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
	public class NewsRepository : IRepository<News>, IDisposable
	{
		private DBEntityContext context;
		public NewsRepository(DBEntityContext context)
		{
			this.context = context;
		}

		public int Delete(int id)
		{
			var item = context.News.SingleOrDefault(x => x.ID == id);
			if (item == null) return 0;
			context.News.Remove(item);
			return context.SaveChanges();
		}

		public IEnumerable<News> GetAll()
		{
			return context.News.OrderByDescending(x=>x.CreateDate).ToList();
		}

		public News GetById(int id)
		{
			return context.News.SingleOrDefault(x => x.ID == id);
		}

		public int Insert(News t)
		{
			t.CreateDate = DateTime.Now;
			context.News.Add(t);
			return context.SaveChanges();

		}

		public IEnumerable<News> Search(string searchString, int Page, int Pagesize)
		{
			throw new NotImplementedException();
		}

		public int Update(News t)
		{

			context.Entry(t).State = System.Data.Entity.EntityState.Modified;
			return context.SaveChanges();
		}
		private bool disposed = false;
		public void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					context.Dispose();
				}
			}
			this.disposed = true;
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
