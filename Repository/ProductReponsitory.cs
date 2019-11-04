
using Model;
using PagedList;
using Repository.DAL;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Repository
{
	public class ProductReponsitory : IRepository<Product>, IDisposable, IListProduct<Product>
	{
		private DBEntityContext context;
		public ProductReponsitory(DBEntityContext context)
		{
			this.context = context;
		}
		public int Delete(int id)
		{
			var orderDetailList = context.OrderDetails.ToList();

			if (orderDetailList.Any(x => x.Product_Id == id)) return -1;

			var item = context.Products.FirstOrDefault(c => c.Id == id);

			if (item != null && item.Status == false)
			{
				context.Products.Remove(item);
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

		public IEnumerable<Product> GetAll()
		{
			return context.Products.ToList();
		}

		public Product GetById(int id)
		{
			return context.Products.SingleOrDefault(s => s.Id == id);
		}

		public Product GetByUserName(string UserName)
		{
			throw new NotImplementedException();
		}

		public int Insert(Product product)
		{
			var productList = context.Products.ToList();
			if (productList.Any(x => x.Name.ToLower().Equals(product.Name.ToLower()))) return -2;
			product.Created = DateTime.Now;
			context.Products.Add(product);
			return context.SaveChanges();
		}

		public bool Login(string username, string password)
		{
			throw new NotImplementedException();
		}

		public int Update(Product t)
		{
			var productList = context.Products.ToList();
			var currentItem = context.Products.Find(t.Id);
			productList.Remove(currentItem);
			if (productList.Any(x => x.Name.ToLower().Equals(t.Name.ToLower()))) return -2;
			if (currentItem != null)
			{
				currentItem.ModifileDate = DateTime.Now;
				currentItem.Images = t.Images;
				currentItem.MoreImages = t.MoreImages;
				currentItem.Name = t.Name;
				currentItem.Price = t.Price;
				currentItem.Sale_Price = t.Sale_Price;
				currentItem.Slug = t.Slug;
				currentItem.Status = t.Status;
				currentItem.TopHot = t.TopHot;
				currentItem.Content = t.Content;
				currentItem.Category_ID = t.Category_ID;
				context.Entry(currentItem).State = System.Data.Entity.EntityState.Modified;
			}

			return context.SaveChanges();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public IEnumerable<Product> ListProductHot()
		{

			return context.Products.Where(s => s.TopHot == true && s.Status == true).OrderByDescending(s => s.Created).Take(8).ToList();
		}
		
		public IEnumerable<Product> ListProductSale()
		{
			var randomItem = context.Products.OrderBy(x => Guid.NewGuid()).Take(4).ToList();
			return randomItem;
		}


		public IEnumerable<Product> ListProductNew()
		{
			return context.Products.Where(s => s.Status).OrderByDescending(s => s.Created).Take(8).ToList();
		}

		public IEnumerable<Product> Search(string searchString, int Page, int Pagesize)
		{
			var model = context.Products.ToList();
			if (!string.IsNullOrWhiteSpace(searchString))
			{
				model = model.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
			}
			return model.OrderByDescending(x => x.Created).ToPagedList(Page, Pagesize);
		}

		public Contact GetContact()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Product> ListProductGetByCategory(int id, int pageIndex, int pageSize)
		{
			var total =pageIndex*pageSize;
			if (total > Count(id))
			{
				pageSize = Count(id) - (pageSize * (pageIndex-1));
			}

			if (pageSize <= 0)
			{
				pageSize = 6;
			}
			return context.Products.Where(x => x.Category_ID == id).OrderByDescending(x => x.Created).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsQueryable();
		}

		public int Count(int id)
		{
			return context.Products.Count(x => x.Category_ID==id);
		}

		public IEnumerable<Category> Categories()
		{
			var listLv2 = (from c1 in context.Categories
					join c2 in context.Categories on c1.ParentID equals c2.ID
					select c1
				).ToList();
			var listLv3 = (from c1 in context.Categories
					join c2 in context.Categories on c1.ParentID equals c2.ID
					join c3 in context.Categories on c2.ParentID equals c3.ID
					select c1
				).ToList();
			var listLv23 = (from c1 in listLv2
							join c2 in listLv3 on c1.ID equals c2.ParentID
					select c1
				).ToList();
			foreach (var item in listLv23)
			{
				listLv2.Remove(item);
			}
			return listLv2;
		}
	}
}
