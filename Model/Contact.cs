using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model
{
	public class Contact
	{
		[Key]
		public int ID { get; set; }
		[DisplayName("Nội dung")]
		public string Content { get; set; }
		[DisplayName("trạng thái")]
		public bool Status { get; set; }
	}
}
