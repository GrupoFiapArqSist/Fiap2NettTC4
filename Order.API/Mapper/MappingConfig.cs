using AutoMapper;
using Order.Domain.Dtos;
using Order.Domain.Entities;

namespace Order.API.Mapper;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<OrderDto, Domain.Entities.Order>().ReverseMap();
            config.CreateMap<AddOrderDto, Domain.Entities.Order>().ReverseMap();
            config.CreateMap<OrderItemsDto, OrderItems>().ReverseMap();
        });
        return mappingConfig;
    }
}
