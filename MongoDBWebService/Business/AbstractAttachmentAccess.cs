using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBWebService
{
   public abstract class AbstractAttachmentAccess
    {
        protected MongoDBHelp helper;
       
        public abstract string InsertAttachment(byte[] attachment);
        public abstract string GetAttachmentByObjId(string objId);
        public abstract byte[] GetAttachmentBytesByObjId(string objId);
        public abstract bool DeleteAttachmentByObjId(string objId);
        
    }
}
