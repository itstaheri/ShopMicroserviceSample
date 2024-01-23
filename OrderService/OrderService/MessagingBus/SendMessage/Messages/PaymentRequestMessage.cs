namespace OrderService.MessagingBus.SendMessage.Messages
{
    public class PaymentRequestMessage : BaseMessage
    {
        public Guid OrderId { get; set; }
        public double PayAmount { get; set; }
    }
}
