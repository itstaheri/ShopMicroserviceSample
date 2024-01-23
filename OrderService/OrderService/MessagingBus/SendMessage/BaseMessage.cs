namespace OrderService.MessagingBus.SendMessage
{
    public class BaseMessage
    {
        public Guid MessageId { get; set; } = Guid.NewGuid();
        public DateTime SendDate { get; set; } = DateTime.UtcNow;

    }
}
