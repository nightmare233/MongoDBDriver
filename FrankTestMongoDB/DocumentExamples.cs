using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Generic;

namespace  FrankTestMongoDB
{
    
    public class DocumentExamples
    {
        public readonly IMongoClient client;
        public readonly IMongoCollection<BsonDocument> collection;
        public readonly IMongoDatabase database;
      
        /// <summary>
        /// Structor 
        /// </summary>
        public DocumentExamples()
        {
            var connectionString = GetConnectionString().ToString();
            client = new MongoClient(connectionString);
            database = client.GetDatabase("test");
            collection = database.GetCollection<BsonDocument>("inventory");
            //database.DropCollection("inventory");
        }


        private static ConnectionString GetConnectionString()
        {
            var uri = Environment.GetEnvironmentVariable("MONGODB_URI") ?? Environment.GetEnvironmentVariable("MONGO_URI") ?? "mongodb://localhost";
            return new ConnectionString(uri);
        }

        #region public method
        // private methods
        public IEnumerable<BsonDocument> ParseMultiple(params string[] documents)
        {
            return documents.Select(d => BsonDocument.Parse(d));
        }

        public BsonDocument Render(FilterDefinition<BsonDocument> filter)
        {
            var registry = BsonSerializer.SerializerRegistry;
            var serializer = registry.GetSerializer<BsonDocument>();
            return filter.Render(serializer, registry);
        }

        public BsonDocument Render(ProjectionDefinition<BsonDocument, BsonDocument> projection)
        {
            var registry = BsonSerializer.SerializerRegistry;
            var serializer = registry.GetSerializer<BsonDocument>();
            return projection.Render(serializer, registry).Document;
        }

        public BsonDocument Render(UpdateDefinition<BsonDocument> update)
        {
            var registry = BsonSerializer.SerializerRegistry;
            var serializer = registry.GetSerializer<BsonDocument>();
            return update.Render(serializer, registry);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="documents"></param>
        public void RemoveIds(IEnumerable<BsonDocument> documents)
        {
            foreach (var document in documents)
            {
                document.Remove("_id");
            }
        }
#endregion
    }



}