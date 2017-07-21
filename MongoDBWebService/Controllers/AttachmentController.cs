using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using System.IO;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace MongoDBWebService.Controllers
{
    public class AttachmentController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/Attachment/objid=597069a23f47be040cb0843d
        public string Get(string objid)
        {
            try
            {
                AbstractAttachmentAccess aa = new AttachmentAccess();
                
                var doc = aa.GetAttachmentBytesByObjId(objid);

                return doc.ToJson();
            }
            catch (Exception)
            {
                throw;
            } 
        }

         
        // POST api/Attachment
        public string Post([FromBody]string value)
        {
            try
            {
                AbstractAttachmentAccess aa = new AttachmentAccess();

                string filepath = @"D:\Documents\kb sql\111.txt";

                byte[] byteImg = File.ReadAllBytes(filepath);

                string _id = aa.InsertAttachment(byteImg);

                return _id;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}