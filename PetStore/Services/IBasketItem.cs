using PetStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Services
{
    public interface IBasketItem
    {
        IEnumerable<BasketItem> GetBasketItems { get; }
        BasketItem GetBasketItem(int? Id);
        void Add(BasketItem _BasketItem);
        void Remove(int? Id);
    }
}
