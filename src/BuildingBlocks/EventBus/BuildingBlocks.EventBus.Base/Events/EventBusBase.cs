using BuildingBlocks.EventBus.Base.Constants;
using BuildingBlocks.EventBus.Base.Interfaces;
using BuildingBlocks.EventBus.Base.SubscriptionManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus.Base.Events
{
    public abstract class EventBusBase : IEventBus
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEventBusSubscriptionManager _eventBusSubscriptionManager;
      
        private EventBusConfiguration EventBusConfiguration;

        public EventBusBase(EventBusConfiguration eventBusConfiguration, IServiceProvider serviceProvider)
        {
            eventBusConfiguration = eventBusConfiguration;
            _serviceProvider = serviceProvider;

            _eventBusSubscriptionManager = new InMemoryEventBusSubscriptionManager(this.ProcessEventName);
        }

        public virtual String ProcessEventName(String eventName)
        {
            if (EventBusConfiguration.DeleteEventPrefix is true)
                eventName = eventName.TrimStart(EventBusConfiguration.EventNamePrefix.ToArray());
            if (EventBusConfiguration.DeleteEventSuffix is true)
                eventName = eventName.TrimStart(EventBusConfiguration.EventNameSuffix.ToArray());

            return eventName;
        }

        public void Publish(IntegrationEvent @event)
        {
            throw new NotImplementedException();
        }

        public void Subscribe<TIntegrationEvent, TIntegrationEventHandler>()
            where TIntegrationEvent : IntegrationEvent
            where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            throw new NotImplementedException();
        }

        public void UnSubscribe<TIntegrationEvent, TIntegrationEventHandler>()
            where TIntegrationEvent : IntegrationEvent
            where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            throw new NotImplementedException();
        }
    }
}
