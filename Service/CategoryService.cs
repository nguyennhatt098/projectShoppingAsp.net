using Model;
using Service.Interface;
using System.Collections.Generic;
using Repository;
using Repository.Interface;
using Repository.DAL;

namespace Service
{
	public class CategoryService : IServices<Category>
    {
        private IRepository<Category> repository;
        public CategoryService()
        {
            repository = new CategoryRepository(new DBEntityContext());
        }
        public int Delete(int id)
        {
            return repository.Delete(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return repository.GetAll();
        }

        public Category GetById(int id)
        {
            return repository.GetById(id);
        }

        public int Insert(Category t)
        {
            return repository.Insert(t);
        }

		public IEnumerable<Category> Search(string searchString, int Page, int Pagesize)
		{
			return repository.Search(searchString, Page, Pagesize);
		}

		public int Update(Category t)
        {
            return repository.Update(t);
        }
    }
}
