using Core.Entities;
using Core.Entities.Repository;
using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DataAccess.MongoDB.Repository
{
    public abstract class MongoDbRepository<T> : IRepository<T> where T : Entity
    {
        private readonly IMongoCollection<T> _collection;

        public MongoDbRepository(MongoDbContext context)
        {
            _collection = context.Database.GetCollection<T>(typeof(T).Name);
        }

        public async Task<Guid> Create(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity.Id;
        }

        public async Task Delete(Guid id)
        {
            await _collection.DeleteOneAsync(x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<T> Get(Guid id)
        {
            return await _collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task Update(T entity)
        {
            await Delete(entity.Id);
            await Create(entity);
        }
    }
}
