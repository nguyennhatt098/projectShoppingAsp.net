﻿using System.Collections.Generic;
using Repository.DAL;
using Model;
using Repository;
using Repository.Interface;
using Service.Interface;

namespace Service
{
	public class LoginService : ILoginService
	{
		private ILoginRepository repository;
		public LoginService()
		{
			repository = new LoginRepository(new DBEntityContext());
		}
		public List<int> GetListAction(string userName)
		{
			return repository.GetListAction(userName);
		}

		public int AddUser(User user)
		{
			return repository.AddUser(user);
		}

		public bool Login(string username, string password)
		{
			return repository.Login(username, password);
		}

		public User GetByUserName(string UserName)
		{
			return repository.GetByUserName(UserName);
		}
	}
}
