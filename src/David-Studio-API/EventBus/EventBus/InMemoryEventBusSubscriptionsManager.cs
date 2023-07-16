using System;
using System.Linq;
using EventBus.Abstractions;

namespace EventBus
{
    public class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        private Dictionary<string, SubscriptionInfo> _handlers;

        public InMemoryEventBusSubscriptionsManager()
        {
            _handlers = new Dictionary<string, SubscriptionInfo>();
        }

        public void AddSubscription<TData, TEventHandler>(string eventKey)
            where TEventHandler : IIntegrationEventHandler<TData>
        {
            if (!HasSubscriptionForEvent(eventKey))
                _handlers.Add(eventKey, new SubscriptionInfo());

            _handlers[eventKey].Data = typeof(TData);
            _handlers[eventKey].Handler = typeof(TEventHandler);
        }

        public bool HasSubscriptionForEvent<TEventSource, TEventAction>
            (TEventSource source, TEventAction action)
            where TEventSource : Enum
            where TEventAction : Enum
            => HasSubscriptionForEvent(GetEventKey(source, action));

        public bool HasSubscriptionForEvent(string eventKey)
            => _handlers.ContainsKey(eventKey);

        public string GetEventKey<TEventSource, TEventAction>
            (TEventSource source, TEventAction action)
            where TEventSource : Enum
            where TEventAction : Enum
        {
            string eventSource = Enum.GetName(typeof(TEventSource), source)!.ToLower();
            string eventAction = Enum.GetName(typeof(TEventAction), action)!.ToLower();

            if (eventAction == "any") eventAction = "*";

            return $"{eventSource}.{eventAction}";
        }

        public Type GetHandlerForEvent(string eventKey) => _handlers[eventKey].Handler;

        public Type GetEventTypeByName(string eventKey) => _handlers[eventKey].Data;
    }
}
