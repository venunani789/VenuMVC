using System;
using Venu.DataAccess.Data;
using Venu.Models.Models;
using Venu.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Venu.DataAccess.Repository
{

    public class CategoryRepository : Repository<Catogory>,ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Catogory obj)
        {
            _db.Catogories.Update(obj);

        }

    }
}
