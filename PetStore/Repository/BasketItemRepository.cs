using Microsoft.EntityFrameworkCore;
using PetStore.Models;
using PetStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Repository
{
    public class BasketItemRepository : IBasketItem
    {
        private DB_Context db;
        public BasketItemRepository(DB_Context _db)
        {
            db = _db;
        }
        public IEnumerable<BasketItem> GetBasketItems => db.BasketItems
                                                            .Include(global=>global.Products);
        

        public void Add(BasketItem _BasketItem)
        {
            db.BasketItems.Add(_BasketItem);
            db.SaveChanges();
        }

        public BasketItem GetBasketItem(int? Id)
        {
            return db.BasketItems.Find(Id);
        }

        public void Remove(int? Id)
        {
            db.BasketItems.Remove(db.BasketItems.Find(Id));
            db.SaveChanges();
        }
    }
}
