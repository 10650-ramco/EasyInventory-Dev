namespace Application.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int LowStockThreshold { get; set; }
        public string Status { get; set; }
    }
}
