using Microsoft.EntityFrameworkCore;
using ProductService.Context;
using ProductService.Dtos;
using ProductService.Entities;
using ProductService.Repositories.BaseRepository;

namespace ProductService.Services
{
    public class ProductService : Repository<Product>,IProductService
    {
        private readonly ProductContext _context;

        public ProductService(ProductContext context)  : base(context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> GetAllProducts()
        {
            return await _context.products.Where(x => !x.IsDeleted).Select(x => new ProductDto
            {
                CategoryId = x.CategoryId,
                Description = x.Description,
                IsInStock = x.IsInStock,
                PoductId = x.Id,
                Price = x.Price,
                ProductCode = x.ProductCode,
                ProductName = x.ProductName,
                QuantityInStock = x.QuantityInStock,

            }).AsNoTracking().ToListAsync();

        }
    }

}
