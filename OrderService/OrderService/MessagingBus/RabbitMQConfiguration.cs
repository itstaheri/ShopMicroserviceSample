namespace OrderService.MessagingBus
{
    public class RabbitMQConfiguration
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Hostname { get; set; }
        public string QueueName_CartCheckout { get; set; }
        public string QueueName_OrderSendToPay { get; set; }
    }
}
