using BuildingBlocks.EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus.Base.Interfaces
{
    public interface IEventBusSubscriptionManager
    {
        // Herhangi bir subscription var mı? Herhangi bir event dinleniyor
        bool IsEmpty { get; } 

        // Event silindiği zaman bu event oluşacak ve
        // dışarıdan unsubscribe metodu çalıstıgında bunu tetikliycez
        event EventHandler<String> OnEventRemoved;

        //Subscription Ekleyecek
        void AddSubscription<TIntegrationEvent, TIntegrationEventHandler>()
            where TIntegrationEvent : IntegrationEvent
            where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>;
        //Subscription Silecek
        void RemoveSubscription<TIntegrationEvent, TIntegrationEventHandler>()
           where TIntegrationEvent : IntegrationEvent
           where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>;

        //Generic yollanan eventin subscripe edilip dinlendiğini veya
        //dinlenmediğini döndürecek
        bool HasSubscriptionsForEvent<TIntegrationEvent>()
            where TIntegrationEvent : IntegrationEvent;
        //Event ado ile yollanan eventin subscripe edilip dinlendiğini veya
        //dinlenmediğini döndürecek
        bool HasSubscriptionsForEvent(String eventName);

        //event adına gore tipini dondurur
        // ornek eventName = "OrderCreated" return_value = OrderCreatedIntegrationHandler
        Type GetEventTypeByName(String eventName);

        //Tüm Subscriptions ları temizleyer listeyi 
        void Clear();

        //generic olarak verilen eventin tüm subscription larını geriye doner
        IEnumerable<SubscriptionInfo> GetHandlersByEvent<TIntegrationEvent>()
            where TIntegrationEvent : IntegrationEvent;
        //eventname olarak verilen eventin tüm subscription larını geriye doner
        IEnumerable<SubscriptionInfo> GetHandlersByEvent(String eventName);

        //event routing_key degerini generic olarak verilen degere gore doner
        String GetEventName<T>();
    }
}
