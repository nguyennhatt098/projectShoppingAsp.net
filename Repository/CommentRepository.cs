
using Model;
using Model.ViewModel;
using Repository.DAL;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class CommentRepository : ICommentRepository
	{
		private DBEntityContext context;
		public CommentRepository(DBEntityContext context)
		{
			this.context = context;
		}

		public IEnumerable<AnswerComment> answerComments()
		{
			return context.Answers.OrderByDescending(x => x.CreatedDate).ToList();
		}

		public IEnumerable<CategoryDTO> Categories()
		{
			var list = (from c1 in context.Categories
						join c2 in context.Categories on c1.ParentID equals c2.ID
						select new CategoryDTO
						{
							ParentName = c2.Name,
							ID = c1.ID,
							CreatedDate = c1.CreatedDate,
							Slug = c1.Slug,
							Name = c1.Name,
							Status = c1.Status,
							ModifileDate = c1.ModifileDate
						}).ToList();
			var categories = context.Categories.ToList().Where(x => !list.Any(z => z.ID == x.ID)).AsEnumerable().Select(k => new CategoryDTO
			{
				ID = k.ID,
				Name = k.Name,
				ParentName = "",
				ModifileDate = k.CreatedDate,
				Slug = k.Slug,
				CreatedDate = k.CreatedDate,
				Status = k.Status
			}).ToList();
			list.AddRange(categories);
			return list;
		}

		public int Count(int productId)
		{
			using (DBEntityContext context = new DBEntityContext())
			{
				return context.Comments.Count(x => x.ProductId == productId);
			}
		}

		public int Insert(Comment t)
		{
			context.Comments.Add(t);
			return context.SaveChanges();
		}

		public int InsertAnswer(AnswerComment item)
		{
			context.Answers.Add(item);
			return context.SaveChanges();
		}

		public IEnumerable<CommentDTO> Search(int productId, int Page, int Pagesize)
		{
			var total = Page * Pagesize;
			if (total > Count(productId))
			{
				Pagesize = Count(productId) - (Pagesize * (Page - 1));
			}

			if (Pagesize <= 0)
			{
				Pagesize = 6;
			}
			var list = (from c in context.Comments
						join u in context.Users on c.UserId equals u.UserId
						where c.ProductId == productId
						select new CommentDTO
						{
							CommentId = c.Id,
							Question = c.Question,
							UserName = u.UserName,
							CreatedDate = c.CreatedDate,
							ModifyDate = c.ModifyDate
						}).OrderByDescending(x => x.CreatedDate).Skip((Page - 1) * Pagesize).Take(Pagesize).AsQueryable();
			return list;
		}
	}
}
