using NoahStore.Core.Interfaces;
using NoahStore.Infrastructure.Data.DbContexts;
using System.Collections;

namespace NoahStore.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Hashtable _repositories;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
           _dbContext = dbContext;
            _repositories = new Hashtable();
        }
        public async ValueTask DisposeAsync()
        {
           await  _dbContext.DisposeAsync();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var key = typeof(TEntity).Name;
            if(!_repositories.ContainsKey(key))
            {
                var repository = new GenericRepositoy<TEntity>(_dbContext);
                _repositories.Add(key, repository);
            }
            return _repositories[key] as IGenericRepository<TEntity>;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
