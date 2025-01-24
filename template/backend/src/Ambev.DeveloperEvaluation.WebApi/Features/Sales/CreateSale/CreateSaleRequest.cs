namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Represents a request to create a new sale in the system.
    /// </summary>
    public class CreateSaleRequest
    {
        /// <summary>
        /// Gets or sets the sale date. The date when the sale was made.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets or sets the customer name associated with the sale.
        /// </summary>
        public string Customer { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the branch where the sale was made.
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the sale items in the transaction.
        /// </summary>
        public List<SaleItemRequest> SaleItems { get; set; } = new List<SaleItemRequest>();

        /// <summary>
        /// Initializes a new instance of CreateSaleRequest with default values.
        /// </summary>
        public CreateSaleRequest()
        {
            SaleDate = DateTime.Now;
        }
    }

    /// <summary>
    /// Represents a request to create a sale item in the system.
    /// </summary>
    public class SaleItemRequest
    {
        /// <summary>
        /// Gets or sets the product name for the sale item.
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product sold.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}
