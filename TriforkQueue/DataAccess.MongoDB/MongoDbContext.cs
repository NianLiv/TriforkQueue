using MongoDB.Driver;
using System;

namespace DataAccess.MongoDB
{
    public class MongoDbContext
    {
        private MongoClient _mongoClient;
        public IMongoDatabase Database { get; }

        private const string DB_NAME = "TriforkDatabase";

        public MongoDbContext(string connectionString)
        {
            _mongoClient = new MongoClient(connectionString);
            Database = _mongoClient.GetDatabase(DB_NAME);
        }
    }
}
