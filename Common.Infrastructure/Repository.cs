using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Common.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class, IAggregateRoot
    {
        protected IDbSet<T> _dbSet;
        protected IDbContext _context;
        public Repository(IDbContext context)
        {
            _dbSet = context.Set<T>();
            _context = context;
        }
    }

    public interface IDbContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        Database Database { get; } 
        int SaveChanges();
    }
}
