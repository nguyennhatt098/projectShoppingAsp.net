using System;
using Service.Interface;
using System.Collections.Generic;
using Model;
using Repository.Interface;
using Repository;
using Repository.DAL;

namespace Service
{
    public class UserService : IServices<User>
	{
		private IRepository<User> repository;
		public UserService()
		{
			repository = new UserRepository(new DBEntityContext());
		}

		public int Delete(int id)
		{
			return repository.Delete(id);
		}



		public IEnumerable<User> GetAll()
		{
			return repository.GetAll();
		}

		public User GetById(int id)
		{
			return repository.GetById(id);
		}

		public int Insert(User t)
		{
			return repository.Insert(t);
		}

		public IEnumerable<User> Search(string searchString, int Page, int Pagesize)
		{
			return repository.Search(searchString, Page, Pagesize);
		}

		public int Update(User t)
		{
			return repository.Update(t);
		}
	}
}
