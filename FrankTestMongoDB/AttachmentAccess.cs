using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Bson.IO;

namespace FrankTestMongoDB
{
    /// <summary>
    /// 按照普通文件存储的方式，非GridFS的方式。
    /// </summary>
    public class AttachmentAccess : AbstractAttachmentAccess
    {
        //private MongoDBHelp helper;
        public AttachmentAccess()
        {
            helper = new MongoDBHelp();
        }

        /// <summary>
        /// 保存附件
        /// </summary>
        /// <param name="attachment">附件文件的字节码数组</param>
        /// <returns>返回ObjecctID</returns>
        public override string InsertAttachment(byte[] attachment)
        {
            //这个判断应该在这个方法调用之前就做好的。
            int fileMaxLendth = 15 * 1024 * 1024;  //15M
            if (attachment.Length > fileMaxLendth)
            {
                return null;
            }
            var _id = ObjectId.GenerateNewId();
            BsonDocument bdoc = new BsonDocument() { { "_id", _id }, { "docs", attachment } };

            helper.collection.InsertOne(bdoc);
            return _id.ToString();
        }

        /// <summary>
        /// 保存附件
        /// </summary>
        /// <param name="attachment">附件文件的字节码数组</param>
        /// <returns>返回ObjecctID</returns>
        public string SaveAttachment(AttachmentModel model)
        {
            //这个判断应该在这个方法调用之前就做好的。
            int fileMaxLendth = 15 * 1024 * 1024;  //15M

            if (model.docs.Length > fileMaxLendth)
            {
                return null;
            }
            var _id = ObjectId.GenerateNewId();

            BsonDocument bdoc = new BsonDocument() { { "_id", _id }, { "ticketid", model.ticketid }, { "module", model.module }, { "docs", model.docs } };

            helper.collection.InsertOne(bdoc);

            return _id.ToString();
        }

        /// <summary>
        /// 根据ObjectId获取附件字节码数组
        /// </summary>
        /// <param name="objId">ObjecID</param>
        /// <returns>附件的字节码数组</returns>
        public override string GetAttachmentByObjId(string objId)
        {
            ObjectId _id;
            if (ObjectId.TryParse(objId, out _id))
            {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", _id);
                var result = helper.collection.Find(filter).ToBson();
                //AttachmentModel amodel = BsonSerializer.Deserialize<AttachmentModel>(result);
                return null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据ObjectId获取附件字节码数组
        /// </summary>
        /// <param name="objId">ObjecID</param>
        /// <returns>附件的字节码数组</returns>
        public override byte[] GetAttachmentBytesByObjId(string objId)
        {
            ObjectId _id;

            if (ObjectId.TryParse(objId, out _id))   //ID要转换为OBjectId类型！
            {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", _id);

                var result = helper.collection.Find(filter).FirstOrDefault();

                return (byte[]) result["docs"] ;  //强制转换成byte[]
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据ObjectID删除附件
        /// </summary>
        /// <param name="objId">ObjectID</param>
        /// <returns>是否成功？</returns>
        public override bool DeleteAttachmentByObjId(string objId)
        {
            ObjectId _id;
            if (ObjectId.TryParse(objId, out _id))   //ID要转换为OBjectId类型！
            {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", _id);
                DeleteResult result = helper.collection.DeleteOne(filter);
                return result.DeletedCount > 0 ? true : false;
            }
            return false;

                
        }


    }
}
