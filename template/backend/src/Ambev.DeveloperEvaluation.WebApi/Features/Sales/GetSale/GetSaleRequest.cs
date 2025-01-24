namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    /// <summary>
    /// API request model for retrieving a sale by its unique identifier.
    /// </summary>
    public class GetSaleRequest
    {
        /// <summary>
        /// The unique identifier of the sale to be retrieved.
        /// </summary>
        public int SaleNumber { get; set; }
    }
}