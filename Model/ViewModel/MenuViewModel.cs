using System.Collections.Generic;

namespace Model.ViewModel
{
	public class MenuViewModel
	{
		public int MenuId { get; set; }
		public string Name { get; set; }
		public string Slug { get; set; }
		public List<MenuViewModel> Children { get; set; }
	}
}
