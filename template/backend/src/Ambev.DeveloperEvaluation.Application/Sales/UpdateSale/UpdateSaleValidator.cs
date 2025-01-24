using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Validator for UpdateSaleCommand that defines validation rules for updating a sale.
    /// </summary>
    public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateSaleValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - SaleId: Required
        /// - SaleDate: Must be today or in the past
        /// - Customer: Required
        /// - Branch: Required
        /// - SaleItems: Must include valid items (validated using SaleItemCommandValidator)
        /// - Total sale amount must be valid
        /// </remarks>
        public UpdateSaleValidator()
        {
            RuleFor(sale => sale.SaleId)
                .NotEmpty().WithMessage("Sale ID is required.");

            RuleFor(sale => sale.SaleDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Sale date must be today or in the past.");

            RuleFor(sale => sale.Customer)
                .NotEmpty().WithMessage("Customer is required.");

            RuleFor(sale => sale.Branch)
                .NotEmpty().WithMessage("Branch is required.")
                .Must(BeAValidBranch).WithMessage("Branch must be a valid option.");

            RuleFor(sale => sale.SaleItems)
                .NotEmpty().WithMessage("Sale must contain at least one item.");

            RuleForEach(sale => sale.SaleItems)
                .SetValidator(new SaleItemCommandValidator());

            RuleFor(sale => sale.TotalSaleAmount)
                .GreaterThan(0).WithMessage("Total sale amount must be greater than 0.");
        }

        // Validate if the branch is valid
        private bool BeAValidBranch(string branch)
        {
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
        /// - Discount: Cannot be negative
        /// - TotalAmount: Must be greater than 0
        /// </remarks>
        public SaleItemCommandValidator()
        {
            RuleFor(item => item.ProductName)
                .NotEmpty().WithMessage("Product name is required.");

            RuleFor(item => item.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
                .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items.");

            RuleFor(item => item.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than zero.");

            RuleFor(item => item.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount cannot be negative.");

            RuleFor(item => item.TotalAmount)
                .GreaterThan(0).WithMessage("Total amount must be greater than zero.");
        }
    }
}
