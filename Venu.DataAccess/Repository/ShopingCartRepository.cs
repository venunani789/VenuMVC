using System;
using Venu.DataAccess.Data;
using Venu.Models.Models;
using Venu.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Venu.DataAccess.Repository
{

    public class ShopingCartRepository : Repository<ShopingCart>, IShopingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShopingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(ShopingCart obj)
        {
            _db.ShopingCarts.Update(obj);

        }

    }
}
