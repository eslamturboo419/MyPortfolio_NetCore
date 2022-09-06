using WebApplication1.Models.Interfaces;

namespace WebApplication1.Models
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly MyDbContext db;
        private IGenericRepository<T> _entity;

        public UnitOfWork(MyDbContext db)
        {
            this.db = db;
        }
        public IGenericRepository<T> Entity
        {
            get
            {
                return _entity ?? (_entity = new GenericRepository<T>(db));
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
