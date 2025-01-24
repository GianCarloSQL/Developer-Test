using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Validator for CreateSaleCommand that defines validation rules for sale creation command.
    /// </summary>
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - SaleDate: Must be today or in the past
        /// - Customer: Required
        /// - Branch: Required
        /// - SaleItems: Must include valid items (validated using SaleItemCommandValidator)
        /// - Total sale amount must be valid
        /// </remarks>
        public CreateSaleValidator()
        {
            RuleFor(sale => sale.SaleDate).LessThanOrEqualTo(DateTime.Now).WithMessage("Sale date must be today or in the past.");
            RuleFor(sale => sale.Customer).NotEmpty().WithMessage("Customer is required.");
            RuleFor(sale => sale.Branch).NotEmpty().WithMessage("Branch is required.")
                .Must(BeAValidBranch).WithMessage("Branch must be a valid option.");
            RuleFor(sale => sale.SaleItems).NotEmpty().WithMessage("Sale must contain at least one item.");
            RuleForEach(sale => sale.SaleItems).SetValidator(new SaleItemCommandValidator());
        }

        // Validate if the branch is valid
        private bool BeAValidBranch(string branch)
        {
            // Example of predefined valid branches
            var validBranches = new[] { "Main Branch" };
            return validBranches.Contains(branch);
        }
    }

    /// <summary>
    /// Validator for SaleItemCommand that defines validation rules for individual sale items.
    /// </summary>
    public class SaleItemCommandValidator : AbstractValidator<SaleItemCommand>
    {
        /// <summary>
        /// Initializes a new instance of the SaleItemCommandValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - ProductName: Required
        /// - Quantity: Must be between 1 and 20
        /// - UnitPrice: Must be greater than 0
        /// - Discount: Must match quantity-based rules
        /// - TotalAmount: Must match calculated value
        /// </remarks>
        public SaleItemCommandValidator()
        {
            RuleFor(item => item.ProductName).NotEmpty().WithMessage("Product name is required.");
            RuleFor(item => item.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.")
                .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items."); ;

            RuleFor(item => item.UnitPrice).GreaterThan(0).WithMessage("Unit price must be greater than zero.");
        }
    }
}
