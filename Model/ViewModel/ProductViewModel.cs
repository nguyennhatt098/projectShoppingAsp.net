
using System;
using System.Collections.Generic;

namespace Model
{
	public class ProductViewModel
	{
        public int Id { get; set; }
		public string Name { get; set; }
        public string Slug { get; set; }
		public string Content { get; set; }
		public string Images { get; set; }
		public double Price { get; set; }
		public double? Sale_Price { get; set; }
		public int Category_ID { get; set; }
		public string MoreImages { get; set; }
		public DateTime? Created { get; set; }
		public DateTime? ModifileDate { get; set; }
		public bool Status { get; set; }
		public bool TopHot { get; set; }
		public float Star1 { get; set; }
		public float Star2 { get; set; }
		public float Star3 { get; set; }
		public float Star4 { get; set; }
		public float Star5 { get; set; }
		public float AverageStar { get; set; }
		public  Category Categorys { get; set; }
		public  ICollection<WishList> wishLists { get; set; }
	}
}
