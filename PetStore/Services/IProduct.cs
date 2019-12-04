using PetStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Services
{
    public interface IProduct
    {
        IEnumerable<Product> GetProducts { get; }
        Product GetProduct(int? Id);
        void Add(Product _Product);
        void Remove(int? Id);
        void Update(Product _product);
    }
}
