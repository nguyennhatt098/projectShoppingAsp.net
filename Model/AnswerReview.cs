using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class AnswerReview
	{
		public int Id { get; set; }
		public int ReviewId { get; set; }
		public string Content { get; set; }
		public DateTime CreatedDate { get; set; }
		public int Status { get; set; }
		[ForeignKey("ReviewId")]
		public virtual ReviewProduct ReviewProduct { get; set; }
	}
}
