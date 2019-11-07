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

		public int Insert(Notify item)
		{
			context.Notifies.Add(item);
			return context.SaveChanges();
		}
	}
}
