using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Handler for processing CreateSaleCommand requests.
    /// </summary>
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of CreateSaleHandler.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the CreateSaleCommand request.
        /// </summary>
        /// <param name="command">The CreateSale command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created sale details.</returns>
        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            // Validate the command using the validator.
            var validator = new CreateSaleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            // Map the command to the Sale entity.
            var sale = _mapper.Map<Sale>(command);

            // Calculate and update total amount and discount if necessary.
            foreach (var item in sale.SaleItems)
            {
                if (item.Quantity >= 4 && item.Quantity <= 9)
                {
                    sale.TotalAmount = item.Quantity * item.UnitPrice * 0.10m;
                }
                else if (item.Quantity >= 10 && item.Quantity <= 20)
                {
                    sale.TotalAmount = item.Quantity * item.UnitPrice * 0.20m;
                }
            }

            // Save the sale to the repository.
            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

            // Map the created Sale entity to the result DTO.
            var result = _mapper.Map<CreateSaleResult>(createdSale);
            return result;
        }
    }
}
