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
}