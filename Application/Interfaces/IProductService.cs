using Application.DTOs;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetProductsAsync();
        Task SaveAsync(ProductDto product);
        Task DeleteAsync(int productId);
    }
}
