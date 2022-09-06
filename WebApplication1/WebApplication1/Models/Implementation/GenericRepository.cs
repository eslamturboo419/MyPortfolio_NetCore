using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models.Interfaces;

namespace WebApplication1.Models
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MyDbContext db;
        // to work with the table
        private DbSet<T> table = null; 

        public GenericRepository(MyDbContext db)
        {
            this.db = db;
            table = db.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return  table.ToList();
        }

        public T GetById(object id)
        {
            var val = table.Find(id) ;
            return val;
        }

        public void Insert(T entity)
        {
            table.Add(entity);
        }

        public void Update(T entity)
        {
            table.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
           var val = GetById(id);
            table.Remove(val);
        }



    }
}
