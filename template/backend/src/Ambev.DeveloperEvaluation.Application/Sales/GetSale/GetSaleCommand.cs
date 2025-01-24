﻿using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Command for retrieving a sale by their number sale
    /// </summary>
    public class GetSaleCommand : IRequest<GetSaleResult>
    {
        /// <summary>
        /// The sale  number identifier of the sale to retrieve
        /// </summary>
        public long SaleNumber { get; }

        /// <summary>
        /// Initializes a new instance of GetSaleCommand
        /// </summary>
        /// <param name="saleNumber">The sale number of the sale to retrieve</param>
        public GetSaleCommand(long saleNumber)
        {
            SaleNumber = saleNumber;
        }
    }
}
