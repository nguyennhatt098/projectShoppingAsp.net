
using Model;
using Repository.DAL;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class WishListRepository : IWishListReponsitory<WishList>, IDisposable
    {
        private DBEntityContext context;
        public WishListRepository(DBEntityContext context)
        {
            this.context = context;
        }
      

        public IEnumerable<WishList> GetById(int id)
        {
	        return context.wishLists.Where(x => x.UserID.Equals(id)).ToList();
        }

        public int AddMultiple(List<WishList> items)
        {
	        context.wishLists.AddRange(items);
	        return context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<WishList> GetAll()
        {
            return context.wishLists.ToList();
        }

        public int Insert(WishList t)
        {
            context.wishLists.Add(t);
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

		public int Delete(WishList item)
		{
			var obj = context.wishLists.FirstOrDefault(c => c.UserID == item.UserID&& c.ProductID==item.ProductID);
			if(obj!=null) context.wishLists.Remove(obj);
			return context.SaveChanges();
		}
	}
}
