using AutoMapper;
using ComandaPro.CrossCutting.Notifications;
using ComandaPro.Domain.Dtos.Default;
using ComandaPro.Domain.Extensions;
using ComandaPro.Service.Services;
using Microsoft.Extensions.Logging;
using Product.Domain.Dtos.Category;
using Product.Domain.Entities;
using Product.Domain.Filters;
using Product.Domain.Interfaces.Repositories;
using Product.Domain.Interfaces.Services;
using Product.Service.Validators.Category;

namespace Product.Service.Services
{
    public class CategoryService(IMapper mapper, NotificationContext notificationContext, ICategoryRepository categoryRepository, IProductRepository productRepository) : BaseService, ICategoryService
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly NotificationContext _notificationContext = notificationContext;

        public async Task<DefaultServiceResponseDto> AddCategoryAsync(AddCategoryDto dto, int userId, string accessToken)
        {
            var validationResult = Validate(dto, Activator.CreateInstance<AddCategoryValidator>());
            if (!validationResult.IsValid) { _notificationContext.AddNotifications(validationResult.Errors); return default; }

            var existingCategory = await _categoryRepository.ExistsActiveCategory(dto.Name);

            if (existingCategory != null)
            {
                _notificationContext.AddNotification(StaticNotifications.CategoryAlreadyExists);
                return new DefaultServiceResponseDto { Success = false };
            }

            var newCategoryDb = _mapper.Map<Category>(dto);

            newCategoryDb.IsActive = true;
            newCategoryDb.CreatedAt = DateTime.Now;
            newCategoryDb.UserId = userId;

            await _categoryRepository.Insert(newCategoryDb);

            if (newCategoryDb.Id > 0)
                return new DefaultServiceResponseDto()
                {
                    Message = StaticNotifications.CategorySucess.Message,
                    Success = true
                };
            else
                return new DefaultServiceResponseDto()
                {
                    Message = StaticNotifications.CategoryError.Message,
                    Success = false
                };
        }

        public async Task<ICollection<CategoryDto>> GetAllCategoriesAsync(CategoryFilter filter)
        {
            var categories = (await _categoryRepository.Select())
              .AsQueryable()
              .OrderByDescending(p => p.CreatedAt)
              .ApplyFilter(filter)
              .Where(p => p.IsActive);

            if (filter.Name is not null)
                categories = categories.Where(t => t.Name == filter.Name);

            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<DefaultServiceResponseDto> DeleteCategoryAsync(int categoryId)
        {
            var categoryDb = await _categoryRepository.Select(categoryId);

            if (categoryDb is null)
            {
                return new DefaultServiceResponseDto()
                {
                    Message = StaticNotifications.CategoryNotExists.Message,
                    Success = false
                };
            }
            else
            {
                var products = await _productRepository.Select(p => p.CategoryId.Equals(categoryId) && p.IsActive);

                if (products.Any())
                {
                    _notificationContext.AddNotification(StaticNotifications.CategoryConflict);
                    return default;
                }

                await _categoryRepository.Delete(categoryId);

                return new DefaultServiceResponseDto()
                {
                    Message = StaticNotifications.CategoryDeleteSucess.Message,
                    Success = true
                };
            }
        }
    }
}
