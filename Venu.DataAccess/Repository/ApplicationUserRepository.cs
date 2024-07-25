using System;
using Venu.DataAccess.Data;
using Venu.Models.Models;
using Venu.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Venu.DataAccess.Repository
{

    public class ApplicationUserRepository : Repository<ApplicationUser>,IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       


    }
}
