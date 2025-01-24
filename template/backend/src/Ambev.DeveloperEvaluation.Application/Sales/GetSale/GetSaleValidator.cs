﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Validator for GetSaleCommand
    /// </summary>
    public class GetSaleValidator : AbstractValidator<GetSaleCommand>
    {
        /// <summary>
        /// Initializes validation rules for GetSaleCommand
        /// </summary>
        public GetSaleValidator()
        {
            RuleFor(x => x.SaleNumber)
                .NotEmpty()
                .WithMessage("Sale Number is required");
        }
    }
}
