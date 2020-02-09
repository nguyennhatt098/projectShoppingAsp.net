using Model;
using Model.ViewModel;
using Repository;
using Repository.DAL;
using Repository.Interface;
using System.Collections.Generic;

namespace Service
{
    public class CommentService : ICommentRepository
	{
		private ICommentRepository repository;
		public CommentService()
		{
			repository = new CommentRepository(new DBEntityContext());
		}

		public IEnumerable<AnswerComment> answerComments()
		{
			return repository.answerComments();
		}

		public IEnumerable<CategoryDTO> Categories()
		{
			return repository.Categories();
		}

		public int Count(int productId)
		{
			return repository.Count(productId);
		}

		public int Insert(Comment t)
		{
			return repository.Insert(t);
		}

		public int InsertAnswer(AnswerComment item)
		{
			return repository.InsertAnswer(item);
		}

		public SearchResponse<CommentViewModel> Search(int productId, int Page, int Pagesize)
		{
			return repository.Search(productId, Page, Pagesize);
		}
	}
}
