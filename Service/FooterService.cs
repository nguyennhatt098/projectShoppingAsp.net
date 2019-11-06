using Model;
using Repository;
using Repository.DAL;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;

namespace Service
{
    public class FooterService : IServices<Footer>
	{
		private IRepository<Footer> repository;
		public FooterService()
		{
			repository = new FooterRepository(new DBEntityContext());
		}
		public int Delete(int id)
		{
		return	repository.Delete(id);
		}

		public IEnumerable<Footer> GetAll()
		{
			return repository.GetAll();
		}

		public Footer GetById(int id)
		{
			return repository.GetById(id);
		}

		public int Insert(Footer t)
		{
			return repository.Insert(t);
		}

		public IEnumerable<Footer> Search(string searchString, int Page, int Pagesize)
		{
			throw new NotImplementedException();
		}

		public int Update(Footer t)
		{
			return repository.Update(t);
		}
	}
}
