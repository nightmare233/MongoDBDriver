using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace FrankTestMongoDB
{
    class GFSTest
    {
        public void Upload(MongoDBHelp de)
        {
            string inputFileName = @"D:\Documents\kb sql\112.sql";

            //读文件内容
            using (var streamReader = new StreamReader(inputFileName))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    using (var jsonReader = new JsonReader(line))
                    {
                        var context = BsonDeserializationContext.CreateRoot(jsonReader);
                        var document = de.collection.DocumentSerializer.Deserialize(context); //反序列化成二进制
                        de.collection.InsertOne(document);
                    }
                }
            }

        }

        public static void GetFileFromMongo(MongoDBHelp de)
        {
            string outputFileName = @"D:\Documents\kb sql\113.sql"; // initialize to the output file
            
            using (var streamWriter = new StreamWriter(outputFileName))
            {
            
            }
        }


    }
}