using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ViewModel;
using Repository;
using Repository.DAL;
using Repository.Interface;

namespace Service
{
	public class ReviewProductService : IReviewProduct
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

		public IEnumerable<ReviewProduct> GetReviewProductsByProductId(int id, int page, int pagesize)
		{
			return repository.GetReviewProductsByProductId(id,page,pagesize);
		}

		public int CountReviewProductById(int id)
		{
			return repository.CountReviewProductById(id);
		}

		public int InsertMultipleReviewProduct(List<ReviewProduct> items)
		{
			return repository.InsertMultipleReviewProduct(items);
		}

		public int InsertAnswerReview(AnswerReview item)
		{
			return repository.InsertAnswerReview(item);
		}

		public List<AnswerReview> AnswerReviews()
		{
			return repository.AnswerReviews();
		}

		public CalculateRateDTO CalculateRate(int productId)
		{
			return repository.CalculateRate(productId);
		}
	}
}
