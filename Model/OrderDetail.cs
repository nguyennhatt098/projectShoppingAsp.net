using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
	public class OrderDetail
	{
		public int ID { get; set; }
		public int ProductId { get; set; }
		public int OrderId { get; set; }
		public float Price { get; set; }
		public double Quantity { get; set; }
		[ForeignKey("OrderId")]
		public  Order Order { get; set; }
		[ForeignKey("ProductId")]
		public virtual Product Product { get; set; }
	}

}
