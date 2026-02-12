using Ecommerce.Exceptions;
using Ecommerce.Repositories;
using Ecommerce.Services;
using Moq;

namespace Ecommerce.Tests;

public class CartServiceTests
{
    private readonly Mock<ICartRepository> _cartRepositoryMock = new();

    [Fact]
    public async Task AddItemAsync_InvalidBookId_ThrowsValidationException()
    {
        var sut = new CartService(_cartRepositoryMock.Object);

        await Assert.ThrowsAsync<ValidationException>(() => sut.AddItemAsync(0, 1));
    }

    [Fact]
    public async Task AddItemAsync_InvalidQuantity_ThrowsValidationException()
    {
        var sut = new CartService(_cartRepositoryMock.Object);

        await Assert.ThrowsAsync<ValidationException>(() => sut.AddItemAsync(10, 0));
    }

    [Fact]
    public async Task AddItemAsync_ValidInput_DelegatesToRepository()
    {
        _cartRepositoryMock
            .Setup(x => x.AddItem(3, 2))
            .ReturnsAsync(4);

        var sut = new CartService(_cartRepositoryMock.Object);

        var result = await sut.AddItemAsync(3, 2);

        Assert.Equal(4, result);
        _cartRepositoryMock.Verify(x => x.AddItem(3, 2), Times.Once);
    }

    [Fact]
    public async Task CheckoutAsync_DelegatesToRepository()
    {
        _cartRepositoryMock
            .Setup(x => x.Checkout())
            .Returns(Task.CompletedTask);

        var sut = new CartService(_cartRepositoryMock.Object);

        await sut.CheckoutAsync();

        _cartRepositoryMock.Verify(x => x.Checkout(), Times.Once);
    }
}
