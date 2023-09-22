using EventBus.Events;

namespace EventBus.Abstractions
{
    public interface IEventBus
    {
        public void Publish(IntegrationEvent @event);

        public void Subscribe<TEvent, TEventHandler>()
            where TEvent : IntegrationEvent
            where TEventHandler : IIntegrationEventHandler<TEvent>;
    }
}
