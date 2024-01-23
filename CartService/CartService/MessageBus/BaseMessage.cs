namespace CartService.MessageBus
{
    public class BaseMessage
    {
        public Guid MessageId { get; set; } = Guid.NewGuid();
        public DateTime CreateDate { get; set; }
    }
}
