using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class GlobalSuppressions : BaseIntegrationTest
    {
        [Test]
        public void GlobalSuppressionsIntegrationTest()
        {
            string email = "example3@example.com";

            TestGet(email);
            string[] emails = {"example1@example.com", "example2@example.com"};
            TestPost(emails);
            TestDelete("example1@example.com");
            TestDelete("example2@example.com");
        }

        private void TestGet(string email)
        {
            HttpResponseMessage response = Client.GlobalSuppressions.Get(email).Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject(rawString);
            Assert.IsNotNull(jsonObject);
        }

        private void TestPost(string[] emails)
        {
            HttpResponseMessage response = Client.GlobalSuppressions.Post(emails).Result;
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            string recipient_emails = jsonObject.recipient_emails.ToString();
            Assert.IsNotNull(recipient_emails);
        }

        private void TestDelete(string email)
        {
            HttpResponseMessage response = Client.GlobalSuppressions.Delete(email).Result;
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}