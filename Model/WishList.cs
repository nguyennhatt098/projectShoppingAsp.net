using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class WishList
    {
        [Key]
        public int ID { get; set; }
  
        public int UserID { get; set; }
       
        public int ProductID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
