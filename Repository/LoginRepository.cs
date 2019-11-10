using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Repository.Interface;
using Repository.DAL;

namespace Repository
{
    public class LoginRepository : ILoginRepository
	{
		private DBEntityContext context;
		public LoginRepository(DBEntityContext context)
		{
			this.context = context;
		}
		public List<int> GetListAction(string userName)
		{
			var user = context.Users.Single(x => x.UserName == userName);
			var data = (from a in context.RoleActions
						join b in context.Roles on a.RoleId equals b.RoleId
						join c in context.Actions on a.ActionId equals c.ActionId
						where b.RoleId == user.RoleId
						select new
						{
							a.RoleId, a.ActionId
						}).AsEnumerable().Select(x => new Model.RoleAction()
						{
							RoleId = x.RoleId,
							ActionId = x.ActionId
						});

			return data.Select(x => x.ActionId).ToList();
		}

		public int AddUser(User user)
		{
			var userList = context.Users.ToList();
			if (userList.Any(x => x.UserName.ToLower().Equals(user.UserName.ToLower()))) return -1;

			if (userList.Any(x => x.Email.ToLower().Equals(user.Email.ToLower()))) return -2;

			if (userList.Any(x => x.Phone.Equals(user.Phone))) return -3;

			var currentUser = new User
				{
					RoleId = user.RoleId,
					Password = user.Password,
					UserName = user.UserName,
					Address = user.Address,
					CreatedDate = DateTime.Now,
					Email = user.Email,
					FullName = user.FullName,
					Phone = user.Phone,
					Status = true,
					ConfirmPassword = user.Password,
				};
				context.Users.Add(currentUser);
				return context.SaveChanges();
		}

		public bool Login(string username, string password)
		{
			var res = context.Users.Count(s => s.UserName == username && s.Password == password);
			return res > 0;
		}

		public User GetByUserName(string UserName)
		{
			return context.Users.FirstOrDefault(s => s.UserName == UserName);
		}

		public int UpdateUserCustomer(User user)
		{
			var userList = context.Users.ToList();
			var currentItem = context.Users.Find(user.UserId);
			userList.Remove(currentItem);

			if (userList.Any(x => x.Email.ToLower().Equals(user.Email.ToLower()))) return -2;

			if (userList.Any(x => x.Phone.Equals(user.Phone))) return -3;

			if (currentItem != null)
			{
				currentItem.EditedDate = DateTime.Now;
				currentItem.Address = user.Address;
				currentItem.Email = user.Email;
				currentItem.Phone = user.Phone;
				currentItem.FullName = user.FullName;
				currentItem.Image = user.Image;
				currentItem.Gender = user.Gender;
				currentItem.ConfirmPassword = currentItem.Password;
			}
			context.Entry(currentItem).State = System.Data.Entity.EntityState.Modified;
			return context.SaveChanges();
		}
	}
}
