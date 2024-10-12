using StackExchange.Redis;
using Store.G04.Core.Entities;
using Store.G04.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G04.Repository.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<CustomerBsaket?> GetBasketAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);

            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBsaket>(basket);
        }

        public async Task<CustomerBsaket> UpdateBasketAsync(CustomerBsaket basket)
        {
            var createdOrUpdated = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
        
            if (createdOrUpdated is false ) return new CustomerBsaket() {Id = basket.Id };

            return basket;
        }
        public async Task<bool> DeleteAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

    }
}
