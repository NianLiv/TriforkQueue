using Core.Entities.ToDoTasks;
using DataAccess.RabbitMQ;
using RabbitMQ.Client;
using System.Threading;

namespace TaskProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            // SOME FANCY FACTORIES HERE:
                // AND ENVIRONMENT VARIABLES

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var client = new RabbitMqClient(factory, "TaskQueue");

            var producer = new RabbitMqProducer<ToDoTaskMessage>(client);

            var taskCreator = new ToDoTaskMessageCreator(producer);

            taskCreator.Start();

            Thread.Sleep(5000);

            taskCreator.Stop();
        }
    }
}
