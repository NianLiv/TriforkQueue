namespace Core.Entities.ToDoTasks
{
    public class ToDoTaskMessage : Message
    {
        public string Title { get; set; }
        public string Objective { get; set; }
    }
}
