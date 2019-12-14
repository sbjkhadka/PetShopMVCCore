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
        int Add(Basket _Basket); //changed from void to int
        void Remove(int? Id);
        void Update(int? Id, Basket _Basket);
    }
}
