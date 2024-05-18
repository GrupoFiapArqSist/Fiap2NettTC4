using AutoMapper;
using ComandaPro.CrossCutting.Notifications;
using Moq;
using Product.Domain.Dtos.Product;
using Product.Domain.Entities;
using Product.Domain.Interfaces.Repositories;
using Product.Service.Services;

public class ProductUnitTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly NotificationContext _notificationContext;
    private readonly ProductService _productService;

    public ProductUnitTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _mapperMock = new Mock<IMapper>();
        _notificationContext = new NotificationContext();
        _productService = new ProductService(
            _mapperMock.Object,
            _productRepositoryMock.Object,
            _notificationContext,
            _categoryRepositoryMock.Object
        );
    }

    [Fact]
    public async Task AddProductAsync_ShouldAddProduct_WhenValidDto()
    {
        var addProductDto = new AddProductDto
        {
            Name = "New Product",
            CategoryId = 1,
            Description = "Description",
            Price = 10.0m
        };

        _productRepositoryMock.Setup(repo => repo.ExistsByName(It.IsAny<string>())).ReturnsAsync((Product.Domain.Entities.Product)null);
        _categoryRepositoryMock.Setup(repo => repo.Select(It.IsAny<int>())).ReturnsAsync(new Category { Id = 1, Name = "Test Category" });
        _mapperMock.Setup(mapper => mapper.Map<Product.Domain.Entities.Product>(It.IsAny<AddProductDto>())).Returns(new Product.Domain.Entities.Product 
        { 
            Id = 1, 
            Name = "New Product", 
            CategoryId = 1 
        });

        var result = await _productService.AddProductAsync(addProductDto, 1, "token");

        Assert.True(result.Success);
        _productRepositoryMock.Verify(repo => repo.Insert(It.IsAny<Product.Domain.Entities.Product>()), Times.Once);
    }

    [Fact]
    public async Task AddProductAsync_ShouldReturnError_WhenProductAlreadyExists()
    {
        var addProductDto = new AddProductDto
        {
            Name = "Existing Product",
            CategoryId = 1,
            Description = "Description",
            Price = 10.0m
        };

        _productRepositoryMock.Setup(repo => repo.ExistsByName(It.IsAny<string>())).ReturnsAsync(new Product.Domain.Entities.Product());

        var result = await _productService.AddProductAsync(addProductDto, 1, "token");

        Assert.False(result.Success);
        _productRepositoryMock.Verify(repo => repo.Insert(It.IsAny<Product.Domain.Entities.Product>()), Times.Never);
    }
}
