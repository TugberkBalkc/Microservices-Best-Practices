using BuildingBlocks.EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus.Base.Interfaces
{
    public interface IIntegrationEventHandler<TIntegrationEvent>
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handler(TIntegrationEvent @event);
    }
}
