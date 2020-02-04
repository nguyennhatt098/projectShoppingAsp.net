using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ViewModel;
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
		public IEnumerable<ReviewProduct> GetAll()
		{
			return context.ReviewProducts.AsQueryable();
		}

		public IEnumerable<ReviewProduct> GetReviewProductsByProductId(int id, int page, int pagesize)
		{
			var total = page * pagesize;
			if (total > CountReviewProductById(id))
			{
				pagesize = CountReviewProductById(id) - (pagesize * (page - 1));
			}

			if (pagesize <= 0)
			{
				pagesize = 6;
			}
			return context.ReviewProducts.Where(x => x.ProductId == id).OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pagesize).Take(pagesize).AsQueryable();
		}

		public int CountReviewProductById(int id)
		{
			return context.ReviewProducts.Count(x => x.ProductId == id);
		}

		public int InsertMultipleReviewProduct(List<ReviewProduct> items)
		{
			foreach (var item in items)
			{
				var obj = new ReviewProduct
				{
					Content = item.Content,
					CreatedDate = DateTime.Now,
					ProductId = item.ProductId,
					Rate = item.Rate,
					Status = true,
					OrderId = item.OrderId
				};
				context.ReviewProducts.Add(obj);
				context.SaveChanges();
			}
			return 1;
		}

		public int InsertAnswerReview(AnswerReview item)
		{
			context.AnswerReviews.Add(item);
			return context.SaveChanges();
		}

		public IEnumerable<AnswerReview> AnswerReviews()
		{
			return context.AnswerReviews.OrderByDescending(x=>x.CreatedDate).AsQueryable();
		}

		public CalculateRateDTO CalculateRate(int productId)
		{
			var list = context.ReviewProducts.Where(x => x.ProductId == productId).AsQueryable();
			float star1 = 0;
			float star2 = 0;
			float star3 = 0;
			float star4 = 0;
			float star5 = 0;
			float star = 0;
			foreach (var item in list)
			{
				star += item.Rate;
				switch (item.Rate)
                {
                    case 1:
                        star1 += 1;
                        break;
                    case 2:
                        star2 += 1;
                        break;
                    case 3:
                        star3 += 1;
                        break;
                    case 4:
                        star4 += 1;
                        break;
                    case 5:
                        star5 += 1;
                        break;
                }
            }
			var calculateItem = new CalculateRateDTO
			{
				Star1 = (int)Math.Round((star1 / list.Count()) * 100),
				Star2 = (int)Math.Round((star2 / list.Count()) * 100),
				Star3 = (int)Math.Round((star3 / list.Count()) * 100),
				Star4 = (int)Math.Round((star4 / list.Count()) * 100),
				Star5 = (int)Math.Round((star5 / list.Count()) * 100),
				Average = star / list.Count(),
				CountReview=context.ReviewProducts.Count(x=>x.ProductId==productId)
			};
			return calculateItem;
		}
	}
}
