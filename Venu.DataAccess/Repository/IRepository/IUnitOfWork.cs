using System;
using System.Collections.Generic;
namespace Venu.DataAccess.Repository.IRepository
{
	public interface IUnitOfWork
	{ //here Category property is expected to return an instance of a type representing repository for categories table
		ICategoryRepository  Catogory{get;}
        IProductRepository Product { get; }
        void Save();
		
		
	}
}

