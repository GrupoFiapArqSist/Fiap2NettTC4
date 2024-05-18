using AutoMapper;
using ComandaPro.CrossCutting.Notifications;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Domain.Filters;
using Product.Infra.Data.Context;
using Product.Infra.Data.Repositories;
using Product.Service.Services;
using static Product.API.Mapper.MappingConfig;

public class CategoryIntegrationTests
{
    private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

    public CategoryIntegrationTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Fact]
    public async Task GetAllCategoriesAsync_ShouldReturnCategories_WhenCategoriesExist()
    {
        using var context = new ApplicationDbContext(_dbContextOptions);
        context.Category.Add(new Category { Id = 1, Name = "Category 1", IsActive = true });
        context.Category.Add(new Category { Id = 2, Name = "Category 2", IsActive = true });
        context.SaveChanges();

        var categoryService = new CategoryService(
            new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())),
            new NotificationContext(),
            new CategoryRepository(context),
            new ProductRepository(context)
        );

        var result = await categoryService.GetAllCategoriesAsync(new CategoryFilter());

        Assert.Equal(2, result.Count);
    }
}
