namespace Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int? LowStockThreshold { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;

        // Navigation Properties
        public virtual Category Category { get; set; } = null!;

        private Product() { }

        public static Product Create(string productName, int categoryId, decimal price, int stock)
        {
            return new Product
            {
                ProductName = productName,
                CategoryId = categoryId,
                Price = price,
                Stock = stock
            };
        }

        public static Product Update(int productId, string productName, int categoryId, decimal price, int stock)
        {
            return new Product
            {
                ProductId = productId,
                ProductName = productName,
                CategoryId = categoryId,
                Price = price,
                Stock = stock
            };
        }

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