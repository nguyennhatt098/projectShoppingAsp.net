using System.Collections.Generic;
using Model;

namespace Service.Interface
{
	public interface IListProductService<T> where T : class
    {
        IEnumerable<ProductViewModel> ListProductHot();
        IEnumerable<T> ListProductSale();
        IEnumerable<ProductViewModel> ListProductNew();
        List<ProductViewModel> ListProductGetByCategory(int id, int pageIndex, int pageSize);
        int Count(int id);
        IEnumerable<Category> Categories();
        ProductViewModel GetProductById(int id);
    }
}
