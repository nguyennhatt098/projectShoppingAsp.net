using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.Interface
{
	public interface IReviewProduct
	{
		List<ReviewProduct> GetAll();
		List<ReviewProduct> GetReviewProductsByProductId(int id);
		int CountReviewProductById(int id);
	}
}
