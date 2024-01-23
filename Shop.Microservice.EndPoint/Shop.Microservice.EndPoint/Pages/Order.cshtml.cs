using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Microservice.EndPoint.Dtos.Order;
using Shop.Microservice.EndPoint.Services;

namespace Shop.Microservice.EndPoint.Pages
{
    [Authorize]
    public class OrderModel : PageModel
    {
        private readonly IOrderApiService _orderApiService;
        public IEnumerable<OrderDto> Orders { get; set; }
        public OrderModel(IOrderApiService orderApiService)
        {
            _orderApiService = orderApiService;
        }

        public void OnGet()
        {
            Orders = _orderApiService.GetAllOrders().Data;
        }
    }
}
