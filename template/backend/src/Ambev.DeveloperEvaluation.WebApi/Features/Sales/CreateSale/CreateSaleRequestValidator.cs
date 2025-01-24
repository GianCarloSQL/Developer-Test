using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Validator for CreateSaleRequest that defines validation rules for sale creation.
    /// </summary>
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        /// <summary>
        /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - SaleDate: Must be a valid date (in the past or present)
        /// - Customer: Required and not empty
        /// - Branch: Required and should be a valid branch from a predefined list
        /// - SaleItems: Cannot be empty, must contain valid products, quantities, unit prices, and discounts
        /// - TotalSaleAmount: Must be greater than 0
        /// - IsCancelled: Default value is false; valid boolean check
        /// </remarks>
        public CreateSaleRequestValidator()
        {
            RuleFor(sale => sale.SaleDate).LessThanOrEqualTo(DateTime.Now).WithMessage("Sale date must be today or in the past.");
            RuleFor(sale => sale.Customer).NotEmpty().WithMessage("Customer is required.");
            RuleFor(sale => sale.Branch).NotEmpty().WithMessage("Branch is required.")
                .Must(BeAValidBranch).WithMessage("Branch must be a valid option.");
            RuleFor(sale => sale.SaleItems).NotEmpty().WithMessage("Sale must contain at least one item.");
            RuleForEach(sale => sale.SaleItems).SetValidator(new SaleItemValidator());
        }

        // Validate if the branch is valid
        private bool BeAValidBranch(string branch)
        {
            // Example of predefined valid branches
            var validBranches = new[] { "Main Branch" };
            return validBranches.Contains(branch);
        }
    }

    public class SaleItemValidator : AbstractValidator<SaleItemRequest>
    {
        public SaleItemValidator()
        {
            RuleFor(item => item.ProductName).NotEmpty().WithMessage("Product name is required.");
            RuleFor(item => item.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.")
                .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items."); ;

            RuleFor(item => item.UnitPrice).GreaterThan(0).WithMessage("Unit price must be greater than zero.");
        }
    }
}
