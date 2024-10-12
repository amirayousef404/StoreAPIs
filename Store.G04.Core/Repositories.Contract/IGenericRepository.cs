using Store.G04.Core.Entities;
using Store.G04.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Repositories.Contract
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(Tkey id);

        Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, Tkey> spec);

        Task<TEntity> GetWhithSpecAsync(ISpecifications<TEntity, Tkey> spec);

        Task<int> GetCountAsync(ISpecifications<TEntity, Tkey> spec);
        Task AddAsync(TEntity entity);


        void UpdateAsync(TEntity entity);

        void DeleteAsync(TEntity entity);


    }
}
