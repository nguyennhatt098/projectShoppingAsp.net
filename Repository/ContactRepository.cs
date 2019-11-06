using Repository.DAL;
using Model;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class ContactRepository : IRepository<Contact>, IDisposable
    {
        private DBEntityContext context;
        public ContactRepository(DBEntityContext context)
        {
            this.context = context;
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        private bool disposed = false;
        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public IEnumerable<Contact> GetAll()
        {
            throw new NotImplementedException();
        }

        public Contact GetById(int id)
        {
			return context.Contacts.FirstOrDefault(x => x.Status);
		}

        public IEnumerable<Contact> Search(string searchString, int Page, int Pagesize)
        {
            throw new NotImplementedException();
        }

        public int Update(Contact t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Contact t)
        {
            throw new NotImplementedException();
        }
    }
}
