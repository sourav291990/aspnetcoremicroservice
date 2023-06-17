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
            ShoppingCart shoppingCart = new ShoppingCart
            {
                UserName = userName,
                Items = new List<ShoppingCartItem>()
            };
            var shoppingCarttems = await _redisCache.GetStringAsync(userName);
            if (null != shoppingCarttems && !string.IsNullOrEmpty(shoppingCarttems))
            {
                shoppingCart.Items = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(shoppingCarttems);
            }

            return shoppingCart;
        }

        public async Task<ShoppingCart> UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            await _redisCache.SetStringAsync(shoppingCart.UserName, JsonConvert.SerializeObject(shoppingCart.Items));
            return await GetShoppingCart(shoppingCart.UserName);
        }
    }
}
