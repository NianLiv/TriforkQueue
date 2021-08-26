using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Entities.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> Get();
        Task<T> Get(Guid id);
        Task<Guid> Create(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
    }
}
