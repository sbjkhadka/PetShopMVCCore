using PetStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Services
{
    public interface IBasket
    {
        IEnumerable<Basket> GetBaskets { get; }
        Basket GetBasket(int? Id);
        void Add(Basket _Basket);
        void Remove(int? Id);
    }
}
