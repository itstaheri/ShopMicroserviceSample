namespace Shop.Microservice.EndPoint.Dtos.Cart
{
    public class CartItemDto
    {
        public Guid CartItemId { get;  set; }
        public Guid ProductId { get;  set; }
        public double UnitPrice { get;  set; }
        public int Quantity { get;  set; }
        public Guid CartId { get;  set; }
    }
}
