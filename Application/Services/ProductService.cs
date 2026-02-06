using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;
using Application.Mapping;
using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return products.Select(ProductMapper.ToDto).ToList();
        }

        public async Task SaveAsync(ProductDto dto)
        {
            Product product;

            if (dto.ProductId == 0)
            {
                product = ProductMapper.ToDomain(dto);
                await _unitOfWork.Products.AddAsync(product);
            }
            else
            {
                product = await _unitOfWork.Products.GetByIdAsync(dto.ProductId)
                          ?? throw new KeyNotFoundException("Product not found");

                ProductMapper.MapToExisting(dto, product);
                await _unitOfWork.Products.UpdateAsync(product);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int productId)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId)
                          ?? throw new KeyNotFoundException("Product not found");

            await _unitOfWork.Products.DeleteAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
