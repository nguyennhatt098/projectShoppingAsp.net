using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
	public class CommentDTO
	{
		public int CommentId { get; set; }
		public string Question { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifyDate { get; set; }
		public bool Status { get; set; }
		public string UserName { get; set; }
	}
}
