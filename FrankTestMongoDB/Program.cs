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
            //BB();
            //AA();
            DeleteTest();
        }

        private static void AA()
        {
            try
            {
                AbstractAttachmentAccess aa = new AttachmentAccess();

                string filepath = @"D:\Documents\kb sql\111.txt";

                byte[] byteImg = File.ReadAllBytes(filepath);
                string id = aa.InsertAttachment(byteImg);

                var doc = aa.GetAttachmentBytesByObjId(id);

                Console.Out.WriteLine(id);
                Console.Out.WriteLine(doc);
                Console.ReadKey();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void BB()
        {
            try
            {
                AbstractAttachmentAccess bb = new AttachmentGFSAccess();


                string filepath = @"D:\Documents\kb sql\111.txt";

                byte[] byteImg = File.ReadAllBytes(filepath);

                //string id = "597047d03f47be3ed0548dac";

                string id = bb.InsertAttachment(byteImg);

                var doc = bb.GetAttachmentBytesByObjId(id);   //实验证明，返回的byte[]和传入的参数是一致的。


                Console.Out.WriteLine(id);
                Console.Out.WriteLine(doc);
                Console.ReadKey();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void DeleteTest()
        {
            try
            {
                string id = "59706b0d3f47be236cc9e42a";
                AbstractAttachmentAccess aaa = new AttachmentAccess();
                aaa.DeleteAttachmentByObjId(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void DeleteGfsTest()
        {
            string id = "59706b0d3f47be236cc9e42a";
            AbstractAttachmentAccess aaa = new AttachmentAccess();
            aaa.DeleteAttachmentByObjId(id);
        }
    }
}
