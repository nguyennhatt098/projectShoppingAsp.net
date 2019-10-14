using Model;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
	public interface ICommentService
	{
		int Insert(Comment t);
		IEnumerable<CommentDTO> Search(int productId, int Page, int Pagesize);
		int Count(int productId);
		int InsertAnswer(AnswerComment item);
		IEnumerable<AnswerComment> answerComments(int commentId);
	}
}
