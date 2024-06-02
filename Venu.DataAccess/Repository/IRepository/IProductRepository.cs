using System;
using Venu.Models.Models;

namespace Venu.DataAccess.Repository.IRepository
{
	public interface IProductRepository:IRepository<Product>
	{
        void Update(Product obj);
    }
}

