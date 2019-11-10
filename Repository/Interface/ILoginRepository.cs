﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.Interface
{
	public interface ILoginRepository
	{
		List<int> GetListAction(string userName);
		int AddUser(User user);
		bool Login(string username, string password);
		User GetByUserName(string UserName);
		int UpdateUserCustomer(User user);
	}
}
