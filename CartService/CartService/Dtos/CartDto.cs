namespace CartService.Dtos
{
    public class CartDto
    {
        public Guid UserId { get; set; }
        public List<CartItemDto> CartItems { get; set; } = new();

        /// <summary>
        /// Get totalPrice
        /// </summary>
        /// <returns></returns>
        public double TotalPrice()
        {
            return CartItems.Count > 0? CartItems
                .Select(x=>x.UnitPrice * x.Count).Sum() : 0;

        }
    }
}
