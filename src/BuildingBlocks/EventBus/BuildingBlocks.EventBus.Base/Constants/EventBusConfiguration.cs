using BuildingBlocks.EventBus.Base.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus.Base.Constants
{
    public class EventBusConfiguration
    {
        public int ConncetionRetryCount { get; set; } = 5;
       
        public String DefaultTopicName { get; set; } = "ExtendTradeEventBus";
   
        public String SubscriberClientAppName { get; set; } = String.Empty;
        
        public String EventNamePrefix { get; set; } = String.Empty;
        public String EventNameSuffix { get; set; } = "IntegrationEvent";
        
        public EventBusType EventBusType { get; set; } = EventBusType.RabbitMQ;

        public String EventBusConnectionString { get; set; } = String.Empty;
        public object Connection { get; set; }


        public bool DeleteEventPrefix => !String.IsNullOrEmpty(EventNamePrefix);
        public bool DeleteEventSuffix => !String.IsNullOrEmpty(EventNameSuffix);
    }
}
