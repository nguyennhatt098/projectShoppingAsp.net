using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
	public class CommentViewModel
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int ProductId { get; set; }
		public string Question { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifyDate { get; set; }
		public bool Status { get; set; }
		public  User User { get; set; }
		public Product Product { get; set; }
		public  ICollection<AnswerComment> Answers { get; set; }
	}
}
