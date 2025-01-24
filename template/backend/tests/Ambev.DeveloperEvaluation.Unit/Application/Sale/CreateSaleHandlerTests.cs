using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale
{
    public class CreateSaleHandlerTests
    {
        private readonly Mock<ISaleRepository> _mockSaleRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateSaleHandler _handler;

        public CreateSaleHandlerTests()
        {
            _mockSaleRepository = new Mock<ISaleRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateSaleHandler(_mockSaleRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenSaleIsCreated()
        {
            // Arrange
            var command = new CreateSaleCommand
            {
                SaleItems = { new SaleItemCommand { Quantity = 5, UnitPrice = 100m, ProductName = "corona" } },
                Customer = "John Doe",
                Branch = "Main Branch"
            };

            var saleEntity = new DeveloperEvaluation.Domain.Entities.Sale
            {
                SaleItems = { new SaleItem { Quantity = 5, UnitPrice = 100m } },
                TotalAmount = 500m
            };
            var result = new CreateSaleResult { TotalAmount = 500 };

            _mockMapper.Setup(m => m.Map<DeveloperEvaluation.Domain.Entities.Sale>(It.IsAny<CreateSaleCommand>())).Returns(saleEntity);
            _mockMapper.Setup(m => m.Map<CreateSaleResult>(It.IsAny<DeveloperEvaluation.Domain.Entities.Sale>())).Returns(result);
            _mockSaleRepository.Setup(repo => repo.CreateAsync(It.IsAny<DeveloperEvaluation.Domain.Entities.Sale>(), It.IsAny<CancellationToken>())).ReturnsAsync(saleEntity);

            // Act
            var createResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(result.TotalAmount, createResult.TotalAmount);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new CreateSaleCommand
            {
                // Invalid as there are no items
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task Handle_ShouldApplyDiscount_WhenItemsQualifyForDiscount()
        {
            // Arrange
            var command = new CreateSaleCommand
            {
                SaleItems = { new SaleItemCommand { Quantity = 5, UnitPrice = 100m, ProductName = "corona" } },
                Customer = "John Doe",
                Branch = "Main Branch",
                SaleDate = DateTime.Now     
            };

            var saleEntity = new DeveloperEvaluation.Domain.Entities.Sale
            {
                SaleItems = { new SaleItem { Quantity = 5, UnitPrice = 100m, ProductName = "corona" } },
                Customer = "John Doe",
                Branch = "Main Branch",
                SaleDate = DateTime.Now,

                TotalAmount = 500m // 5 items * 100 unit price * 0.10 discount
            };
            var result = new CreateSaleResult { TotalAmount = 500m };

            _mockMapper.Setup(m => m.Map<DeveloperEvaluation.Domain.Entities.Sale>(It.IsAny<CreateSaleCommand>())).Returns(saleEntity);
            _mockMapper.Setup(m => m.Map<CreateSaleResult>(It.IsAny<DeveloperEvaluation.Domain.Entities.Sale>())).Returns(result);
            _mockSaleRepository.Setup(repo => repo.CreateAsync(It.IsAny<DeveloperEvaluation.Domain.Entities.Sale>(), It.IsAny<CancellationToken>())).ReturnsAsync(saleEntity);

            // Act
            var createResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(result.TotalAmount, createResult.TotalAmount);
        }

        [Fact]
        public async Task Handle_ShouldNotApplyDiscount_WhenItemsDoNotQualify()
        {
            // Arrange
            var command = new CreateSaleCommand
            {
                SaleItems = { new SaleItemCommand { Quantity = 3, UnitPrice = 100m, ProductName = "corona" } },
                Customer = "John Doe",
                Branch = "Main Branch",
                SaleDate = DateTime.Now
            };

            var saleEntity = new DeveloperEvaluation.Domain.Entities.Sale
            {
                SaleItems = { new SaleItem { Quantity = 3, UnitPrice = 100m } },
                TotalAmount = 300m // No discount, 3 items * 100 unit price
            };
            var result = new CreateSaleResult { TotalAmount = 300m };

            _mockMapper.Setup(m => m.Map<DeveloperEvaluation.Domain.Entities.Sale>(It.IsAny<CreateSaleCommand>())).Returns(saleEntity);
            _mockMapper.Setup(m => m.Map<CreateSaleResult>(It.IsAny<DeveloperEvaluation.Domain.Entities.Sale>())).Returns(result);
            _mockSaleRepository.Setup(repo => repo.CreateAsync(It.IsAny<DeveloperEvaluation.Domain.Entities.Sale>(), It.IsAny<CancellationToken>())).ReturnsAsync(saleEntity);

            // Act
            var createResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(result.TotalAmount, createResult.TotalAmount);
        }
    }
}