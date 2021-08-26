using Core.Entities.Repository;
using Core.Entities.ToDoTasks;

namespace DataAccess.MongoDB.Repository
{
    public class ToDoTaskRepository : MongoDbRepository<ToDoTask>, IToDoTaskRepository
    {
        public ToDoTaskRepository(MongoDbContext mongoDbContext) : base(mongoDbContext)
        {}
    }
}
