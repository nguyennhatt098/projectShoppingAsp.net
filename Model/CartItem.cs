using System;

namespace Model
{
	[Serializable]
    public class CartItem
    {
        public Product Product { get; set; }
        public double Quantity { get; set; }
    }
}
