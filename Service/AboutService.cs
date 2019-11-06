using System;
using System.Collections.Generic;
using Model;
using Repository;
using Repository.Interface;
using Service.Interface;
using Repository.DAL;

namespace Service
{
    public class AboutService : IServices<About>
	{
		private IRepository<About> repository;
		public AboutService()
		{
			repository = new AboutRepository(new DBEntityContext());
		}
		public IEnumerable<About> GetAll()
		{
			return repository.GetAll();
		}

		public IEnumerable<About> Search(string searchString, int Page, int Pagesize)
		{
			throw new NotImplementedException();
		}

		public int Insert(About t)
		{
			return repository.Insert(t);
		}

		public int Update(About t)
		{
			return repository.Update(t);
		}

		public int Delete(int id)
		{
			return repository.Delete(id);
		}

		public About GetById(int id)
		{
			return repository.GetById(id);
		}
	}
}
