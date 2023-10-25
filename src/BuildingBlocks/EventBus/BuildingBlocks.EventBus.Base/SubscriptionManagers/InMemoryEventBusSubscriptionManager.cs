using BuildingBlocks.EventBus.Base.Events;
using BuildingBlocks.EventBus.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus.Base.SubscriptionManagers
{
    public class InMemoryEventBusSubscriptionManager : IEventBusSubscriptionManager
    {
        private readonly Dictionary<String, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;

        public Func<String, String> EventNameGetter;
        public event EventHandler<String> OnEventRemoved;

        public InMemoryEventBusSubscriptionManager(Func<String, String> eventNameGetter)
        {
            _handlers = new Dictionary<string, List<SubscriptionInfo>>();
            _eventTypes = new List<Type>();

            EventNameGetter = eventNameGetter;
        }

        public bool IsEmpty => _handlers.Keys.Any();

        public void Clear() => _handlers.Clear();

        public void AddSubscription<TIntegrationEvent, TIntegrationEventHandler>()
            where TIntegrationEvent : IntegrationEvent
            where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            String eventName = this.GetEventName<TIntegrationEvent>();

            this.AddSubscription(typeof(TIntegrationEventHandler), eventName);

            if (_eventTypes.Contains(typeof(TIntegrationEvent)) is false)
                _eventTypes.Add(typeof(TIntegrationEvent));
        }

        private void AddSubscription(Type handlerType, String eventName)
        {
            if (this.HasSubscriptionsForEvent(eventName) is false)
                _handlers.Add(eventName, new List<SubscriptionInfo>());

            if (_handlers[eventName].Any(s => s.HandlerType == handlerType) is true)
                throw new ArgumentException($"Handler Type '{handlerType.Name}' Already Registered For '{eventName}'", nameof(handlerType));

            _handlers[eventName].Add(SubscriptionInfo.Create(handlerType));
        }

        public void RemoveSubscription<TIntegrationEvent, TIntegrationEventHandler>()
         where TIntegrationEvent : IntegrationEvent
         where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            SubscriptionInfo subscriptionInfoToRemove = this.GetSubscriptionInfo<TIntegrationEvent, TIntegrationEventHandler>();

            String eventName = this.GetEventName<TIntegrationEvent>();

            this.RemoveHandler(eventName, subscriptionInfoToRemove);
        }
        private void RemoveHandler(String eventName, SubscriptionInfo subscriptionInfoToRemove)
        {
            if (subscriptionInfoToRemove is not null)
            {
                _handlers[eventName].Remove(subscriptionInfoToRemove);

                if (_handlers[eventName].Any() is false)
                {
                    _handlers.Remove(eventName);

                    Type eventType = this.GetEventTypeByName(eventName);

                    if (eventType is not null)
                        _eventTypes.Remove(eventType);

                    this.RaiseOnEventRemoved(eventName);
                }
            }
        }

        private void RaiseOnEventRemoved(String eventName)
        {
            EventHandler<String> handler = this.OnEventRemoved;

            handler?.Invoke(this, eventName);
        }

        private SubscriptionInfo GetSubscriptionInfo<TIntegrationEvent, TIntegrationEventHandler>()
            where TIntegrationEvent : IntegrationEvent
            where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            String eventName = this.GetEventName<TIntegrationEvent>();

            return this.GetSubscriptionInfo(eventName, typeof(TIntegrationEventHandler));
        }

        private SubscriptionInfo GetSubscriptionInfo(String eventName, Type handlerType)
        {
            if (this.HasSubscriptionsForEvent(eventName) is false)
                return null;

            return _handlers[eventName].SingleOrDefault(s => s.HandlerType == handlerType);
        }

        public IEnumerable<SubscriptionInfo> GetHandlersByEvent<TIntegrationEvent>() where TIntegrationEvent : IntegrationEvent
        {
            var eventName = this.GetEventName<TIntegrationEvent>();

            return this.GetHandlersByEvent(eventName);
        }

        public IEnumerable<SubscriptionInfo> GetHandlersByEvent(string eventName)
        {
            return _handlers[eventName];
        }

        public bool HasSubscriptionsForEvent<TIntegrationEvent>() where TIntegrationEvent : IntegrationEvent
        {
            String eventName = this.GetEventName<TIntegrationEvent>();

            return this.HasSubscriptionsForEvent(eventName);
        }

        public bool HasSubscriptionsForEvent(string eventName)
        {
            return _handlers.ContainsKey(eventName);
        }

        public Type GetEventTypeByName(string eventName)
        {
            return _eventTypes.SingleOrDefault(e => e.Name == eventName);
        }

        public string GetEventName<T>()
        {
            String eventName = typeof(T).Name;

            return EventNameGetter(eventName);
        }
    }
}
