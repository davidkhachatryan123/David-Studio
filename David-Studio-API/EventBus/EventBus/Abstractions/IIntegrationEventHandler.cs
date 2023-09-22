namespace EventBus.Abstractions
{
    public interface IIntegrationEventHandler<TData>
    {
        public Task Handle(TData data);
    }
}
