namespace EventBus
{
    public class SubscriptionInfo
    {
        public Type Event { get; set; } = null!;
        public Type Handler { get; set; } = null!;
    }
}
