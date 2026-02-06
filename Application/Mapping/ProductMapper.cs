using Application.DTOs;
using Domain.Entities;

namespace Application.Mapping
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(Product product)
        {
            return new ProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Category = product.Category,
                Unit = product.Unit,
                Price = product.Price,
                Stock = product.Stock,
                LowStockThreshold = product.LowStockThreshold,
                Status = product.GetStatus()
            };
        }

        public static Product ToDomain(ProductDto dto)
        {
            return new Product
            {
                ProductName = dto.ProductName,
                Category = dto.Category,
                Unit = dto.Unit,
                Price = dto.Price,
                Stock = dto.Stock,
                LowStockThreshold = dto.LowStockThreshold
            };
        }

        public static void MapToExisting(ProductDto dto, Product product)
        {
            product.ProductName = dto.ProductName;
            product.Category = dto.Category;
            product.Unit = dto.Unit;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            product.LowStockThreshold = dto.LowStockThreshold;
            product.ModifiedDate = DateTime.Now;
        }
    }
}
