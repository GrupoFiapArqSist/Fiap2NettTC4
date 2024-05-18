using AutoMapper;
using ComandaPro.CrossCutting.Notifications;
using ComandaPro.Domain.Dtos.Default;
using ComandaPro.Domain.Extensions;
using ComandaPro.Service.Services;
using Product.Domain.Dtos.Product;
using Product.Domain.Entities;
using Product.Domain.Filters;
using Product.Domain.Interfaces.Repositories;
using Product.Domain.Interfaces.Services;
using Product.Service.Validators.Product;

namespace Product.Service.Services
{
    public class ProductService(IMapper mapper, IProductRepository productRepository, NotificationContext notificationContext, ICategoryRepository categoryRepository) : BaseService, IProductService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly NotificationContext _notificationContext = notificationContext;

        public async Task<DefaultServiceResponseDto> AddProductAsync(AddProductDto dto, int userId, string accessToken)
        {
            var validationResult = Validate(dto, Activator.CreateInstance<AddProductValidator>());
            if (!validationResult.IsValid) { _notificationContext.AddNotifications(validationResult.Errors);
                return new DefaultServiceResponseDto { Success = false };
            }
           
            if (await _productRepository.ExistsByName(dto.Name) is not null)
            {
                _notificationContext.AddNotification(StaticNotifications.ProductAlreadyExists);
                return new DefaultServiceResponseDto { Success = false };
            }
            else if (await _categoryRepository.Select(dto.CategoryId) is null)
            {
                _notificationContext.AddNotification(StaticNotifications.CategoryNotExists);
                return new DefaultServiceResponseDto { Success = false };
            }

            var newProductDb = _mapper.Map<Domain.Entities.Product>(dto);

            newProductDb.IsActive = true;
            newProductDb.CreatedAt = DateTime.Now;
            newProductDb.UserId = userId;

            await _productRepository.Insert(newProductDb);

            if (newProductDb.Id > 0)
                return new DefaultServiceResponseDto()
                {
                    Message = StaticNotifications.ProductSucess.Message,
                    Success = true
                };
            else
                return new DefaultServiceResponseDto()
                {
                    Message = StaticNotifications.ProductError.Message,
                    Success = false
                };
        }

        public async Task<DefaultServiceResponseDto> UpdateProductAsync(UpdateProductDto dto)
        {
            var validationResult = Validate(dto, Activator.CreateInstance<UpdateProducValidator>());
            if (!validationResult.IsValid) { _notificationContext.AddNotifications(validationResult.Errors); return default; }

            var productDb = await _productRepository.Select(dto.Id);

            if (productDb is null)
            {
                _notificationContext.AddNotification(StaticNotifications.ProductNotExists);
                return default;
            }

            productDb.Name = dto.Name;
            productDb.Description = dto.Description;
            productDb.CategoryId = dto.CategoryId;
            productDb.Price = dto.Price;

            await _productRepository.Update(productDb);

            return new DefaultServiceResponseDto
            {
                Success = true,
                Message = string.Format(StaticNotifications.ProductUpdateSucess.Message)
            };
        }

        public async Task<ProductDto> GetProductAsync(int productId)
        {
            var product = _mapper.Map<ProductDto>(await _productRepository.Select(productId));

            return product;
        }

        public async Task<ICollection<ProductDto>> GetAllProductsAsync(ProductFilter filter)
        {
            var products = (await _productRepository.Select())
                .Join(
                    await _categoryRepository.Select(),
                    product => product.CategoryId,
                    category => category.Id,
                    (product, category) => new
                    {
                        Product = product,
                        Category = category
                    }
                )
                .AsQueryable()
                .OrderByDescending(p => p.Product.CreatedAt)
                .ApplyFilter(filter)
                .Where(p => p.Product.IsActive);

            if (filter.Name is not null)
                products = products.Where(t => t.Product.Name == filter.Name);
            else if (filter.CategoryId > 0)
                products = products.Where(t => t.Product.CategoryId == filter.CategoryId);

            return _mapper.Map<List<ProductDto>>(products.Select(p => new ProductDto
            {
                Id = p.Product.Id,
                Name = p.Product.Name,
                Description = p.Product.Description,
                Price = p.Product.Price,
                CategoryId = p.Product.CategoryId,
                CategoryName = p.Category.Name
                                                
            }));
        }


        public async Task<DefaultServiceResponseDto> DeleteProductAsync(int productId)
        {
            var productDb = await _productRepository.Select(productId);

            if (productDb is null)
            {
                return new DefaultServiceResponseDto()
                {
                    Message = StaticNotifications.ProductNotExists.Message,
                    Success = false
                };
            }
            else
            {
                await _productRepository.Delete(productId);

                return new DefaultServiceResponseDto()
                {
                    Message = StaticNotifications.ProductDeleteSucess.Message,
                    Success = true
                };
            }
        }
    }
}
