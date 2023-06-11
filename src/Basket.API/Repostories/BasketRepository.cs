using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace Basket.API.Repostories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public Task DeleteShoppingCart(string userName)
        {
            return _redisCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetShoppingCart(string userName)
        {
            var shoppingCart = await _redisCache.GetStringAsync(userName);
            if (shoppingCart == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ShoppingCart>(shoppingCart);
        }

        public async Task<ShoppingCart> UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            await _redisCache.SetStringAsync(shoppingCart.UserName, JsonConvert.SerializeObject(shoppingCart.Items));
            return await GetShoppingCart(shoppingCart.UserName);
        }
    }
}
