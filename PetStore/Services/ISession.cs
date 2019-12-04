using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Services
{
    public interface ISession
    {
        bool TryGetValue(string key, out int value);
        void Set(string key, byte[] value);
        void Remove(string key);
        int GetInt(string key);


    }
}
