namespace OrderService.MessagingBus.RecivedMessage
{
    public class CheckoutDto
    {
        public string CartId { get; set; }
        public Guid UserId { get; set; }
        public string AddressId { get; set; }
        public string Description { get; set; }
        public object CartItems { get; set; }
        public string MessageId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
