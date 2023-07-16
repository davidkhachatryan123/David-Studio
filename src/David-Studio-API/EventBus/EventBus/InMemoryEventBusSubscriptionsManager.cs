using EventBus.Abstractions;
using EventBus.Events;

namespace EventBus
{
    public class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        private Dictionary<string, SubscriptionInfo> _handlers;

        public InMemoryEventBusSubscriptionsManager()
        {
            _handlers = new Dictionary<string, SubscriptionInfo>();
        }

        public void AddSubscription<TEvent, TEventHandler>()
            where TEvent : IntegrationEvent
            where TEventHandler : IIntegrationEventHandler<TEvent>
        {
            string eventName = GetEventName<TEvent>();

            if (!HasSubscriptionForEvent(eventName))
                _handlers.Add(eventName, new SubscriptionInfo());

            _handlers[eventName].Event = typeof(TEvent);
            _handlers[eventName].Handler = typeof(TEventHandler);
        }

        public bool HasSubscriptionForEvent<TEvent>()
            where TEvent : IntegrationEvent
            => HasSubscriptionForEvent(GetEventName<TEvent>());

        public bool HasSubscriptionForEvent(string eventName)
            => _handlers.ContainsKey(eventName);

        public string GetEventName<TEvent>()
            where TEvent : IntegrationEvent
            => typeof(TEvent).Name;

        public SubscriptionInfo GetSubscribtion(string eventName) => _handlers[eventName];
    }
}
