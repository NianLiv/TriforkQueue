using System;

namespace Core.Contracts.MessageBroker
{
    public interface IMessage
    {
        DateTime Timestamp { get; set; }
    }
}
