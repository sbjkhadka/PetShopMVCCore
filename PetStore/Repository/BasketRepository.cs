using Microsoft.EntityFrameworkCore;
using PetStore.Models;
using PetStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Repository
{
    public class BasketRepository : IBasket
    {
        private DB_Context db;
        public BasketRepository(DB_Context _db)
        {
            db = _db;
        }
        public IEnumerable<Basket> GetBaskets => db.Baskets
                                                    .Include(global => global.Customers)
                                                //.Include(global=>global.BasketItems).ThenInclude(global=>global.Products);
                                                ;
        public void Add(Basket _Basket)
        {
            db.Baskets.Add(_Basket);
            db.SaveChanges();
        }

        public Basket GetBasket(int? Id)
        {
            return db.Baskets
                .Include(e => e.BasketItems)
                    .ThenInclude(a=>a.Products)
                .SingleOrDefault(m => m.BasketId == Id);
        }

        public void Remove(int? Id)
        {
            db.Baskets.Remove(db.Baskets.Find(Id));
            db.SaveChanges();
        }
    }
}
