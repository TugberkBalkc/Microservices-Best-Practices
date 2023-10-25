using BuildingBlocks.EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus.Base.Interfaces
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent @event);

        void Subscribe<TIntegrationEvent, TIntegrationEventHandler>() 
            where TIntegrationEvent : IntegrationEvent 
            where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>;

        void UnSubscribe<TIntegrationEvent, TIntegrationEventHandler>()
           where TIntegrationEvent : IntegrationEvent
           where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>;
    }
}
