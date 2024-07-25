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
        public ICompanyRepository Company { get; private set; }
        public IShopingCartRepository ShopingCart { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }
        

        public UnitOfWork(ApplicationDbContext db)
            {
                _db = db;
            OrderHeader = new OrderHeaderRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
       
            ApplicationUser = new ApplicationUserRepository(_db);
            Catogory = new CategoryRepository(_db);
            Product = new  ProductRepository(_db);
            Company = new CompanyRepository(_db);
           ShopingCart = new ShopingCartRepository(_db);
         

        }
       

      

        public void Save()
        {
             _db.SaveChanges();
        }
    }
}

