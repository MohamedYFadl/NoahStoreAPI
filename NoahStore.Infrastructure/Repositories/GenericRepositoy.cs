using Microsoft.EntityFrameworkCore;
using NoahStore.Core.Interfaces;
using NoahStore.Infrastructure.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NoahStore.Infrastructure.Repositories
{
    public class GenericRepositoy<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;

        public GenericRepositoy(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
        }

        public async Task<int> GetCountAsync(ISpecification<T> specs)
        {
            return await ApplySpecs(specs).CountAsync();
        }
        

        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _db.Set<T>().AsNoTracking().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> specs)
            => await ApplySpecs(specs).AsNoTracking().ToListAsync();

        public async Task<T> GetByIdAsync(int id)
            => await _db.Set<T>().FindAsync(id);

        public async Task<T> GetByIdAsync(ISpecification<T> specs)
            => await ApplySpecs(specs).FirstOrDefaultAsync();
        

        public async Task RemoveAsync(int id)
        {
            var entity = await _db.Set<T>().FindAsync(id);
              _db.Set<T>().Remove(entity);
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
            =>  _db.Set<T>().RemoveRange(entities);

        public async Task UpdateAsync(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }

        private IQueryable<T> ApplySpecs(ISpecification<T> specs)
        {
            return SpecificationEvaluator<T>.GetQuery(_db.Set<T>(), specs);
        }
    }
}
