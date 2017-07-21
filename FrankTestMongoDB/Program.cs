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
using System.Net.Http;
using System.Net;

namespace FrankTestMongoDB
{
    class Program
    {
        static MongoDBHelp de = new MongoDBHelp();
        static void Main(string[] args)
        {
            //BB();
            //AA();
            //DeleteTest();
            GetWebServiceTest();
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

        /// <summary>
        /// HttpClient 异步调用方式GetAsync
        /// </summary>
        private static void GetWebServiceTest()
        {
            try
            {
                string url = string.Format("http://localhost:59687/api/Attachment?objid={0}", "597069a23f47be040cb0843d");

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.MaxResponseContentBufferSize = 256000;

                    httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");

                    HttpResponseMessage response = httpClient.GetAsync(new Uri(url)).Result;

                    String result = response.Content.ReadAsStringAsync().Result;    //结果正常：new BinData(0, \"MTIzNDU2Nzg5MA==\")"
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
