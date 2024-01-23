namespace OrderService.Dtos
{
    public class AddOrderDto
    {
        public Guid UserId { get; set; }
        public List<OrderDetailDto> Details { get; set; } = new();
    }
}
