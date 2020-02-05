using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
	public class ReviewProduct
	{
		[Key]
		public int Id { get; set; }
		public int ProductId { get; set; }
		public int OrderId { get; set; }
		public int Rate { get; set; }
		public string Content { get; set; }
		public DateTime? CreatedDate{ get; set; }
		public DateTime? EndDate { get; set; }
		public bool Status { get; set; }
		public string Image { get; set; }
		[ForeignKey("OrderId")]
		public virtual  Order Order { get; set; }
		[ForeignKey("ProductId")]
		public  Product Product { get; set; }
		public virtual ICollection<AnswerReview> AnswerReviews { get; set; }
	}
}
