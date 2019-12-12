using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
	public class Notify
	{
        [Key]
		public int Id { get; set; }
		public int? UserId { get; set; }
		public string Content { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifyDate { get; set; }
		public DateTime? EndDate { get; set; }
        public string Image { get; set; }
		public string Link { get; set; }
		public int Status { get; set; }
		[ForeignKey("UserId")]
		public User User { get; set; }
	}
}
