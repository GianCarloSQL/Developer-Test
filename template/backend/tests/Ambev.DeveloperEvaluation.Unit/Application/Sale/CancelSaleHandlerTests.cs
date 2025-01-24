using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale
{
    public class CancelSaleHandlerTests
    {
        private readonly Mock<ISaleRepository> _mockSaleRepository;
        private readonly CancelSaleHandler _handler;

        public CancelSaleHandlerTests()
        {
            _mockSaleRepository = new Mock<ISaleRepository>();
            _handler = new CancelSaleHandler(_mockSaleRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenSaleIsCanceled()
        {
            // Arrange
            var command = new CancelSaleCommand(Guid.NewGuid());
            _mockSaleRepository.Setup(repo => repo.CancelAsync(command.Id, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Handle_ShouldThrowKeyNotFoundException_WhenSaleNotFound()
        {
            // Arrange
            var command = new CancelSaleCommand(Guid.NewGuid());
            _mockSaleRepository.Setup(repo => repo.CancelAsync(command.Id, It.IsAny<CancellationToken>())).ReturnsAsync(false);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal($"Sale with ID {command.Id} not found", exception.Message);
        }
    }
}
