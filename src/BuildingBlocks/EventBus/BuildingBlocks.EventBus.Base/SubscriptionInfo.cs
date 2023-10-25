using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus.Base
{
    public class SubscriptionInfo
    {
        public Type HandlerType { get; }


        public SubscriptionInfo(Type handlerType)
        {
            HandlerType = handlerType;
        }


        public static SubscriptionInfo Create(Type handlerType)
        {
            return new(handlerType);
        }
    }
}
