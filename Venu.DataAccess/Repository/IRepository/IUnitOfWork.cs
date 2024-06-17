using System;
using System.Collections.Generic;
namespace Venu.DataAccess.Repository.IRepository
{
	public interface IUnitOfWork
	{ //here Category,product,company property is expected to return an instance of a type representing repository for categories & Companies & Producs table
		ICategoryRepository  Catogory{get;}
        IProductRepository Product { get; }
      ICompanyRepository Company { get; }

        void Save();
		
		
	}
}

