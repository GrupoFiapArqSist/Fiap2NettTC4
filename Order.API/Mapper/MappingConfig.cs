using AutoMapper;
using Order.Domain.Dtos;
using Order.Domain.Entities;

namespace Order.API.Mapper;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<OrderDto, Domain.Entities.Order>().ReverseMap();
        CreateMap<AddOrderDto, Domain.Entities.Order>().ReverseMap();
        CreateMap<OrderItemsDto, OrderItems>().ReverseMap();
    }
}
