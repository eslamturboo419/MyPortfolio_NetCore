using System.Collections;
using System.Collections.Generic;

namespace WebApplication1.Models.Interfaces
{
    public interface IGenericRepository<T> where T :  class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T  entity);
        void Update(T entity);
        void Delete(object id);
    }
}
