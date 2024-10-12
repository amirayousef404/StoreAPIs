using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Repositories.Contract
{
    public interface IBasketRepository
    {
        Task<CustomerBsaket?> GetBasketAsync(string basketId);

        Task<CustomerBsaket> UpdateBasketAsync(CustomerBsaket basket);

        Task<bool> DeleteAsync(string basketId);
    }
}
