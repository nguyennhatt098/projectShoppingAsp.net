using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
	public class AnswerComment
	{
		public int Id { get; set; }
		public int CommentId { get; set; }
		public int? UserId { get; set; }
		public string Content { get; set; }
		public DateTime CreatedDate { get; set; }
		public int Status { get; set; }
		[ForeignKey("CommentId")]
		public  Comment Comment { get; set; }
		[ForeignKey("UserId")]
		public virtual User User { get; set; }
	}
}
