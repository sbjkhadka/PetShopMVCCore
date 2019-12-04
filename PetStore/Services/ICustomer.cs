using PetStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Services
{
    public interface ICustomer
    {
        IEnumerable<Customer> GetCustomers { get; }
        Customer GetCustomer(int? Id);
        void Add(Customer _Customer);
        void Remove(int? Id);

    }
}
