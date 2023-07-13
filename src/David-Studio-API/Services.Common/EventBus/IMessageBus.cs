namespace Services.Common.EventBus
{
    public interface IMessageBus
    {
        public void Publish<TEventSource, TEventAction, TData>
            (TEventSource source, TEventAction action, TData data)
            where TData : class
            where TEventSource : Enum
            where TEventAction : Enum;
    }
}
