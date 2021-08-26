using Core.Contracts.MessageBroker;
using Core.Entities.ToDoTasks;
using System;
using System.Threading.Tasks;

namespace TaskProducer
{
    public class ToDoTaskMessageCreator : IMessageCreator
    {
        private bool cancellationToken;

        private readonly IProducer<ToDoTaskMessage> _producer;

        public ToDoTaskMessageCreator(IProducer<ToDoTaskMessage> producer)
        {
            _producer = producer;
        }

        public async void Start()
        {
            cancellationToken = false;

            Console.WriteLine("Start creating ToDoTask messages.");

            while (!cancellationToken)
            {
                var toDoTaskMessage = GenerateToDoTaskMessage();

                _producer.Publish(toDoTaskMessage);

                await Task.Delay(1500);
            }
        }

        public void Stop()
        {
            cancellationToken = true;
            Console.WriteLine("Stop creating ToDoTask messages.");
        }

        private static ToDoTaskMessage GenerateToDoTaskMessage()
        {
            return new ToDoTaskMessage
            {
                Title = "Task 999",
                Objective = "Do this and do that"
            };
        }
    }
}
