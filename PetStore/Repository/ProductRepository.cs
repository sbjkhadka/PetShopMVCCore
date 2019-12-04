using PetStore.Models;
using PetStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Repository
{
    public class ProductRepository : IProduct
    {
        private DB_Context db;
        public ProductRepository(DB_Context _db)
        {
            db = _db;
        }
        public IEnumerable<Product> GetProducts => db.Products;

        public void Add(Product _Product)
        {
            if (_Product.ProductId == 0)
            {
                db.Products.Add(_Product);
                db.SaveChanges();
            }
            else
            {
                var dbEntity = db.Products.Find(_Product.ProductId);
                dbEntity.ProductName = _Product.ProductName;
                dbEntity.Price = _Product.Price;
                dbEntity.Description = _Product.Description;
                db.SaveChanges();
            }
            
        }

        public Product GetProduct(int? Id)
        {
            return db.Products.Find(Id);
        }

        public void Remove(int? Id)
        {
            db.Products.Remove(db.Products.Find(Id));
            db.SaveChanges();
        }
        public void Update(Product _Product) {
            db.Products.Update(_Product);
            db.SaveChanges();
        }
    }
}
