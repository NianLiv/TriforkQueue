using Core.Entities.Task;
using DataAccess.RabbitMQ;
using RabbitMQ.Client;

namespace TaskProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var client = new RabbitMqClient(factory, "TaskQueue");

            var producer = new RabbitMqProducer<TaskMessage>(client);

            var taskMsg = new TaskMessage
            {
                Title = "Task one",
                Objective = "Do this and do that."
            };

            producer.Publish(taskMsg);
        }
    }
}
