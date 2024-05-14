using AutoMapper;
using Product.Domain.Dtos.Category;
using Product.Domain.Dtos.Product;

namespace Product.API.Mapper;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            // Category
            config.CreateMap<AddCategoryDto, Domain.Entities.Category>().ReverseMap();
            config.CreateMap<CategoryDto, Domain.Entities.Category>().ReverseMap();

            // Product
            config.CreateMap<AddProductDto, Domain.Entities.Product>().ReverseMap();
            config.CreateMap<ProductDto, Domain.Entities.Product>().ReverseMap();
            config.CreateMap<UpdateProductDto, Domain.Entities.Product>().ReverseMap();
        });
        return mappingConfig;
    }
}
