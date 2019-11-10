using Model;
using Repository.DAL;
using Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
	public class NotifyRepository : INotify
	{
		private DBEntityContext context;
		public NotifyRepository(DBEntityContext context)
		{
			this.context = context;
		}

		public IEnumerable<Notify> GetById(int id)
		{
			return context.Notifies.OrderByDescending(x => x.CreatedDate).Take(10).ToList();
		}

		public Notify GetNotifyByLink(string verifyCode)
		{
			var list= context.Notifies.ToList();
			foreach (var item in list)
			{
				var verifyLink = item.Link.Split('/');
				if (verifyLink.Count() > 3 &&verifyLink[3].Equals(verifyCode))
				{
					context.Notifies.Remove(context.Notifies.FirstOrDefault(x => x.Id == item.Id));
					context.SaveChanges();
					return item;
				}
			}
			return null;
		}

		public int Insert(Notify item)
		{
			context.Notifies.Add(item);
			return context.SaveChanges();
		}
	}
}
