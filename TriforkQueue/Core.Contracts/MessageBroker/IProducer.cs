namespace Core.Contracts.MessageBroker
{
    public interface IProducer<T> where T : IMessage
    {
        IBrokerClient Client { get; }
        void Publish(T @message);
    }
}
