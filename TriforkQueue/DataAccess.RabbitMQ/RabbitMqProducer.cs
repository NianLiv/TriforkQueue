using Core.Contracts.MessageBroker;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace DataAccess.RabbitMQ
{

    public class RabbitMqProducer<T> : IProducer<T> where T : IMessage
    {
        private readonly RabbitMqClient _client;
        public IBrokerClient Client => _client;

        public RabbitMqProducer(RabbitMqClient client)
        {
            _client = client;
        }

        public void Publish(T message)
        {
            _client.ConnectToBroker();

            var messageInBytes = GetMessageAsBytes(message);
            var properties = GetPublisherProperties();

            _client.Channel.BasicPublish("", _client.BrokerName, properties, messageInBytes);
        }
        private IBasicProperties GetPublisherProperties()
        {
            var properties = _client.Channel.CreateBasicProperties();
            properties.Timestamp = new AmqpTimestamp(DateTime.Now.Second);
            properties.Persistent = true;   // Allows multiple consumers.
            return properties;
        }

        private static byte[] GetMessageAsBytes(IMessage message)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
        }
    }
}
