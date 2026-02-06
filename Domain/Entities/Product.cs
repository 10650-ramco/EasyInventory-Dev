namespace Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int LowStockThreshold { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        public string GetStatus()
        {
            if (Stock == 0)
                return "Out of Stock";
            if (Stock <= LowStockThreshold)
                return "Low Stock";
            return "In Stock";
        }
    }
}