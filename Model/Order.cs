using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
	public class Order
	{
		[Key]
		public int ID { get; set; }
		public int User_ID { get; set; }
		[DisplayName("Tên người nhận")]
		[Required(ErrorMessage = "Trường này không được để trống")]
		public string Name { get; set; }
		[RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", ErrorMessage = "Không đúng định dạng mail")]
		[Required(ErrorMessage = "Trường này không được để trống")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Trường này không được để trống")]
		[RegularExpression(@"^([0-9]{10})$", ErrorMessage = "không đúng định dạng phone")]
		[DisplayName("Số điện thoại")]
		public string Phone { get; set; }
		[Required(ErrorMessage = "Trường này không được để trống")]
		[DisplayName("Địa chỉ")]
		public string Address { get; set; }
		[DisplayName("Ngày tạo")]
		public DateTime? Created { get; set; }
		[DisplayName("Trạng thái")]
		public int Status { get; set; }
		[ForeignKey("User_ID")]
		public virtual User Users { get; set; }
		public ICollection<OrderDetail> OdersDetail { get; set; }
		public virtual ICollection<ReviewProduct> ReviewProducts { get; set; }
	}
}
