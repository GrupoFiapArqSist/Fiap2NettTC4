using AutoMapper;
using Product.Domain.Dtos.Category;
using Product.Domain.Dtos.Product;
using Product.Domain.Entities;

namespace Product.API.Mapper;

public class MappingConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category
            CreateMap<AddCategoryDto, Category>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();

            // Product
            CreateMap<AddProductDto, Domain.Entities.Product>().ReverseMap();
            CreateMap<ProductDto, Domain.Entities.Product>().ReverseMap();
            CreateMap<UpdateProductDto, Domain.Entities.Product>().ReverseMap();
        }
    }
}
