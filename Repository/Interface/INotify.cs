using Model;
using System.Collections.Generic;

namespace Repository.Interface
{
	public interface INotify
	{
		IEnumerable<Notify> GetById(int id);
		int Insert(Notify item);
		Notify GetNotifyByLink(string verifyCode);
	}
}
