using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Venu.Models.Models;

namespace Venu.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Catogory> Catogories { get; set; }
        public DbSet<Product> Products { get; set; }
       
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ShopingCart> ShopingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Catogory>().HasData(
                new Catogory { id = 1, Name = "Test", DisplayOrder = 1 },
                new Catogory { id = 2, Name = "Test1", DisplayOrder = 2 }
            );
            modelBuilder.Entity<Company>().HasData(
               new Company { id = 1, Name = "VIVID", StreetAddress = "2229 west taylor street",State="Illinois",PostalCode="60612",City="Chicago",PhoneNumber="7735841333" },
               new Company { id = 2, Name = "VIKING", StreetAddress = "2226 west taylor street", State = "Illinois", PostalCode = "60612", City = "Chicago", PhoneNumber = "7725841333" }
           );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    id = 1,
                    Description = "Test",
                    ISBN = "SWD7868",
                    Author = "Venu",
                    Title = "KingKong",
                    ListPrice = 20,
                    Price = 30,
                    Price50 = 60,
                    Price100 = 100,
                    CatogoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    id = 2,
                    Description = "Test1",
                    ISBN = "SWD7869",
                    Author = "Venu1",
                    Title = "KingKong1",
                    ListPrice = 30,
                    Price = 40,
                    Price50 = 70,
                    Price100 = 100,
                    CatogoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    id = 3,
                    Description = "Test1",
                    ISBN = "SWD7870",
                    Author = "Venu2",
                    Title = "KingKong2",
                    ListPrice = 30,
                    Price = 40,
                    Price50 = 70,
                    Price100 = 100,
                    CatogoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    id = 4,
                    Description = "Test1",
                    ISBN = "SWD7871",
                    Author = "Venu3",
                    Title = "KingKong3",
                    ListPrice = 30,
                    Price = 40,
                    Price50 = 70,
                    Price100 = 100,
                    CatogoryId = 2,
                    ImageUrl = ""
                }
            );
        }
    }
}
