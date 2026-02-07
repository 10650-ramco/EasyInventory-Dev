using Application.DTOs;
using Domain.Entities;

namespace Application.Mapping
{
    public static class ProductMapper
    {
        // Domain → DTO
        public static ProductDto ToDto(this Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            return new ProductDto
            {
                Id = product.ProductId,
                Name = product.ProductName,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.CategoryName ?? "No Category",
                Price = product.Price,
                Stock = product.Stock,
                Unit = product.Unit,
                LowStockThreshold = product.LowStockThreshold,
                Status = product.GetStatus()
            };
        }

        // DTO → Domain (CREATE)
        public static Product ToDomain(this ProductDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var product = Product.Create(
                dto.Name.Trim(),
                dto.CategoryId,
                dto.Price,
                dto.Stock);

            product.Unit = dto.Unit;
            product.LowStockThreshold = dto.LowStockThreshold;
            return product;
        }

        // DTO → Domain (UPDATE)
        public static Product ToDomain(this ProductDto dto, int id)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var product = Product.Update(
                id,
                dto.Name.Trim(),
                dto.CategoryId,
                dto.Price,
                dto.Stock);

            product.Unit = dto.Unit;
            product.LowStockThreshold = dto.LowStockThreshold;
            return product;
        }
    }
}
