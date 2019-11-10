using Model;
using Repository;
using Repository.DAL;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
	public class NotifyService : INotify
	{
		private NotifyRepository repository;
		public NotifyService()
		{
			repository = new NotifyRepository(new DBEntityContext());
		}

		public IEnumerable<Notify> GetById(int id)
		{
			return repository.GetById(id);
		}

		public Notify GetNotifyByLink(string verifyCode)
		{
			return repository.GetNotifyByLink(verifyCode);
		}

		public int Insert(Notify item)
		{
			return repository.Insert(item);
		}
	}
}
