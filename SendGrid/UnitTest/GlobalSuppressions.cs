using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using SendGrid;

namespace UnitTest
{
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
}