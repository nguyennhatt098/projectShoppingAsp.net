﻿using Model;
using Repository;
using Repository.DAL;
using Repository.Interface;
using Service.Interface;
using System.Collections.Generic;

namespace Service
{
    public class WishListService : IWishListService<WishList>
    {
        private IWishListReponsitory<WishList> repository;
        public WishListService()
        {
            repository = new WishListRepository(new DBEntityContext());
        }


        public int Delete(WishList item)
        {
	        return repository.Delete(item);
        }

        public IEnumerable<WishList> GetById(int id)
        {
	        return repository.GetById(id);
        }

        public int AddMutiple(List<WishList> items)
        {
	        return repository.AddMultiple(items);
        }

        public int CountByProductId(int id)
        {
            return repository.CountByProductId(id);
        }

        public IEnumerable<WishList> GetAll()
        {
            return repository.GetAll();
        }

        public int Insert(WishList t)
        {
            return repository.Insert(t);
        }
    }
}
