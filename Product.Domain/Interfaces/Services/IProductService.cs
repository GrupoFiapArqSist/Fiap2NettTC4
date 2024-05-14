using ComandaPro.Domain.Dtos.Default;
using Product.Domain.Dtos.Product;
using Product.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<DefaultServiceResponseDto> AddProductAsync(AddProductDto dto, int userId, string accessToken);
        Task<DefaultServiceResponseDto> DeleteProductAsync(int productId);
        Task<ICollection<ProductDto>> GetAllProductsAsync(ProductFilter filter);
        Task<ProductDto> GetProductAsync(int productId);
        Task<DefaultServiceResponseDto> UpdateProductAsync(UpdateProductDto dto);
    }
}
