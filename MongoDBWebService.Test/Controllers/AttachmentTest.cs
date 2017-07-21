using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDBWebService.Controllers;
using System.Web.Mvc;

namespace MongoDBWebService.Test.Controllers
{
    [TestClass]
    public class AttachmentTest
    {
        [TestMethod]
        public void TestMethod1()
        {

        }

        [TestMethod]
        public void GetTest()
        { 
            AttachmentController controller = new AttachmentController();
             
            string result = controller.Get("597069a23f47be040cb0843d");

            // 断言
            Assert.IsNotNull(result);
            //Assert.AreEqual("Home Page", result);
        }

        [TestMethod]
        public void PostTest()
        {
            AttachmentController controller = new AttachmentController();

            string result = controller.Post("");
            // 断言
            Assert.IsNotNull(result);
            //Assert.AreEqual("Home Page", result);
        }
    }
}
