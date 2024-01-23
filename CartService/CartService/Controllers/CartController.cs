using CartService.Dtos;
using CartService.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: api/<CartController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        // GET api/<CartController>/5
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            return Ok(await _cartService.GetCartAsync(userId));
           

        }

        // POST api/<CartController>
        [HttpPost]
        [Route("/api/Cart/AddCartItem")]
        public async Task<IActionResult> AddCartItem([FromBody] AddCartItemDto commend)
        {
            await _cartService.AddCartItem(commend);
            return Ok();

        }
        [HttpPost]
        [Route("/api/Cart/AddCart")]
        public async Task<IActionResult> AddCart([FromQuery] Guid userId)
        {
           var CartId = await _cartService.AddCartAsync(userId);
            return Ok(CartId);

        }

        // PUT api/<CartController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _cartService.RemoveCartItem(id);
            return Ok();
        }
        [HttpPost]
        public IActionResult Checkout(CheckoutDto entity)
        {
            var checkout = _cartService.CheckoutCart(entity);
            if (!checkout.IsSuccess) return StatusCode(500, checkout);
            return Ok(checkout);
        }
    }

}
