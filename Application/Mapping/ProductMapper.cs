using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers
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
                //CategoryName = product.Category?.Name,
                Price = product.Price,
                Stock = product.Stock
            };
        }

        // DTO → Domain (CREATE)
        public static Product ToDomain(this ProductDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return Product.Create(
                dto.Name.Trim(),
                dto.CategoryId,
                dto.Price,
                dto.Stock);
        }

        // DTO → Domain (UPDATE)
        public static Product ToDomain(this ProductDto dto, int id)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return Product.Update(
                id,
                dto.Name.Trim(),
                dto.CategoryId,
                dto.Price,
                dto.Stock);
        }
    }
}
