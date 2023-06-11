using Basket.API.Entities;

namespace Basket.API.Repostories
{
    public interface IBasketRepository
    {
        /// <summary>
        /// Gets the list of item from the basket
        /// </summary>
        /// <returns></returns>
        Task<ShoppingCart> GetShoppingCart(string userName);

        /// <summary>
        /// Updates the basket
        /// </summary>
        /// <returns></returns>
        Task<ShoppingCart> UpdateShoppingCart(ShoppingCart shoppingCart);

        /// <summary>
        /// Deletes shopping cart
        /// </summary>
        /// <returns></returns>
        Task DeleteShoppingCart(string userName);
    }
}
