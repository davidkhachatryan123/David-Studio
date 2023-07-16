using EventBus.Abstractions;

namespace EventBus
{
    public interface IEventBusSubscriptionsManager
    {
        void AddSubscription<TData, TEventHandler>(string eventKey)
            where TEventHandler : IIntegrationEventHandler<TData>;

        bool HasSubscriptionForEvent<TEventSource, TEventAction>
            (TEventSource source, TEventAction action)
            where TEventSource : Enum
            where TEventAction : Enum;

        bool HasSubscriptionForEvent(string eventKey);

        public string GetEventKey<TEventSource, TEventAction>
            (TEventSource source, TEventAction action)
            where TEventSource : Enum
            where TEventAction : Enum;

        public Type GetHandlerForEvent(string eventKey);

        public Type GetEventTypeByName(string eventKey);
    }
}
