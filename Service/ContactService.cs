using Repository.DAL;
using Model;
using Repository;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;

namespace Service
{
    public class ContactService : IServices<Contact>
    {
        private IRepository<Contact> repository;
        public ContactService()
        {
            repository = new ContactRepository(new DBEntityContext());
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contact> GetAll()
        {
            throw new NotImplementedException();
        }

        public Contact GetById(int id)
        {
	        return repository.GetById(id);
        }

        public int Insert(Contact t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contact> Search(string searchString, int Page, int Pagesize)
        {
            throw new NotImplementedException();
        }

        public int Update(Contact t)
        {
            throw new NotImplementedException();
        }
    }
}

