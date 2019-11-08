using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;
using Repository.DAL;
using Repository.Interface;

namespace Service
{
	public class ReviewProductService:IReviewProduct
	{
		private IReviewProduct repository;
		public ReviewProductService()
		{
			repository = new ReviewProductRepository(new DBEntityContext());
		}
		public List<ReviewProduct> GetAll()
		{
			return repository.GetAll();
		}

		public List<ReviewProduct> GetReviewProductsByProductId(int id)
		{
			return repository.GetReviewProductsByProductId(id);
		}

		public int CountReviewProductById(int id)
		{
			return repository.CountReviewProductById(id);
		}
	}
}
