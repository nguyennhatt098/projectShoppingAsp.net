using System.Collections.Generic;
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
