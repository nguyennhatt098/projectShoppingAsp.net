using Model;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using Repository.DAL;

namespace Repository
{
	public class SliderRepository : IRepository<Slider>, IDisposable
	{
		private DBEntityContext context;
		public SliderRepository(DBEntityContext context)
		{
			this.context = context;
		}
		public int Delete(int id)
		{
			var item = context.Sliders.SingleOrDefault(s => s.ID == id);
			if (item!=null&& item.Status == false)
			{
				context.Sliders.Remove(item);
				return context.SaveChanges();
			}
			return -1;
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Slider> GetAll()
		{
			return context.Sliders.Where(x => x.Status == true).OrderByDescending(x => x.DisplayOrder).Take(3).ToList();
		}

		public Slider GetById(int id)
		{
			return context.Sliders.FirstOrDefault(s => s.ID == id);
		}

		public int Insert(Slider t)
		{
			t.Created = DateTime.Now;
			context.Sliders.Add(t);
			return context.SaveChanges();
		}


		public IEnumerable<Slider> Search(string searchString, int Page, int Pagesize)
		{
			var model = context.Sliders.ToList();
			if (!string.IsNullOrEmpty(searchString))
			{
				model = model.Where(x => x.Description.Contains(searchString)).ToList();
			}
			return model.OrderByDescending(x => x.DisplayOrder).ToPagedList(Page, Pagesize);
		}

		public int Update(Slider t)
		{
			var currentItem = context.Sliders.Find(t.ID);
			if (currentItem == null) return -5;
			currentItem.ModifileDate = DateTime.Now;
			currentItem.Description = t.Description;
			currentItem.DisplayOrder = t.DisplayOrder;
			currentItem.Images = t.Images;
			currentItem.Link = t.Link;
			currentItem.Status = t.Status;
			context.Entry(currentItem).State = System.Data.Entity.EntityState.Modified;
			return context.SaveChanges();

		}
	}
}
