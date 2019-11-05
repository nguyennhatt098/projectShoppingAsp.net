using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class Notify
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string Content { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifyDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string Link { get; set; }
		public int Status { get; set; }
		[ForeignKey("UserId")]
		public User User { get; set; }
	}
}
