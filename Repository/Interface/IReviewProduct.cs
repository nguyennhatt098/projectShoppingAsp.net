﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ViewModel;

namespace Repository.Interface
{
	public interface IReviewProduct
	{
		List<ReviewProduct> GetAll();
		IEnumerable<ReviewProduct> GetReviewProductsByProductId(int id, int page, int pagesize);
		int CountReviewProductById(int id);
		int InsertMultipleReviewProduct(List<ReviewProduct> items);
		int InsertAnswerReview(AnswerReview item);
		List<AnswerReview> AnswerReviews();
		CalculateRateDTO CalculateRate(int productId);
	}
}
