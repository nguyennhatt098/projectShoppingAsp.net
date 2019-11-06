using Model;
using Repository;
using Repository.DAL;
using Service.Interface;
using System;
using System.Collections.Generic;

namespace Service
{
    public class NewsService : IServices<News>
    {
        private NewsRepository repository;
        public NewsService()
        {
            repository = new NewsRepository(new DBEntityContext());
        }
        public int Delete(int id)
        {
            return repository.Delete(id);
        }

        public IEnumerable<News> GetAll()
        {
            return repository.GetAll();
        }

        public News GetById(int id)
        {
            return repository.GetById(id);
        }

        public int Insert(News t)
        {
            return repository.Insert(t);
        }

        public IEnumerable<News> Search(string searchString, int Page, int Pagesize)
        {
            throw new NotImplementedException();
        }

        public int Update(News t)
        {
            return repository.Update(t);
        }
    }
}
