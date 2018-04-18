using BookRental.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookRental.Dal
{
    public class DBStuff : DbContext
    {
        // get objects from type Customer and save in DB or Change or Delete , and more...

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Login>().ToTable("OrdersTB");
            modelBuilder.Entity<Cars>().ToTable("CarsAvailable");
            modelBuilder.Entity<MyOrder>().ToTable("MyUsers");

        }
        public DbSet<Login> CustomersLog { get; set; }
        public DbSet<Cars> CarsLog { get; set; }
        public DbSet<MyOrder> OrdersLog { get; set; }

    }
}