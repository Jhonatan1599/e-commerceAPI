using System.Text.Json;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {   
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);  
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);  
        }

        //will use this method to update or create 
        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {   
            //we'll keep the baskets hanging around 30 days
            var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket),
                 TimeSpan.FromDays(30));

            if(!created) return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}