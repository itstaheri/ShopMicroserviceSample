namespace CartService.Dtos
{
    public class CartItemDto
    {
        public Guid CartItemId { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }

        public string ImageUrl { get; set; }
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        
    }
}
