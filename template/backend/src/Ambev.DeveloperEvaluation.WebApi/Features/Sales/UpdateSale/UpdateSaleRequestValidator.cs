using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Validator for UpdateSaleRequest
    /// </summary>
    public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
    {
        /// <summary>
        /// Initializes validation rules for UpdateSaleRequest
        /// </summary>
        public UpdateSaleRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Sale name is required.")
                .MaximumLength(100)
                .WithMessage("Sale name cannot exceed 100 characters.");

            RuleFor(x => x.TotalAmount)
                .GreaterThan(0)
                .WithMessage("Total amount must be greater than zero.");

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Sale status is required.")
                .Must(status => status == 0 || status == 1 || status == 2)
                .WithMessage("Sale status is invalid.");

            RuleFor(x => x.SaleDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Sale date cannot be in the future.");
        }
    }
}