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
                                                    .Include(global => global.Customers);
        public int Add(Basket _Basket) //changed from void to int
        {
            db.Baskets.Add(_Basket);
            db.SaveChanges();
            return _Basket.BasketId; //returned basketId of recently created basket
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

        public void Update(int? Id,Basket _Basket) {
            Basket updatedBasket = db.Baskets.FirstOrDefault(i=>i.BasketId==Id);
            updatedBasket.Quantity += _Basket.Quantity;
            updatedBasket.Total += _Basket.Total;
            updatedBasket.SubTotal = updatedBasket.Total;
            db.Baskets.Update(updatedBasket);
            db.SaveChanges();
        }
    }
}
