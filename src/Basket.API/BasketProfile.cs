using AutoMapper;
using Basket.API.Controllers;
using EventMangementLibrary.Events;

namespace Basket.API
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
