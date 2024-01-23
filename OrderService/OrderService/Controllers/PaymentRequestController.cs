using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Services;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentRequestController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public PaymentRequestController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult Post(Guid OrderId)
        {
            var result = _orderService.RequestPayment(OrderId);
            return Ok(result);

        }
    }
}
