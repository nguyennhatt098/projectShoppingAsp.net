using Model;
using Repository.DAL;
using Repository.Interface;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class FeedBackReponsitory : IRepository<FeedBack>, IDisposable
    {
        private DBEntityContext context;
        public FeedBackReponsitory(DBEntityContext context)
        {
            this.context = context;
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FeedBack> GetAll()
        {
            throw new NotImplementedException();
        }

        public FeedBack GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(FeedBack t)
        {
            context.FeedBacks.Add(t);
            return context.SaveChanges();
        }

        public IEnumerable<FeedBack> Search(string searchString, int Page, int Pagesize)
        {
            throw new NotImplementedException();
        }

        public int Update(FeedBack t)
        {
            throw new NotImplementedException();
        }
    }
}
