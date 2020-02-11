using System.Collections.Generic;

namespace Model.ViewModel
{
	public class SearchResponse<TEntity>
	{
		public int TotalRecords { get; set; }
		public IList<TEntity> items { get; set; }
	}
}
