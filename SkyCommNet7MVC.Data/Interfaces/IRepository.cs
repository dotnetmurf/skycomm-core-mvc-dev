using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Data.Interfaces
{
    public interface IRepository
    {
        public interface IRepository<T> where T : class
        {
            T GetById(int id);
            DbSet<T> GetAll();
            IQueryable<T> Where(Expression<Func<T, bool>> predicate);
            void Add(T entity);
            void AddRange(DbSet<T> entities);
            void Remove(T entity);
            void RemoveRange(DbSet<T> entities);
        }
    }
}
