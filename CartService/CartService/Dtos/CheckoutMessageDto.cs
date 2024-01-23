using CartService.MessageBus;

namespace CartService.Dtos
{
    public class CheckoutMessageDto : BaseMessage
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public Guid AddressId { get; set; }
        public string Description { get; set; }
        public ICollection<CartItemDto> CartItems { get; set; }
    }
}
