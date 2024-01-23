namespace CartService.MessageBus
{
    public class MessageBusConfiguration
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Hostname { get; set; }

    }
    public class RabbitMQConfiguration : MessageBusConfiguration
    {
        public string QueueName_CartCheckout { get; set; }
    }
}