using AutoMapper;
using ComandaPro.CrossCutting.Notifications;
using Microsoft.EntityFrameworkCore;
using Product.Infra.Data.Context;
using Product.Infra.Data.Repositories;
using Product.Service.Services;
using static Product.API.Mapper.MappingConfig;

public class ProductServiceIntegrationTests
{
    private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

    public ProductServiceIntegrationTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Fact]
    public async Task GetProductAsync_ShouldReturnProduct_WhenProductExists()
    {
        using var context = new ApplicationDbContext(_dbContextOptions);
        var product = new Product.Domain.Entities.Product 
        { 
            Id = 1, 
            Name = "Test Product", 
            Description = "Description",
            Price = 10.0m, 
            CategoryId = 1 
        };
        context.Product.Add(product);
        await context.SaveChangesAsync();

        var productService = new ProductService(
            new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())),
            new ProductRepository(context),
            new NotificationContext(),
            new CategoryRepository(context)
        );

        var result = await productService.GetProductAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Test Product", result.Name);
    }
}
