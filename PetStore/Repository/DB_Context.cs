using Microsoft.EntityFrameworkCore;
using PetStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Repository
{
    public class DB_Context:DbContext
    {
        public DB_Context(DbContextOptions<DB_Context> options) : base(options)
        {

        }
        //name should exactly match with the database table
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
    }
}
