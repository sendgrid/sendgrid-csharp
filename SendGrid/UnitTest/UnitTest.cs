using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using SendGrid;
using Newtonsoft.Json;

namespace UnitTest
{
    [TestFixture]
    public class APIKeys
    {
        static string _baseUri = "https://api.sendgrid.com/";
        static string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
        static string _sendGridUsername = Environment.GetEnvironmentVariable("SENDGRID_USERNAME");
        static string _sendGridPassword = Environment.GetEnvironmentVariable("SENDGRID_PASSWORD");
        public Client client;
        private static string _api_key_id = "";

        [Test]
        public void ApiKeysIntegrationTestUsingApiKey()
        {
            client = new Client(_apiKey, _baseUri);
            TestGet();
            TestPost();
            TestPatch();
            TestDelete();
        }

        [Test]
        public void ApiKeysIntegrationTestUsingCredentials()
        {
            client = new Client(new NetworkCredential(_sendGridUsername, _sendGridPassword), _baseUri);
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

    [TestFixture]
    public class UnsubscribeGroups
    {
        static string _baseUri = "https://api.sendgrid.com/";
        static string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
        public Client client = new Client(_apiKey, _baseUri);
        private static string _unsubscribe_groups_key_id = "";

        [Test]
        public void UnsubscribeGroupsIntegrationTest()
        {
            int unsubscribeGroupId = 69;

            TestGet();
            TestGetUnique(unsubscribeGroupId);
            TestPost();
            TestDelete();
        }

        private void TestGet()
        {
            HttpResponseMessage response = client.UnsubscribeGroups.Get().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject(rawString);
            Assert.IsNotNull(jsonObject);
        }

        private void TestGetUnique(int unsubscribeGroupId)
        {
            HttpResponseMessage response = client.UnsubscribeGroups.Get(unsubscribeGroupId).Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject(rawString);
            Assert.IsNotNull(jsonObject);
        }

        private void TestPost()
        {
            HttpResponseMessage response = client.UnsubscribeGroups.Post("C Sharp Unsubscribes", "Testing the C Sharp Library", false).Result;
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            string name = jsonObject.name.ToString();
            string description = jsonObject.description.ToString();
            _unsubscribe_groups_key_id = jsonObject.id.ToString();
            bool is_default = jsonObject.is_default;
            Assert.IsNotNull(name);
            Assert.IsNotNull(description);
            Assert.IsNotNull(_unsubscribe_groups_key_id);
            Assert.IsNotNull(is_default);
        }

        private void TestDelete()
        {
            HttpResponseMessage response = client.UnsubscribeGroups.Delete(_unsubscribe_groups_key_id).Result;
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

    }

    [TestFixture]
    public class Suppressions
    {
        static string _baseUri = "https://api.sendgrid.com/";
        static string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
        public Client client = new Client(_apiKey, _baseUri);

        [Test]
        public void SuppressionsIntegrationTest()
        {
            int unsubscribeGroupId = 69;

            TestGet(unsubscribeGroupId);
            string[] emails = { "example@example.com", "example2@example.com" };
            TestPost(unsubscribeGroupId, emails);
            TestDelete(unsubscribeGroupId, "example@example.com");
            TestDelete(unsubscribeGroupId, "example2@example.com");
        }

        private void TestGet(int unsubscribeGroupId)
        {
            HttpResponseMessage response = client.Suppressions.Get(unsubscribeGroupId).Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject(rawString);
            Assert.IsNotNull(jsonObject);
        }

        private void TestPost(int unsubscribeGroupId, string[] emails)
        {
            HttpResponseMessage response = client.Suppressions.Post(unsubscribeGroupId, emails).Result;
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            string recipient_emails = jsonObject.recipient_emails.ToString();
            Assert.IsNotNull(recipient_emails);
        }

        private void TestDelete(int unsubscribeGroupId, string email)
        {
            HttpResponseMessage response = client.Suppressions.Delete(unsubscribeGroupId, email).Result;
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

    }

    [TestFixture]
    public class GlobalSuppressions
    {
        static string _baseUri = "https://api.sendgrid.com/";
        static string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
        public Client client = new Client(_apiKey, _baseUri);

        [Test]
        public void GlobalSuppressionsIntegrationTest()
        {
            string email = "example3@example.com";

            TestGet(email);
            string[] emails = { "example1@example.com", "example2@example.com" };
            TestPost(emails);
            TestDelete("example1@example.com");
            TestDelete("example2@example.com");
        }

        private void TestGet(string email)
        {
            HttpResponseMessage response = client.GlobalSuppressions.Get(email).Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject(rawString);
            Assert.IsNotNull(jsonObject);
        }

        private void TestPost(string[] emails)
        {
            HttpResponseMessage response = client.GlobalSuppressions.Post(emails).Result;
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            string recipient_emails = jsonObject.recipient_emails.ToString();
            Assert.IsNotNull(recipient_emails);
        }

        private void TestDelete(string email)
        {
            HttpResponseMessage response = client.GlobalSuppressions.Delete(email).Result;
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }

    [TestFixture]
    public class GlobalStats
    {
        static string _baseUri = "https://api.sendgrid.com/";
        static string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
        public Client client = new Client(_apiKey, _baseUri);

        [Test]
        public void GlobalStatsIntegrationTest()
        {
            string startDate = "2015-11-01";
            string endDate = "2015-12-01";
            string aggregatedBy = "day";
            TestGet(startDate);
            TestGet(startDate, endDate);
            TestGet(startDate, endDate, aggregatedBy);
            aggregatedBy = "week";
            TestGet(startDate, endDate, aggregatedBy);
            aggregatedBy = "month";
            TestGet(startDate, endDate, aggregatedBy);
        }

        private void TestGet(string startDate, string endDate=null, string aggregatedBy=null)
        {
            HttpResponseMessage response = client.GlobalStats.Get(startDate, endDate, aggregatedBy).Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject(rawString);
            Assert.IsNotNull(jsonObject);
        }
    }
}
