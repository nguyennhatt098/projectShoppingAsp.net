using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
	public class ReviewProductViewModel
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public int OrderId { get; set; }
		public int Rate { get; set; }
		public string Content { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? EndDate { get; set; }
		public bool Status { get; set; }
		public string Image { get; set; }
		public  Order Order { get; set; }
		public User User { get; set; }
		public Product Product { get; set; }
		public  ICollection<AnswerReview> AnswerReviews { get; set; }
	}
}
