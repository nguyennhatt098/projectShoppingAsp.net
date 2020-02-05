using System.Collections.Generic;

namespace Service.Interface
{
	public interface IServices<T> where T:class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Search(string searchString, int Page, int Pagesize);
        int Insert(T t);
        int Update(T t);
        int Delete(int id);
        T GetById(int id);
    }
}
