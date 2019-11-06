using Model;
using Repository.DAL;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			TimeSpan duration = new TimeSpan(30, 0, 0, 0);
			var calculateDate = DateTime.Now.Subtract(duration);
			return context.Notifies.OrderByDescending(x => x.CreatedDate).Where(c => c.UserId.Equals(id) && c.CreatedDate>calculateDate).ToList();
		}
	}
}
