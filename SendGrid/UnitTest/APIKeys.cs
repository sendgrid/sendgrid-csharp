using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class APIKeys : BaseIntegrationTest
    {
        private static string _apiKeyId;

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
            HttpResponseMessage response = Client.ApiKeys.Get().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            string jsonString = jsonObject.result.ToString();
            Assert.IsNotNull(jsonString);
        }

        private void TestPost()
        {
            HttpResponseMessage response = Client.ApiKeys.Post("CSharpTestKey").Result;
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            string api_key = jsonObject.api_key.ToString();
            _apiKeyId = jsonObject.api_key_id.ToString();
            string name = jsonObject.name.ToString();
            Assert.IsNotNull(api_key);
            Assert.IsNotNull(_apiKeyId);
            Assert.IsNotNull(name);
        }

        private void TestPatch()
        {
            HttpResponseMessage response = Client.ApiKeys.Patch(_apiKeyId, "CSharpTestKeyPatched").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            _apiKeyId = jsonObject.api_key_id.ToString();
            string name = jsonObject.name.ToString();
            Assert.IsNotNull(_apiKeyId);
            Assert.IsNotNull(name);
        }

        private void TestDelete()
        {
            HttpResponseMessage response = Client.ApiKeys.Delete(_apiKeyId).Result;
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public void TestGetOnce()
        {
            var responseGet = Client.ApiKeys.Get().Result;
        }

        [Test]
        public void TestGetTenTimes()
        {
            HttpResponseMessage responseGet;
            for (int i = 0; i < 10; i++)
            {
                responseGet = Client.ApiKeys.Get().Result;
            }
        }

        [Test]
        public void TestGetTenTimesAsync()
        {
            Task[] tasks = new Task[10];
            for (int i = 0; i < 10; i++)
            {
                tasks[i] = Client.ApiKeys.Get();
            }
            Task.WaitAll(tasks);
        }
    }
}