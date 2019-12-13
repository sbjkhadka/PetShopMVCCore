using Microsoft.EntityFrameworkCore;
using PetStore.Models;
using PetStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Repository
{
    public class CustomerRepository : ICustomer
    {
        private DB_Context db;
        public CustomerRepository(DB_Context _db)
        {
            db = _db;
        }
        public IEnumerable<Customer> GetCustomers => db.Customers;

        public void Add(Customer _Customer)
        {
            db.Customers.Add(_Customer);
            db.SaveChanges();
        }

        public Customer GetCustomer(int? Id) {
            return db.Customers
                        .Include(e => e.Baskets)
                        .SingleOrDefault(m => m.CustomerId == Id);
        }
       
            public void Remove(int? Id)
        {
            db.Customers.Remove(db.Customers.Find(Id));
            db.SaveChanges();

        }
    }
}
