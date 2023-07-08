using System.Text;
using System.Threading.Channels;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Storage.Services;

namespace Storage.MessageBus.Services
{
    public class StorageImagesSubscriber : BackgroundService
    {
        private const string topic = "images";

        private readonly IRepositoryManager _repositoryManager;
        private readonly IConfiguration _configuration;
        private readonly IModel _channel;

        private string queueName;

        public StorageImagesSubscriber(
            IServiceScopeFactory scopeFactory,
            IConfiguration configuration,
            IMessageBusClient messageBusClient)
        {
            IServiceScope scope = scopeFactory.CreateScope();

            _repositoryManager = scope.ServiceProvider
                .GetRequiredService<IRepositoryManager>();
            _configuration = configuration;
            _channel = messageBusClient.GetChannel();

            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {
            queueName = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(queue: queueName,
                               exchange: "storage",
                               routingKey: $"{topic}.*");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var routingKey = ea.RoutingKey;

                switch (routingKey)
                {
                    case $"{topic}.delete":
                        await _repositoryManager.Images.DeleteAsync(message);
                        await _repositoryManager.SaveAsync();
                        break;
                }
            };

            _channel.BasicConsume(queue: queueName,
                                  autoAck: true,
                                  consumer: consumer);

            return Task.CompletedTask;
        }
    }
}

