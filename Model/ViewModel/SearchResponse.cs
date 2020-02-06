using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
	public class SearchResponse<TEntity>
	{
		public int TotalRecords { get; set; }
		public IList<TEntity> items { get; set; }
	}
}
