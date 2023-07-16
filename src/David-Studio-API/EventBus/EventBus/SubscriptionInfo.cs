namespace EventBus
{
    public class SubscriptionInfo
    {
        public Type Data { get; set; } = null!;
        public Type Handler { get; set; } = null!;
    }
}
