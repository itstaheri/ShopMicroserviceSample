using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace OrderService.MessagingBus.SendMessage
{
    public interface IMessageBus
    {
        void SendMessage(BaseMessage baseMessage, string QueueName);
    }
    public class RabbitMQMessageBus : IMessageBus
    {
        private readonly string username;
        private readonly string password;
        private readonly string hostname;
        public IConnection connection;

        public RabbitMQMessageBus(IOptions<RabbitMQConfiguration> options)
        {
            username = options.Value.Username;
            password = options.Value.Password;
            hostname = options.Value.Hostname;

            
        }
        public void SendMessage(BaseMessage baseMessage, string QueueName)
        {
            if (CheckConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(QueueName, true, false, false, null);
                    var serializedMessage = JsonConvert.SerializeObject(baseMessage);
                    var body = Encoding.UTF8.GetBytes(serializedMessage);
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    channel.BasicPublish("",QueueName, properties, body);

                }
            }
        }
        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    UserName = username,
                    Password = password,
                    HostName = hostname,
                };
                connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Connection Feild! message : {ex.Message} , InnerException : {ex.InnerException}");
            }
        }
        private bool CheckConnection()
        {
            if (connection != null) return true;
            CreateConnection();
            return connection != null;

        }
    }
}
