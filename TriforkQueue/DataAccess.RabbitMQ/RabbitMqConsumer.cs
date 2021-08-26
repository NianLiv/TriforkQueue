using Core.Contracts.MessageBroker;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace DataAccess.RabbitMQ
{
    public class RabbitMqConsumer : IConsumer<BasicDeliverEventArgs>
    {
        private readonly RabbitMqClient _client;
        public IBrokerClient Client => _client;

        private readonly EventingBasicConsumer _consumer;

        public RabbitMqConsumer(RabbitMqClient client)
        {
            _client = client;

            _consumer = new EventingBasicConsumer(_client.Channel);
        }

        public void AddEventHandler(EventHandler<BasicDeliverEventArgs> onEventReceived)
        {
            _consumer.Received += onEventReceived;
        }

        public void Consume()
        {
            _client.Channel.BasicConsume(_client.BrokerName, true, _consumer);
        }
    }
}
