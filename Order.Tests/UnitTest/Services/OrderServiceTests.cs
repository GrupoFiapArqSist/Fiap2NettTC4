using AutoMapper;
using ComandaPro.CrossCutting.Notifications;
using FluentValidation.TestHelper;
using Moq;
using Order.Domain.Dtos;
using Order.Domain.Entities;
using Order.Domain.Enums;
using Order.Domain.Interfaces.Repositories;
using Order.Service.Services;
using Order.Service.Validators.Order;
using Xunit;

namespace Order.API.Tests.UnitTest.Services;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly Mock<IOrderItemsRepository> _orderItemsRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<NotificationContext> _notificationContextMock;
    private readonly OrderService _orderService;
    private readonly AddOrderValidator _validator;

    public OrderServiceTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _orderItemsRepositoryMock = new Mock<IOrderItemsRepository>();
        _mapperMock = new Mock<IMapper>();
        _notificationContextMock = new Mock<NotificationContext>();
        _orderService = new OrderService(
            _mapperMock.Object,
            _orderRepositoryMock.Object,
            _notificationContextMock.Object,
            _orderItemsRepositoryMock.Object);

        _validator = new AddOrderValidator();
    }

    [Fact]
    public async Task ValidAddOrderDto_ReturnsSuccessResponse()
    {
        // Arrange
        var addOrderDto = new AddOrderDto()
        {
            CommandId = 1,
            Observation = "teste",
            OrderItems = new List<OrderItemsDto>()
            {
                new()
                {
                    Amount = 1,
                    ProductId = 1,
                    ValueUnit = 1.00
                },
                new()
                {
                    Amount = 1,
                    ProductId = 1,
                    ValueUnit = 1.00
                }
            }
        };

        var order = new Domain.Entities.Order()
        {
            Id = 1,
            CreatedAt = DateTime.UtcNow,
            CommandId = 1,
            Observation = "teste",
            UserId = 1,
            ValueTotal = 0.00,
            Status = OrderStatusEnum.InPreparation,
            OrderItems = new List<OrderItems>()
            {
                new()
                {
                    Id = 1,
                    CreatedAt = DateTime.UtcNow,
                    OrderId = 1,
                    Amount = 1,
                    ProductId = 1,
                    ValueUnit = 1.00
                },
                new()
                {
                    Id = 2,
                    CreatedAt = DateTime.UtcNow,
                    OrderId = 1,
                    Amount = 1,
                    ProductId = 1,
                    ValueUnit = 1.00
                }
            }
        };

        _mapperMock.Setup(m => m.Map<Domain.Entities.Order>(It.IsAny<AddOrderDto>())).Returns(order);
        _orderRepositoryMock.Setup(r => r.InsertWithReturnId(It.IsAny<Domain.Entities.Order>())).ReturnsAsync(order);

        // Act
        var result = await _orderService.AddOrder(addOrderDto, 1);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(StaticNotifications.OrderSuccess.Message, result.Message);
    }

    [Fact]
    public async Task InvalidAddOrderDto_ReturnsDefaultValidation()
    {
        // Arrange
        var addOrderDto = new AddOrderDto() { };

        // Act
        var result = await _validator.TestValidateAsync(addOrderDto);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public async Task InsertFails_ReturnsErrorResponse()
    {
        // Arrange
        var addOrderDto = new AddOrderDto()
        {
            CommandId = 1,
            Observation = "teste",
            OrderItems = new List<OrderItemsDto>()
            {
                new()
                {
                    Amount = 1,
                    ProductId = 1,
                    ValueUnit = 1.00
                },
                new()
                {
                    Amount = 1,
                    ProductId = 1,
                    ValueUnit = 1.00
                }
            }
        };

        var order = new Domain.Entities.Order()
        {
            Id = 0,
            CreatedAt = DateTime.UtcNow,
            CommandId = 1,
            Observation = "teste",
            UserId = 1,
            ValueTotal = 0.00,
            Status = OrderStatusEnum.InPreparation,
            OrderItems = new List<OrderItems>()
            {
                new()
                {
                    Id = 1,
                    CreatedAt = DateTime.UtcNow,
                    OrderId = 1,
                    Amount = 1,
                    ProductId = 1,
                    ValueUnit = 1.00
                },
                new()
                {
                    Id = 2,
                    CreatedAt = DateTime.UtcNow,
                    OrderId = 1,
                    Amount = 1,
                    ProductId = 1,
                    ValueUnit = 1.00
                }
            }
        };

        _mapperMock.Setup(m => m.Map<Domain.Entities.Order>(addOrderDto)).Returns(order);
        _orderRepositoryMock.Setup(r => r.InsertWithReturnId(order)).ReturnsAsync((Domain.Entities.Order)null);

        // Act
        var result = await _orderService.AddOrder(addOrderDto, 1);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Equal(StaticNotifications.OrderError.Message, result.Message);
    }
}