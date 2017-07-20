using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.IO;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace FrankTestMongoDB
{
    class Program
    {
        static MongoDBHelp de = new MongoDBHelp();
        static void Main(string[] args)
        {
            try
            {
                AbstractAttachmentAccess aa, bb;
                aa = new AttachmentAccess();

                //string filepath = @"D:\Documents\kb sql\111.txt";

                //byte[] byteImg = File.ReadAllBytes(filepath);
                //string id = aa.InsertAttachment(byteImg);
                string id = "597047d03f47be3ed0548dac";

                var doc = aa.GetAttachmentBytesByObjId(id);

                //bb = new AttachmentGFSAccess();
                //string id = bb.InsertAttachment(byteImg);

                //var doc = bb.GetAttachmentBytesByObjId(id);



                Console.Out.WriteLine(id);
                Console.Out.WriteLine(doc);
                 Console.ReadKey();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Example_1()
        {
            // db.inventory.insertOne( { item: "canvas", qty: 100, tags: ["cotton"], size: { h: 28, w: 35.5, uom: "cm" } } ) 

            // Start Example 1
            //MongoDBHelp de = new MongoDBHelp();
            //var document = new BsonDocument
            //{
            //    { "item", "canvas" },
            //    { "qty", 100 },
            //    { "tags", new BsonArray { "cotton" } },
            //    { "size", new BsonDocument { { "h", 28 }, { "w", 35.5 }, { "uom", "cm" } } }
            //};
            //de.collection.InsertOne(document);
            // End Example 1
            //List<BsonDocument> lists = new List<BsonDocument>();
            for (int i = 0; i <= 100; i++)
            {
                var document = new BsonDocument {
                        { "item", "canvas"+ i },
                        { "qty", 100+i },
                        { "tags", new BsonArray { "cotton" } },
                        { "size", new BsonDocument { { "h", 28 }, { "w", 35.5 }, { "uom", "cm"+i } } } };
                de.collection.InsertOneAsync(document);
            }

            var result = de.collection.Find("{}").ToJson();

            //de.RemoveIds(result); 
        }

        public static void Example_2()
        {
            // db.inventory.find( { item: "canvas" } )

            // Start Example 2
            //MongoDBHelp de = new MongoDBHelp();
            //var filter = Builders<BsonDocument>.Filter.Eq("item", "canvas1");
            var filter = Builders<BsonDocument>.Filter.Gt("qty", 190);
            var result = de.collection.Find(filter).ToList();
            // End Example 2
            Console.Out.Write(result.ToJson());
            Console.ReadKey();
            //de.Render(filter).Should().Be("{ item: \"canvas\" }");
        }

        public static void Example_3()
        {
            var documents = new BsonDocument[]
       {
                new BsonDocument
                {
                    { "item", "journal" },
                    { "qty", 25 },
                    { "tags", new BsonArray { "blank", "red" } },
                    { "size", new BsonDocument { { "h", 14 }, { "w", 21 }, {  "uom", "cm"} } }
                },
                new BsonDocument
                {
                    { "item", "mat" },
                    { "qty", 85 },
                    { "tags", new BsonArray { "gray" } },
                    { "size", new BsonDocument { { "h", 27.9 }, { "w", 35.5 }, {  "uom", "cm"} } }
                },
                new BsonDocument
                {
                    { "item", "mousepad" },
                    { "qty", 25 },
                    { "tags", new BsonArray { "gel", "blue" } },
                    { "size", new BsonDocument { { "h", 19 }, { "w", 22.85 }, {  "uom", "cm"} } }
                },
       };
            de.collection.InsertMany(documents);
            var filter = Builders<BsonDocument>.Filter.Eq("item", "mousepad");
            var result = de.collection.Find(filter).ToList();
            Console.Out.Write(result.ToJson());
            Console.ReadKey();
        }

        public static string TestUploadFiles()
        {
            try
            {
                string filepath = @"D:\Documents\kb sql\dbo.t_KB_Article.sql";

                byte[] byteImg = File.ReadAllBytes(filepath);
                string _id = Guid.NewGuid().ToString();
                //BsonDocument bdoc = new BsonDocument() { { "id", "002" }, { "doc", byteImg } };
                BsonDocument bdoc = new BsonDocument() { { "_id", _id }, { "name", "frank" }, { "age", "12" }, { "doc", byteImg } };
                //de.collection.InsertOneAsync(bdoc);
                de.collection.InsertOne(bdoc);
                return _id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void WritetoFieles()
        {

        }

        public static string GetFileFromMongo(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var result = de.collection.Find(filter).ToList().ToJson();
            return result;
        }
    }
}
