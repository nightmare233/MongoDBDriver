using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace MongoDBWebService
{
    [BsonIgnoreExtraElements]
    public class AttachmentModel
    {
        public ObjectId _id { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.Document)]
        public byte[] docs { get; set; }
         
        public int ticketid { get; set; } 

        public string module { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.Document)]
        public DateTime createtime { get; set; }
    }
}
