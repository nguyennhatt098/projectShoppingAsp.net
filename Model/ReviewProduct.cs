using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public virtual Product Product { get; set; }
		public ICollection<AnswerReview> AnswerReviews { get; set; }
	}
}
