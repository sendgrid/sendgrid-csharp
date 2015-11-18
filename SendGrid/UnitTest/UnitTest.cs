using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using SendGrid;

namespace UnitTest
{
    [TestFixture]
    public class APIKeys
    {
        static string _baseUri = "https://api.sendgrid.com/";
        static string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
        public Client client = new Client(_apiKey, _baseUri);
        private static string _api_key_id = "";

        [Test]
        public void ApiKeysIntegrationTest()
        {
            TestGet();
            TestPost();
            TestPatch();
            TestDelete();
        }

        private void TestGet()
        {
            HttpResponseMessage response = client.ApiKeys.Get().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            string jsonString = jsonObject.result.ToString();
            Assert.IsNotNull(jsonString);
        }

        private void TestPost()
        {
            HttpResponseMessage response = client.ApiKeys.Post("CSharpTestKey").Result;
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            string api_key = jsonObject.api_key.ToString();
            _api_key_id = jsonObject.api_key_id.ToString();
            string name = jsonObject.name.ToString();
            Assert.IsNotNull(api_key);
            Assert.IsNotNull(_api_key_id);
            Assert.IsNotNull(name);
        }

        private void TestPatch()
        {
            HttpResponseMessage response = client.ApiKeys.Patch(_api_key_id, "CSharpTestKeyPatched").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            _api_key_id = jsonObject.api_key_id.ToString();
            string name = jsonObject.name.ToString();
            Assert.IsNotNull(_api_key_id);
            Assert.IsNotNull(name);
        }

        private void TestDelete()
        {
            HttpResponseMessage response = client.ApiKeys.Delete(_api_key_id).Result;
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public void TestGetOnce()
        {
            var responseGet = client.ApiKeys.Get().Result;
        }

        [Test]
        public void TestGetTenTimes()
        {
            HttpResponseMessage responseGet;
            for (int i = 0; i < 10; i++)
            {
                responseGet = client.ApiKeys.Get().Result;
            }
        }

        [Test]
        public void TestGetTenTimesAsync()
        {
            Task[] tasks = new Task[10];
            for (int i = 0; i < 10; i++)
            {
                tasks[i] = client.ApiKeys.Get();
            }
            Task.WaitAll(tasks);
        }
    }
}
