using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebsiteAsImageWebService.Api;

using System.IO;    
namespace WebsiteAsImageWebService.Api.Tests
{
    [TestClass]
    public class Test
    {

        [TestMethod]
        public void TestMethod1()
        {
            var client = new WebsiteAsImageWebService.Api.WebsiteAsImageClient("localhost:8080");
            var stream = client.GetScreenshot("http://www.google.com").Result;
            using (var fileStream = File.Create("e:/testimage.png"))
            {
                stream.CopyTo(fileStream);
            }

            //stream.SaveToFile("e:/testimage.png"); // futured functionality

        }
    }
}
