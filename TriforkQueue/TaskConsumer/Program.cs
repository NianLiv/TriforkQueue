using Business.ToDoTasks;
using DataAccess.MongoDB;
using DataAccess.MongoDB.Repository;
using DataAccess.RabbitMQ;
using RabbitMQ.Client;

namespace TaskConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            // SOME FANCY FACTORIES HERE:
            // AND ENVIRONMENT VARIABLES

            var dbContect = new MongoDbContext("mongodb://root:rootpassword@localhost:27017/?authSource=admin");

            var repository = new ToDoTaskRepository(dbContect);

            var service = new ToDoTaskService(repository);

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var client = new RabbitMqClient(factory, "TaskQueue");

            var consumer = new RabbitMqConsumer(client);

            var messageHandler = new ToDoTaskMessageHandler(service, consumer);

            messageHandler.Start();
        }
    }
}
