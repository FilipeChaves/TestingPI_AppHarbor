using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etapa1.DataMappers
{
    interface IDataMapper<T, K>
    {
        void Add(T t);
        T GetById(K id);
        IEnumerable<T> GetAll();
    }
}
