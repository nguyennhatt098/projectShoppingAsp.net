using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model
{
	public class Footer
	{
		public int ID { get; set; }
		[DisplayName("Nội dung")]
		[Required(ErrorMessage = "Trường này không được để trống")]
		public string Content { get; set; }
		[DisplayName("Trạng thái")]
		public bool Status { get; set; }
	}
}
