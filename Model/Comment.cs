using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
	public class Comment
	{
		public int Id { get; set; }
		public  int UserId { get; set; }
		public int  ProductId { get; set; }
		public string Question { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifyDate { get; set; }
		public bool Status { get; set; }
		[ForeignKey("UserId")]
		public virtual  User User { get; set; }
		[ForeignKey("ProductId")]
		public  Product Product { get; set; }
		public virtual ICollection<AnswerComment> Answers { get; set; }
	}
}
