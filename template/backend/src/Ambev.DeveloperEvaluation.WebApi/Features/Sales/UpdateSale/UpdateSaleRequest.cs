namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// API request model for updating a sale
    /// </summary>
    public class UpdateSaleRequest
    {
        /// <summary>
        /// The name or description of the sale
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The total amount of the sale
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// The status of the sale
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// The date and time of the sale
        /// </summary>
        public DateTime SaleDate { get; set; }
    }
}
