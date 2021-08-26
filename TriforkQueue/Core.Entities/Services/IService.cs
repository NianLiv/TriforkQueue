using System;
using System.Collections.Generic;

namespace Core.Entities.Services
{
    public interface IService<T>
    {
        IEnumerable<T> Get();
        T Get(Guid id);
        Guid Create(T entity);
        void Update(T entity);
        void Delete(Guid id);
    }
}
