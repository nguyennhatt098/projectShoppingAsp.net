using System;

namespace Model.ViewModel
{
	public class CategoryDTO
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Slug { get; set; }
		public int? ParentID { get; set; }
		public bool Status { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifileDate { get; set; }
		public string ParentName { get; set; }
	}
}
