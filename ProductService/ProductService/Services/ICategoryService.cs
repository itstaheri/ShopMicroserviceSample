using ProductService.Context;
using ProductService.Entities;
using ProductService.Repositories.BaseRepository;

namespace ProductService.Services
{
    public interface ICategoryService : IRepository<Category>
    {
    }
    public class CategoryService : Repository<Category>, ICategoryService
    {
        public CategoryService(ProductContext context) : base(context)
        {

        }


    }
}
