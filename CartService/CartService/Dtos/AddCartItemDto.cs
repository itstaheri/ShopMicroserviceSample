namespace CartService.Dtos
{
    public class AddCartItemDto
    {
        public Guid ProductId { get;  set; }
        public double UnitPrice { get;  set; }
        public int Quantity { get;  set; }
        public string ProductName { get; set; }
        public Guid CartId { get;  set; }
    }
}
