using MediPortalEmailService.Models;
using MediPortalEmailService.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MediPortalEmailService.Messaging
{
    public class RabbitMQApplicationsConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private IModel _channel;
        private IConfiguration _configuration;
        private readonly EmailService _saveToDb;
        private readonly EmailSendService _emailService;
        public RabbitMQApplicationsConsumer(IConfiguration configuration, EmailService service)
        {
            _configuration = configuration;
            _saveToDb = service;
            _emailService = new EmailSendService(_configuration);
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(_configuration.GetSection("QueuesandTopics:Applications").Get<string>(), false, false, false, null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, arg) =>
            {
                var content = Encoding.UTF8.GetString(arg.Body.ToArray());
                var userMessage = JsonConvert.DeserializeObject<UserMessage>(content);
                //send email & save to dB
                sendEmail(userMessage).GetAwaiter().GetResult();


                //its Done Now delete from Queue
                _channel.BasicAck(arg.DeliveryTag, false);
            };
            _channel.BasicConsume(_configuration.GetSection("QueuesandTopics:Applications").Get<string>(), false, consumer);

            return Task.CompletedTask;
        }

        private async Task sendEmail(UserMessage userMessage)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<img src=\"https://cdn.pixabay.com/photo/2017/03/14/03/20/woman-2141808_640.jpg\" width=\"1000\" height=\"600\">");
                stringBuilder.Append("<h1> Hello " + userMessage.Name + "</h1>");
                stringBuilder.AppendLine("<br/>Welcome to Mediportal.");

                stringBuilder.Append("<br/>");
                stringBuilder.Append('\n');
                stringBuilder.Append("<p>Your application has been recieved and we will notify you once we review it. </p>");
                var emailLogger = new EmailLoggers()
                {
                    Email = userMessage.Email,
                    Message = stringBuilder.ToString()

                };
                await _saveToDb.SaveData(emailLogger);
                await _emailService.SendEmail(userMessage, stringBuilder.ToString());


            }
            catch (Exception ex) { }
        }
    }
}
