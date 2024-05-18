using ComandaPro.Domain.Dtos.Default;
using Product.Domain.Dtos.Category;
using Product.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<DefaultServiceResponseDto> AddCategoryAsync(AddCategoryDto dto, int userId, string accessToken);
        Task<DefaultServiceResponseDto> DeleteCategoryAsync(int categoryId);
        Task<ICollection<CategoryDto>> GetAllCategoriesAsync(CategoryFilter filter);
    }
}
