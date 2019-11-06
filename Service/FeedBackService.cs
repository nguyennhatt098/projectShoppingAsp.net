using Model;
using Repository;
using Repository.DAL;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;

namespace Service
{
    public class FeedBackService : IServices<FeedBack>
    {
        private IRepository<FeedBack> repository;
        public FeedBackService()
        {
            repository = new FeedBackReponsitory(new DBEntityContext());
        }
        public int Delete(int id)
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
            return repository.Insert(t);
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
