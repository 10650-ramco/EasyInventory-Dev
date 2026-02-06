using Application.Common;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<Product>>GetPagedAsync(int page, int pageSize)
    {
        var query = _context.Products
                            .Include(p => p.ProductName)
                            .AsNoTracking();

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(p => p.ProductName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Product>(items, totalCount, page, pageSize);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
        => await _context.Products
            .Include(p => p.ProductName)
            .ToListAsync();

    public async Task AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _context.Categories
                             .AsNoTracking()
                             .OrderBy(c => c.CategoryName)
                             .ToListAsync();
    }

}
