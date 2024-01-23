namespace Shop.Microservice.EndPoint.Dtos.Order
{
    public class OrderDto
    {
        public Guid orderId { get; set; }
        public string ?code { get; set; }
        public Guid userId { get; set; }
        public bool isPaid { get; set; }
        public object ?orderDetails { get; set; }
    }
}
