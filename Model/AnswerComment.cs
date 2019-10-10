using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class AnswerComment
	{
		public int Id { get; set; }
		public int CommentId { get; set; }
		public string Content { get; set; }
		public DateTime CreatedDate { get; set; }
		public int Status { get; set; }
		[ForeignKey("CommentId")]
		public virtual Comment Comment { get; set; }
	}
}
