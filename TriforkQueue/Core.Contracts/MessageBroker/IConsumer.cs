using System;

namespace Core.Contracts.MessageBroker
{
    public interface IConsumer<T>
    {
        IBrokerClient Client { get; }
        void AddEventHandler(EventHandler<T> onEventReceived);
        void Consume();
    }
}
