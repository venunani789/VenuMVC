using System;
using Venu.DataAccess.Data;
using Venu.DataAccess.Repository.IRepository;
using Venu.Models.Models;

namespace Venu.DataAccess.Repository
{
	public class UnitOfWork:IUnitOfWork
	{

        private readonly ApplicationDbContext _db;
        public ICategoryRepository Catogory { get; private set; }
        public IProductRepository Product { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
            {
                _db = db;
            Catogory = new CategoryRepository(_db);
            Product = new  ProductRepository(_db);
        }
       

      

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

