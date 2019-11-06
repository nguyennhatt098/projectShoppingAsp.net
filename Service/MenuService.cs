using Repository.DAL;
using Model;
using Repository;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;

namespace Service
{
    public class MenuService : IServices<Menu>
    {
        private IRepository<Menu> repository;
        public MenuService()
        {
            repository = new MenuRepository(new DBEntityContext());
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Menu> GetAll()
        {
            return repository.GetAll();
        }

        public Menu GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Menu t)
        {
            throw new NotImplementedException();
        }

		public IEnumerable<Menu> Search(string searchString, int Page, int Pagesize)
		{
			throw new NotImplementedException();
		}

		public int Update(Menu t)
        {
            throw new NotImplementedException();
        }
    }
}
