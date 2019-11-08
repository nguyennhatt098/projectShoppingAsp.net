using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository.DAL;
using Repository.Interface;

namespace Repository
{
	public class ReviewProductRepository:IReviewProduct
	{
		private DBEntityContext context;
		public ReviewProductRepository(DBEntityContext context)
		{
			this.context = context;
		}
		public List<ReviewProduct> GetAll()
		{
			return context.ReviewProducts.ToList();
		}

		public List<ReviewProduct> GetReviewProductsByProductId(int id)
		{
			return context.ReviewProducts.Where(x => x.ProductId == id).ToList();
		}

		public int CountReviewProductById(int id)
		{
			return context.ReviewProducts.Count(x => x.ProductId == id);
		}
	}
}
