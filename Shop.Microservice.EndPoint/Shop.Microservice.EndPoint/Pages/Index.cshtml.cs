using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Microservice.EndPoint.Dtos.Product;
using Shop.Microservice.EndPoint.Services;
using System.ComponentModel.DataAnnotations;

namespace Shop.Microservice.EndPoint.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IProductApiService productApiService;
        private readonly ICartApiService cartApiService;
       [BindProperty] public List<ProductDto> products { get; set; }
        public IndexModel(IProductApiService productApiService, ICartApiService cartApiService)
        {
            this.productApiService = productApiService;
            this.cartApiService = cartApiService;
        }

        public ActionResult OnGet()
        {
            //   products = productApiService.GetAllProducts().Data.ToList();

            var login = User.Claims;
            return Page();

        }

        [HttpPost]
        public IActionResult OnPost([Required] Guid userId)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(userId.ToString(), "userId is invalid");
                return Page();
            }
            var result = cartApiService.AddCart(userId);
            return RedirectToPage();

        }

    }
}
