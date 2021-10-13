using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repository
{
    public interface IRepository<T>
    {
         Task<T> Create(T _object);

         void Update(T _object);

         IEnumerable<T> GetAll();

         T GetById(int Id);

         void Delete(T _object);

    }
}