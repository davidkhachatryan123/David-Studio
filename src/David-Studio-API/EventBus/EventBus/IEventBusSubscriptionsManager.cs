using EventBus.Abstractions;
using EventBus.Events;

namespace EventBus
{
    public interface IEventBusSubscriptionsManager
    {
        void AddSubscription<TEvent, TEventHandler>()
            where TEvent : IntegrationEvent
            where TEventHandler : IIntegrationEventHandler<TEvent>;

        bool HasSubscriptionForEvent<TEvent>()
            where TEvent : IntegrationEvent;

        bool HasSubscriptionForEvent(string eventName);

        public string GetEventName<TEvent>()
            where TEvent : IntegrationEvent;

        public SubscriptionInfo GetSubscribtion(string eventName);
    }
}
