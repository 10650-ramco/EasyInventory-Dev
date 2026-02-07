using Application.DTOs;
using Domain.Entities;

namespace Application.Mapping
{
    public static class CategoryMapper
    {
        public static CategoryDto ToDto(this Category category)
        {
            if (category == null) return null;

            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
            };
        }

        public static Category ToDomain(this CategoryDto dto)
        {
            if (dto == null) return null;

            return new Category
            {
                CategoryId = dto.CategoryId,
                CategoryName = dto.CategoryName,
                Description = dto.Description
            };
        }
    }
}
