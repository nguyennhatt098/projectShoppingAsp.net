﻿using System;
using System.Collections.Generic;
using Model;
using Repository;
using Repository.Interface;
using Service.Interface;
using Repository.DAL;

namespace Service
{
    public	class RoleService:IServices<Role>
	{
		private IRepository<Role> repository;
		public RoleService()
		{
			repository = new RoleRepository(new DBEntityContext());
		}
		public IEnumerable<Role> GetAll()
		{
			return repository.GetAll();
		}

		public IEnumerable<Role> Search(string searchString, int Page, int Pagesize)
		{
			throw new NotImplementedException();
		}

		public int Insert(Role t)
		{
			return repository.Insert(t);
		}

		public int Update(Role t)
		{
			return repository.Update(t);
		}

		public int Delete(int id)
		{
			return repository.Delete(id);
		}

		public Role GetById(int id)
		{
			return repository.GetById(id);
		}
	}
}
