using Core.Entities.Repository;
using Core.Entities.Services;
using Core.Entities.ToDoTasks;
using System;
using System.Collections.Generic;

namespace Business.ToDoTasks
{
    public class ToDoTaskService : IToDoTaskService
    {
        private readonly IRepository<ToDoTask> _toDoTaskRepository;

        public ToDoTaskService(IRepository<ToDoTask> repository)
        {
            _toDoTaskRepository = repository;
        }

        public IEnumerable<ToDoTask> Get()
        {
            return _toDoTaskRepository.Get().Result;
        }

        public ToDoTask Get(Guid id)
        {
            return _toDoTaskRepository.Get(id).Result;
        }

        public Guid Create(ToDoTask toDoTask)
        {
            return _toDoTaskRepository.Create(toDoTask).Result;
        }

        public void Update(ToDoTask toDoTask)
        {
            _toDoTaskRepository.Update(toDoTask).RunSynchronously();
        }

        public void Delete(Guid id)
        {
            _toDoTaskRepository.Delete(id);
        }
    }
}
