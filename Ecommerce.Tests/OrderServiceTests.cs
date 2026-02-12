using Ecommerce.Models;
using Ecommerce.Repositories;
using Ecommerce.Services;
using Moq;

namespace Ecommerce.Tests;

public class OrderServiceTests
{
    [Fact]
    public async Task GetUserOrdersAsync_ReturnsRepositoryOrders()
    {
        var orders = new List<Order>
        {
            new Order { Id = 1, UserId = "user-1", OrderStatusId = 1 }
        };

        var repo = new Mock<IUserOrderRepository>();
        repo.Setup(x => x.UserOrders()).ReturnsAsync(orders.AsEnumerable());

        var sut = new OrderService(repo.Object);

        var result = await sut.GetUserOrdersAsync();

        Assert.Single(result);
        Assert.Equal(1, result.First().Id);
        repo.Verify(x => x.UserOrders(), Times.Once);
    }
}
