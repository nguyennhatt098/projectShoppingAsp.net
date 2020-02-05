using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
	public class WishList
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        [ForeignKey("UserID")]
        public  User User { get; set; }
        [ForeignKey("ProductID")]
        public  Product Product { get; set; }
    }
}
