namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale
{
    /// <summary>
    /// API request model for canceling a sale.
    /// </summary>
    public class CancelSaleRequest
    {
        /// <summary>
        /// The unique identifier of the sale to be canceled.
        /// </summary>
        public Guid Id { get; set; }

    }
}