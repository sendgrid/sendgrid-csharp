using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class UnsubscribeGroups : BaseIntegrationTest
    {
        private static string _unsubscribeGroupsKeyId = "";

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
            HttpResponseMessage response = Client.UnsubscribeGroups.Get().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject(rawString);
            Assert.IsNotNull(jsonObject);
        }

        private void TestGetUnique(int unsubscribeGroupId)
        {
            HttpResponseMessage response = Client.UnsubscribeGroups.Get(unsubscribeGroupId).Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject(rawString);
            Assert.IsNotNull(jsonObject);
        }

        private void TestPost()
        {
            HttpResponseMessage response =
                Client.UnsubscribeGroups.Post("C Sharp Unsubscribes", "Testing the C Sharp Library", false).Result;
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            string name = jsonObject.name.ToString();
            string description = jsonObject.description.ToString();
            _unsubscribeGroupsKeyId = jsonObject.id.ToString();
            bool is_default = jsonObject.is_default;
            Assert.IsNotNull(name);
            Assert.IsNotNull(description);
            Assert.IsNotNull(_unsubscribeGroupsKeyId);
            Assert.IsNotNull(is_default);
        }

        private void TestDelete()
        {
            HttpResponseMessage response = Client.UnsubscribeGroups.Delete(_unsubscribeGroupsKeyId).Result;
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}