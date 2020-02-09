using Repository.DAL;
using Model;
using Repository;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;

namespace Service
{
    public class ProductService : IServices<Product>, IListProductService<Product>
    {
        private IRepository<Product> repository;
        private IListProduct<Product> rep;

        public ProductService()
        {
            rep = new ProductRepository(new DBEntityContext());
            repository = new ProductRepository(new DBEntityContext());
        }

        public int Delete(int id)
        {
            return repository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return repository.GetAll();
        }

        public Product GetById(int id)
        {
            return repository.GetById(id);
        }

        public int Insert(Product t)
        {
            return repository.Insert(t);
        }

        public List<ProductViewModel> ListProductGetByCategory(int id, int pageIndex, int pageSize)
        {
            return rep.ListProductGetByCategory(id,pageIndex,pageSize);
        }

        public int Count(int id)
        {
	        return rep.Count(id);
        }

        public IEnumerable<Category> Categories()
        {
	        return rep.Categories();
        }

        public ProductViewModel GetProductById(int id)
        {
	        return rep.GetProductById(id);
        }

        public IEnumerable<ProductViewModel> ListProductHot()
        {
            return rep.ListProductHot();
        }

        public IEnumerable<ProductViewModel> ListProductNew()
        {
            return rep.ListProductNew();
        }

        public IEnumerable<Product> ListProductSale()
        {
            return rep.ListProductSale();
        }

		public IEnumerable<Product> Search(string searchString, int Page, int Pagesize)
		{
			return repository.Search(searchString, Page, Pagesize);
		}

		public int Update(Product t)
        {
            return repository.Update(t);
        }
    }
}
