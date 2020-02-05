using System.Collections.Generic;
using Model;

namespace Repository.Interface
{
	public interface IListProduct<T> where T : class
    {
        IEnumerable<T> ListProductHot();
        IEnumerable<T> ListProductSale();
        IEnumerable<T> ListProductNew();
        IEnumerable<T> ListProductGetByCategory(int id,int pageIndex,int pageSize);
        int Count(int id);
        IEnumerable<Category> Categories();
    }
}
