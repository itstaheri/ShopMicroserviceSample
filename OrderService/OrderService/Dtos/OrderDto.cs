namespace OrderService.Dtos
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public string Code { get; set; }
        public Guid UserId { get; set; }
        public bool IsPaid { get; set; }
        public List<OrderDetailDto>  OrderDetails { get; set; }
    }
}
