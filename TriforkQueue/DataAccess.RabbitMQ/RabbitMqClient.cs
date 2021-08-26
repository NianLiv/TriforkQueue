using Core.Contracts.MessageBroker;
using RabbitMQ.Client;
using System;

namespace DataAccess.RabbitMQ
{
    public class RabbitMqClient : IBrokerClient
    {
        public string BrokerName { get; }

        public IModel Channel { get; private set; }
        private IConnection _connection;
        private readonly ConnectionFactory _connectionFactory;

        public RabbitMqClient(ConnectionFactory connectionFactory, string queueName)
        {
            _connectionFactory = connectionFactory;
            BrokerName = queueName;

            ConnectToBroker();
        }

        public void ConnectToBroker()
        {
            if (!IsConnected())
            {
                CreateConnection();
            }

            if (!IsQueueDeclared())
            {
                CreateQueue();
            }
        }

        private void CreateQueue()
        {
            Channel = _connection.CreateModel();
            Channel.QueueDeclare(queue: BrokerName, durable: false, exclusive: false, autoDelete: false);
        }

        private bool IsQueueDeclared()
        {
            if (Channel == null) return false;
            if (Channel.IsOpen == false) return false;
            return true;
        }

        private void CreateConnection()
        {
            _connection = _connectionFactory.CreateConnection();
        }

        private bool IsConnected()
        {
            if (_connection == null) return false;
            if (_connection.IsOpen == false) return false;
            return true;
        }

        public void Dispose()
        {
            Channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
