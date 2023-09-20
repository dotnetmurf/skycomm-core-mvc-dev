using Microsoft.EntityFrameworkCore;
using SkyCommNet7MVC.Data.Repositories;
using System.Linq.Expressions;
using static SkyCommNet7MVC.Data.Interfaces.IRepository;

namespace SkyCommCoreMVC.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _entities;

        public Repository(SkyCommDbContext context)
        {
            _entities = context.Set<T>();
        }

        public T GetById(int id)
        {
            return _entities.Find(id);
        }

        public DbSet<T> GetAll()
        {
            return _entities;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void AddRange(DbSet<T> entities)
        {
            _entities.AddRange(entities);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(DbSet<T> entities)
        {
            _entities.RemoveRange(entities);
        }
    }
}
