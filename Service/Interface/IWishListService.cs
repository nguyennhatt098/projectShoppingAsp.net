﻿using System.Collections.Generic;
using Model;

namespace Service.Interface
{
	public  interface IWishListService<T> where T : class
    {
        IEnumerable<T> GetAll();
        int Insert(T t);
		int Delete(WishList item);
		IEnumerable<WishList> GetById(int id);
		int AddMutiple(List<WishList> items);
        int CountByProductId(int id);
    }
}
