﻿using AutoMapper;
using EventMangementLibrary.Events;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.API
{
    public class OrderingProfile : Profile
    {
        public OrderingProfile()
        {
            CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
