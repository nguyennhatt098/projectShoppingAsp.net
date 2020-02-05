using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
	public class AnswerReview
	{
		public int Id { get; set; }
		public int ReviewId { get; set; }
		public int? UserId { get; set; }
		public string Content { get; set; }
		public DateTime CreatedDate { get; set; }
		public int Status { get; set; }
		[ForeignKey("ReviewId")]
		public  ReviewProduct ReviewProduct { get; set; }
		[ForeignKey("UserId")]
		public virtual  User User { get; set; }
	}
}
