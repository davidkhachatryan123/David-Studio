using System.Text;
using System.Threading.Channels;
using Google.Protobuf;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Storage.Services;

namespace Storage.MessageBus.Subscribers
{
    public class ImagesSubscriber : BackgroundService
    {
        private const string topic = "images";

        private readonly IMessageBusClient _messageBusClient;
        private readonly IRepositoryManager _repositoryManager;

        public ImagesSubscriber(
            IMessageBusClient messageBusClient,
            IServiceScopeFactory scopeFactory)
        {
            _messageBusClient = messageBusClient;

            IServiceScope scope = scopeFactory.CreateScope();
            _repositoryManager = scope.ServiceProvider
                .GetRequiredService<IRepositoryManager>();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            _messageBusClient.CreateSubsciber(topic, ProccedEventAsync);

            return Task.CompletedTask;
        }

        private async Task ProccedEventAsync(string routingKey, string? message)
        {
            if (message is null) throw new ArgumentNullException(message);

            switch (routingKey)
            {
                case $"{topic}.delete":
                    await _repositoryManager.Images.DeleteAsync(
                        message.Substring(message.LastIndexOf('/') + 1));
                    await _repositoryManager.SaveAsync();
                    break;
            }
        }
    }
}

