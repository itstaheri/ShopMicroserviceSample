namespace Shop.Microservice.EndPoint.Dtos.Cart
{
    public class CheckoutDto
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public Guid AddressId { get; set; }
        public string Description { get; set; }
    }
}
