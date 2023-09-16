﻿using EventBus.Events;

namespace Search.IntegrationEvents.Events
{
    public record RemoveTagIntegrationEvent : IntegrationEvent
    {
        public int TagId { get; }

        public RemoveTagIntegrationEvent(int id)
        {
            TagId = id;
        }
    }
}
