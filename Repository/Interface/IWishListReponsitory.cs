using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.Interface
{
    public interface IWishListReponsitory<T> where T : class
    {
        IEnumerable<T> GetAll();
        int Insert(T t);
        int Delete(WishList item);
        IEnumerable<WishList> GetById(int id);
        int AddMultiple(List<WishList> items);
        int CountByProductId(int id);
    }
}
