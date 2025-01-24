using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Response model for the GetSale operation
    /// </summary>
    public class GetSaleResult
    {
        /// <summary>
        /// The unique identifier of the sale
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the product sold
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// The quantity of the product sold
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The total price of the sale
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// The discount applied to the sale
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// The status of the sale
        /// </summary>
        public SaleStatus Status { get; set; }

        /// <summary>
        /// The date and time when the sale was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The date and time when the sale was last updated
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}