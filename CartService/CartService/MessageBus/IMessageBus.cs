using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace CartService.MessageBus
{
    public interface IMessageBus
    {
        void SendMessage(BaseMessage message, string QueueName);
    }


    public class RabbitMQMessageBus : IMessageBus
    {
        private string username;
        private string password;
        private string hostname;
        private IConnection connection;

        public RabbitMQMessageBus(IOptions<RabbitMQConfiguration> options)
        {
            username = options.Value.Username;
            password = options.Value.Password;
            hostname = options.Value.Hostname;

        }

        public void SendMessage(BaseMessage message,string QueueName)
        {
          
           if(CheckConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(QueueName, true, false, false, null);
                    var messageSerialyze = JsonConvert.SerializeObject(message);
                    var body = Encoding.UTF8.GetBytes(messageSerialyze);
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    channel.BasicPublish("", QueueName, properties,body);
                };
            }
        }
        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    Password = password,
                    UserName = username,
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
