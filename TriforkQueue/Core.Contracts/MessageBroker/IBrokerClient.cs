using System;

namespace Core.Contracts.MessageBroker
{
    public interface IBrokerClient : IDisposable
    {
        string BrokerName { get; }
        void ConnectToBroker();
    }
}
