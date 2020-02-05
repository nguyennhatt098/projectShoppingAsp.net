using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model
{
	public class News
    {
        [Key]
        public int  ID { get; set; }
		[Required(ErrorMessage = "Trường này không được để trống")]
		public string Slug { get; set; }
		[DisplayName("Hình ảnh")]
		[Required(ErrorMessage = "Trường này không được để trống")]
		public string Images { get; set; }
		[DisplayName("Nội dung chung")]
		[Required(ErrorMessage = "Trường này không được để trống")]
		public string Summary { get; set; }
		[DisplayName("Nội dung")]
		[Required(ErrorMessage = "Trường này không được để trống")]
		public string Content { get; set; }
		[DisplayName("Ngày tạo")]
		public DateTime CreateDate { get; set; }
        [DisplayName("trạng thái")]
		public bool Status { get; set; }
    }
}
