using AutoMapper;
using ComandaPro.CrossCutting.Notifications;
using Moq;
using Product.Domain.Dtos.Category;
using Product.Domain.Entities;
using Product.Domain.Interfaces.Repositories;
using Product.Service.Services;

public class CategoryUnitTests
{
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly NotificationContext _notificationContext;
    private readonly CategoryService _categoryService;

    public CategoryUnitTests()
    {
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _mapperMock = new Mock<IMapper>();
        _notificationContext = new NotificationContext();
        _categoryService = new CategoryService(
            _mapperMock.Object,
            _notificationContext,
            _categoryRepositoryMock.Object,
            _productRepositoryMock.Object
        );
    }

    [Fact]
    public async Task AddCategoryAsync_ShouldAddCategory_WhenValidDto()
    {
        var addCategoryDto = new AddCategoryDto
        {
            Name = "New Category"
        };

        _categoryRepositoryMock.Setup(repo => repo.ExistsActiveCategory(It.IsAny<string>())).ReturnsAsync((Category)null);
        _mapperMock.Setup(mapper => mapper.Map<Category>(It.IsAny<AddCategoryDto>())).Returns(new Category 
        { 
            Id = 1, 
            Name = "New Category" 
        });

        var result = await _categoryService.AddCategoryAsync(addCategoryDto, 1, "token");

        Assert.True(result.Success);
        _categoryRepositoryMock.Verify(repo => repo.Insert(It.IsAny<Category>()), Times.Once);
    }

    [Fact]
    public async Task AddCategoryAsync_ShouldReturnError_WhenCategoryAlreadyExists()
    {
        var addCategoryDto = new AddCategoryDto
        {
            Name = "New Category"
        };

        _categoryRepositoryMock.Setup(repo => repo.ExistsActiveCategory(It.IsAny<string>())).ReturnsAsync(new Category());

        var result = await _categoryService.AddCategoryAsync(addCategoryDto, 1, "token");

        Assert.False(result.Success);
        _categoryRepositoryMock.Verify(repo => repo.Insert(It.IsAny<Category>()), Times.Never);
    }
}
