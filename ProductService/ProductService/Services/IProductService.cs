using ProductService.Dtos;
using ProductService.Entities;
using ProductService.Repositories.BaseRepository;

namespace ProductService.Services
{
    public interface IProductService : IRepository<Product>
    {
        Task<List<ProductDto>> GetAllProducts();
    }

}
