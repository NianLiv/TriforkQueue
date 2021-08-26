using Core.Contracts.MessageBroker;
using Core.Entities.Services;
using Core.Entities.ToDoTasks;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Business.ToDoTasks
{
    public class ToDoTaskMessageHandler
    {
        private bool cancellationToken;

        private readonly IService<ToDoTask> _toDoTaskService;
        private readonly IConsumer<BasicDeliverEventArgs> _consumer;

        public ToDoTaskMessageHandler(IService<ToDoTask> service, IConsumer<BasicDeliverEventArgs> consumer)
        {
            _toDoTaskService = service;
            _consumer = consumer;
            _consumer.AddEventHandler(OnEventReceived);
        }

        private void OnEventReceived(object sender, BasicDeliverEventArgs evt)
        {
            var message = GetToDoTaskMessageFromEvent(evt);

            Console.WriteLine($"Consumed a ToDoTask message: Time: {message.Timestamp}, Tittle: {message.Title}");

            if (IsOlderThanOneMinute(message))
            {
                Console.WriteLine("Message recieved is more than one minute old - disregarded");
                return;
            }

            var toDoTaskEnity = ToDoTaskMessageMapper.Map(message);
            _toDoTaskService.Create(toDoTaskEnity);

            Console.WriteLine($"ToDoTask with tittle {message.Title}, stored in database.");
        }

        private static bool IsOlderThanOneMinute(ToDoTaskMessage message)
        {
            return message.Timestamp > message.Timestamp.AddMinutes(1);
        }

        private static ToDoTaskMessage GetToDoTaskMessageFromEvent(BasicDeliverEventArgs evt)
        {
            var body = Encoding.UTF8.GetString(evt.Body.ToArray());
            return JsonConvert.DeserializeObject<ToDoTaskMessage>(body);
        }

        public async void Start()
        {
            cancellationToken = false;

            Console.WriteLine("Start consuming ToDoTask messages.");

            while (!cancellationToken)
            {
                _consumer.Consume();
            }
        }

        public void Stop()
        {
            cancellationToken = true;
        }
    }
}
