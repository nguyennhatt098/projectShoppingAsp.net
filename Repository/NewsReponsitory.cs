using Model;
using Repository.DAL;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
	public class NewsReponsitory : IRepository<News>, IDisposable
	{
		private DBEntityContext context;
		public NewsReponsitory(DBEntityContext context)
		{
			this.context = context;
		}

		public int Delete(int id)
		{
			var item = context.News.Where(x => x.ID == id).SingleOrDefault();
			context.News.Remove(item);
			return context.SaveChanges();
		}



		public IEnumerable<News> GetAll()
		{
			return context.News.ToList();
		}

		public News GetById(int id)
		{
			return context.News.Where(x => x.ID == id).SingleOrDefault();
		}

		public News GetByUserName(string UserName)
		{
			throw new NotImplementedException();
		}

		public Contact GetContact()
		{
			throw new NotImplementedException();
		}

		public int Insert(News t)
		{
			t.CreateDate = DateTime.Now;
			context.News.Add(t);
			return context.SaveChanges();

		}

		public bool Login(string username, string password)
		{
			throw new NotImplementedException();
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
