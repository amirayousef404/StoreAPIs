using Store.G04.Core.Entities;
using Store.G04.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();

        // this func will create Repository<T> and return it to me

        IGenericRepository<TEntity, Tkey> Repository<TEntity, Tkey>() where TEntity: BaseEntity<Tkey>;
    }
}
