using System.Net;
using System.Net.Http;
using NUnit.Framework;
using Newtonsoft.Json;

namespace UnitTest
{
    [TestFixture]
    public class GlobalStats : BaseIntegrationTest
    {
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

        private void TestGet(string startDate, string endDate = null, string aggregatedBy = null)
        {
            HttpResponseMessage response = Client.GlobalStats.Get(startDate, endDate, aggregatedBy).Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject(rawString);
            Assert.IsNotNull(jsonObject);
        }
    }
}