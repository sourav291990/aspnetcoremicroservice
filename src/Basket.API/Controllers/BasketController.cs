using AutoMapper;
using Basket.API.Entities;
using Basket.API.Repostories;
using Basket.API.Services;
using EventMangementLibrary.Events;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.CompilerServices;
using static StackExchange.Redis.Role;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGRPCService _discountService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketController(IBasketRepository basketRepository, 
            DiscountGRPCService discountGRPCService, 
            IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            _basketRepository = basketRepository;
            _discountService = discountGRPCService;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
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
            foreach (var item in shoppingCart.Items)
            {
                var discount = await _discountService.GetDiscount(item.ProductId);
                item.Price -= discount.Amount;
            }
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


        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            var basket = await _basketRepository.GetShoppingCart(basketCheckout.UserName);
            if (basket == null)
            {
                return BadRequest();
            }

            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = (decimal) basket.TotalPrice;

            await _publishEndpoint.Publish(eventMessage);

            await _basketRepository.DeleteShoppingCart(basket.UserName);

            return Accepted();
        }
    
    
    }   
}
