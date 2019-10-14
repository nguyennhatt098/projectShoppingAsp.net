using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
using Repository.Interface;

namespace Repository
{
	public class CategoryRepository : IRepository<Category>, IDisposable
	{
		private DBEntityContext context;
		public CategoryRepository(DBEntityContext context)
		{
			this.context = context;
		}

		public int Delete(int id)
		{
			var productList = context.Products.ToList();
			var item = context.Categories.FirstOrDefault(c => c.ID == id);
			var items = !context.Categories.Any(x => x.ParentID == id);
			if (productList.Any(x => x.Category_ID == id)) return -1;

			if (items && item != null && !item.Status)
			{
				context.Categories.Remove(item);
				return context.SaveChanges();
			}
			return -1;
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



		public IEnumerable<Category> GetAll()
		{
			var listRemove = (from c1 in context.Categories
							   join c2 in context.Categories on c1.ParentID equals c2.ID
							   join c3 in context.Categories on c2.ParentID equals c3.ID
							   select c1
							  ).ToList();
			var list = context.Categories.ToList().Where(x => !listRemove.Any(z=>z.ID==x.ID));
			return list;
		}


		public Category GetById(int id)
		{
			return context.Categories.FirstOrDefault(c => c.ID == id);
		}

		public int Insert(Category t)
		{
			var categories = context.Categories.ToList();

			if (categories.Any(x => x.Name.ToLower().Equals(t.Name.ToLower()))) return -2;

			t.CreatedDate = DateTime.Now;
			context.Categories.Add(t);

			return context.SaveChanges();
		}

		

		public int Update(Category t)
		{
			var currentItem = context.Categories.Find(t.ID);
			var categories = context.Categories.ToList();
			categories.Remove(currentItem);

			if (categories.Any(x => x.Name.ToLower().Equals(t.Name.ToLower()))) return -2;
			if (currentItem != null)
			{
				currentItem.ModifileDate = DateTime.Now;
				currentItem.Name = t.Name;
				currentItem.Slug = t.Slug;
				currentItem.Status = t.Status;
			}

			context.Entry(currentItem).State = System.Data.Entity.EntityState.Modified;
			return context.SaveChanges();
		}

		public Category GetByUserName(string UserName)
		{
			throw new NotImplementedException();
		}

		public bool Login(string username, string password)
		{
			throw new NotImplementedException();
		}



		public IEnumerable<Category> Search(string searchString, int Page, int Pagesize)
		{
			return context.Categories.ToList();
		}

		public Contact GetContact()
		{
			throw new NotImplementedException();
		}
	}
}
