using System;
using Venu.DataAccess.Data;
using Venu.Models.Models;
using Venu.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace Venu.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Product obj)
        {
            _db.Products.Update(obj);

        }
    }
}

