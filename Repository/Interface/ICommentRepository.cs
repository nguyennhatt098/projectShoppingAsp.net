using Model;
using Model.ViewModel;
using System.Collections.Generic;

namespace Repository.Interface
{
	public interface ICommentRepository
	{
		int Insert(Comment t);
		IEnumerable<Comment> Search(int productId, int Page, int Pagesize);
		int Count(int productId);
		int InsertAnswer(AnswerComment item);
		IEnumerable<AnswerComment> answerComments();
		IEnumerable<CategoryDTO> Categories();
	}
}
