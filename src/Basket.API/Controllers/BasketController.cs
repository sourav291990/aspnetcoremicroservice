using Basket.API.Entities;
using Basket.API.Repostories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.CompilerServices;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        [Route("basket/{userName}")]
        [ProducesResponseType(typeof(ShoppingCart), ((int)HttpStatusCode.OK))]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var shopppingCart = await _basketRepository.GetShoppingCart(userName);
            return Ok(shopppingCart ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [Route("basket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket(ShoppingCart shoppingCart)
        {
            var updatedCart = await _basketRepository.UpdateShoppingCart(shoppingCart);
            return Ok(updatedCart);
        }

        [HttpDelete]
        [Route("basket/{userName}")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteBasket(string userName)
        {
            var basket = await _basketRepository.GetShoppingCart(userName);
            if (basket == null || basket.Items == null || basket.Items.Count == 0)
            {
                return NotFound("No items in shopping cart!!!");
            }
            await _basketRepository.DeleteShoppingCart(userName);
            return Ok("shopping cart deleled succesfully");
        }
    }   
}
