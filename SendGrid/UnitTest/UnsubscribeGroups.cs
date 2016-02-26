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
}