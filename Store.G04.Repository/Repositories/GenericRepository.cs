using Microsoft.EntityFrameworkCore;
using Store.G04.Core.Entities;
using Store.G04.Core.Repositories.Contract;
using Store.G04.Core.Specifications;
using Store.G04.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Repository.Repositories
{
    public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return (IEnumerable<TEntity>) await _context.Products.Include(p => p.Brand).Include(p => p.Type).ToListAsync();
            }

            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetAsync(Tkey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                //return  await _context.Products.Include(p => p.Brand).Include(p => p.Type).FirstOrDefaultAsync(p => p.Id == id as int?) as TEntity;
                return await _context.Products.Where(p => p.Id == id as int?).Include(p => p.Brand).Include(p => p.Type).FirstOrDefaultAsync() as TEntity;
            }

            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }
        public void UpdateAsync(TEntity entity)
        {
            _context.Update(entity);
        }

        public void DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, Tkey> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }

        public async Task<TEntity> GetWhithSpecAsync(ISpecifications<TEntity, Tkey> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> ApplySpecifications(ISpecifications<TEntity, Tkey> spec)
        {
            return SpecificationsEvaluator<TEntity, Tkey>.GetQuery(_context.Set<TEntity>(), spec);
        }
    }
}
