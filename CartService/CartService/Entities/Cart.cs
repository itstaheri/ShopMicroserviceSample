namespace CartService.Entities
{
    public class Cart : BaseEntity
    {
        public Cart(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
        public List<CartItem> CartItems { get; set; } = new();
    }
}
