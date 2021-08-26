namespace Core.Entities.ToDoTasks
{
    public static class ToDoTaskMessageMapper
    {
        public static ToDoTask Map(ToDoTaskMessage message)
        {
            return new ToDoTask
            {
                Title = message.Title,
                Objective = message.Objective
            };
        }

    }
}
