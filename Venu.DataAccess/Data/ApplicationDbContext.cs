using System;
using Microsoft.EntityFrameworkCore;
using Venu.Models.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace Venu.DataAccess.Data
{
	public class ApplicationDbContext: DbContext
    { //This is the constructor for the ApplicationDbContext class. It takes an instance of DbContextOptions<ApplicationDbContext> as a parameter and passes it to the base class constructor (DbContext) using the base(options) syntax. This constructor allows Entity Framework Core to inject the database context options, including the connection string and other configuration settings.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) :base(options)
		{
			
		}//DbSet is a property of EF to accessing and manipulating Catogory class
        public DbSet<Catogory> Catogories { get; set; }
        public DbSet<Product> Products { get; set; }
        //Seeding data

        // This method OnModelCreating is an override of a method from the DbContext class. 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {//Inside this method, you're using the modelBuilder parameter to configure the model for the Category entity. Specifically, you're using the HasData method to seed some initial data into the Category table.
            modelBuilder.Entity<Catogory>().HasData(
            new Catogory { id = 1, Name ="Test", DisplayOrder = 1 });
            modelBuilder.Entity<Product>().HasData(
           new Product { id = 1, Description = "Test", ISBN = "",Author="Venu" ,Title="KingKong",ListPrice=20,Price=30,Price50=60,Price100=110});

        }
    }
}

