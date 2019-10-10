using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public virtual User User { get; set; }
		[ForeignKey("ProductId")]
		public virtual Product Product { get; set; }
		public ICollection<AnswerComment> Answers { get; set; }
	}
}
