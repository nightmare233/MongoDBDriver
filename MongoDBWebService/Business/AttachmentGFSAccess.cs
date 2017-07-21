using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace MongoDBWebService
{
    /// <summary>
    /// 用gridfs的方式实现文件的存取。
    /// </summary>
    public class AttachmentGFSAccess : AbstractAttachmentAccess
    {
        public AttachmentGFSAccess()
        {
            helper = new MongoDBHelp();
        }

        public override bool DeleteAttachmentByObjId(string objId)
        {
            ObjectId _id;
            try
            {
                if (ObjectId.TryParse(objId, out _id))
                {
                    var bucket = new MongoDB.Driver.GridFS.GridFSBucket(helper.database);
                    bucket.Delete(_id);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override string GetAttachmentByObjId(string objId)
        {
            var bucket = new MongoDB.Driver.GridFS.GridFSBucket(helper.database);

            ObjectId objId1 = new ObjectId(objId);

            byte[] resut = bucket.DownloadAsBytes(objId1);  //这里的参数要ObjectId类型不能是string。

            return resut.ToString();
        }
        /// <summary>
        /// Download Files Bytes Array By ObjectID
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        public override byte[] GetAttachmentBytesByObjId(string objId)
        {
            var bucket = new MongoDB.Driver.GridFS.GridFSBucket(helper.database);

            ObjectId objId1 = new ObjectId(objId);

            byte[] resut = bucket.DownloadAsBytes(objId1);  //这里的参数要ObjectId类型不能是string。

            return resut;
        }

        /// <summary>
        /// Download by filename.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public byte[] GetAttachmentBytesByName(string filename)
        {
            var bucket = new MongoDB.Driver.GridFS.GridFSBucket(helper.database);

            byte[] result = bucket.DownloadAsBytesByName(filename);

            return result;
        }

        public override string InsertAttachment(byte[] attachment)
        {
            string filename = "ticket1";
            var bucket = new MongoDB.Driver.GridFS.GridFSBucket(helper.database);
            var objectID = bucket.UploadFromBytes(filename, attachment);
            return objectID.ToString();
        }
    }
}
