using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MessageBus
{
    public class RabbitMQPublisher : IRabbitMQPublisherInterface
    {
        private IConnection _connection;
        public void PublishMessage(object message, string queue_topic_name)
        {
            if (CheckConnection())
            {
                var channel = _connection.CreateModel();
                channel.QueueDeclare(queue_topic_name, false, false, false, null);
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: "", routingKey: queue_topic_name, null, body: body);
            }
            
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest"
                };

                _connection = factory.CreateConnection();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }
        public bool CheckConnection()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();
            return true;
        }
    }
}
