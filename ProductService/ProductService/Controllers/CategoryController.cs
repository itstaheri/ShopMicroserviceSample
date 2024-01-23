using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Dtos;
using ProductService.Services;

namespace ProductService.Controllers
{
    [Route("api/Category/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: CategoryController
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_categoryService.GetAll());
        }

        // GET: CategoryController/Details/5
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            return Ok(_categoryService.GetById(id));
        }

        // POST: CategoryController/Create
        [HttpPost]
        public IActionResult Create(AddCategoryDto entery)
        {
            _categoryService.Insert(new Entities.Category(entery.CategoryName, entery.CategoryDescription));
            return Ok(entery);
            
            
        }

     
    }
}
