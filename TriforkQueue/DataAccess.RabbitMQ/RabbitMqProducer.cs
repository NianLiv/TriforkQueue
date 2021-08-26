using Core.Contracts.MessageBroker;
using Core.Entities;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace DataAccess.RabbitMQ
{

    public class RabbitMqProducer<T> : IProducer<T> where T : Message
    {
        private readonly RabbitMqClient _client;
        public IBrokerClient Client => _client;

        public RabbitMqProducer(RabbitMqClient client)
        {
            _client = client;
        }

        public void Publish(T message)
        {
            var messageInBytes = GetMessageAsBytes(message);
            var properties = GetPublisherProperties();

            SetTimestampToNow(message);

            _client.Channel.BasicPublish("", _client.BrokerName, properties, messageInBytes);
            Console.WriteLine($"Publishing a message at: {message.Timestamp}");
        }

        private void SetTimestampToNow(T message)
        {
            message.Timestamp = DateTime.Now;
        }

        private IBasicProperties GetPublisherProperties()
        {
            var properties = _client.Channel.CreateBasicProperties();
            properties.Persistent = true;   // Allows multiple consumers.
            return properties;
        }

        private static byte[] GetMessageAsBytes(Message message)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
        }
    }
}
