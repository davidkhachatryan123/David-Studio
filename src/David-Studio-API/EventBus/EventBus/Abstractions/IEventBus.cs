namespace EventBus.Abstractions
{
    public interface IEventBus
    {
        public void Publish<TEventSource, TEventAction, TData>
            (TEventSource source, TEventAction action, TData data)
            where TData : class
            where TEventSource : Enum
            where TEventAction : Enum;

        public void Subscribe<TData, TEventHandler, TEventSource, TEventAction>
            (TEventSource source, TEventAction action)
            where TData : class
            where TEventHandler : IIntegrationEventHandler<TData>
            where TEventSource : Enum
            where TEventAction : Enum;
    }
}
