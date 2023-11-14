using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer
{
    public interface IDb<T, K>
    {
        void Create(T item);
        T Read(K key, bool usenavigationalproperties = false);
        IEnumerable<T> ReadAll(bool usenavigationalproperties = false);
        void Update(T item, bool usenavigationalproperties = false);
        void Delete(K key);
    }
}
