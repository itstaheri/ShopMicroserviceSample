using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrderService.Dtos;
using OrderService.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace OrderService.MessagingBus.RecivedMessage
{
    public class RecivedOrderCreateMessage : BackgroundService
    {
        private IModel channel;
        private IConnection connection;
        private readonly IOrderService orderService;

        private readonly string username;
        private readonly string password;
        private readonly string hostname;
        private readonly string queueName_CartCheckout;

        public RecivedOrderCreateMessage (IOptions<RabbitMQConfiguration> options, IOrderService orderService)
        {
         
            this.orderService = orderService;

            username = options.Value.Username;
            password = options.Value.Password;
            hostname = options.Value.Hostname;
            queueName_CartCheckout = options.Value.QueueName_CartCheckout;


            var factory = new ConnectionFactory
            {
                UserName = username,
                Password = password,
                HostName = hostname
            };
            this.connection = factory.CreateConnection();
            this.channel = connection.CreateModel();
            this.channel.QueueDeclare(queueName_CartCheckout, true, false, false, null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, args) =>
              {
                  var body = Encoding.UTF8.GetString(args.Body.ToArray());
                  var deSerializedObject = JsonConvert.DeserializeObject<CheckoutDto>(body);

                  AddOrder(deSerializedObject);
                  channel.BasicAck(args.DeliveryTag, false);
              };

            channel.BasicConsume(queueName_CartCheckout, false, consumer);

            return Task.CompletedTask;

        }
        private void AddOrder(CheckoutDto checkout)
        {
          
            var order = new AddOrderDto
            {
                UserId = checkout.UserId
            };
            orderService.AddOrdeAsync(order);
        }
    }
}
