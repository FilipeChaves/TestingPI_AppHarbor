using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_PI.DataMapper
{
    interface IDataMapper<T>
    {
        void Add(T t);
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}
