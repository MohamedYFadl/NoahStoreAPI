using NoahStore.Core.Entities;
using NoahStore.Core.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace NoahStore.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public Task<bool> DeleteBasketAsync(string id)
        {
            return _database.KeyDeleteAsync( id);
        }
        public async Task<CustomerBasket> GetBasketAsync(string id)
        {
            var result = await _database.StringGetAsync( id);
            return string.IsNullOrEmpty(result) ? null : JsonSerializer.Deserialize<CustomerBasket>(result);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            if (basket.Id is null)
            {// new basket
                basket.Id= Guid.NewGuid().ToString();
            }
            var _basket = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(10));
            if (_basket)
            {
                return await GetBasketAsync(basket.Id);
            }
            return null;

        }
    }
}
