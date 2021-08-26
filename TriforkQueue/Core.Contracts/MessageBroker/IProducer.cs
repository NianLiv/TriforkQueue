using Core.Entities;

namespace Core.Contracts.MessageBroker
{
    public interface IProducer<T> where T : Message
    {
        IBrokerClient Client { get; }
        void Publish(T @message);
    }
}
