using System;

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
        public string Image { get; set; }
	}
}
