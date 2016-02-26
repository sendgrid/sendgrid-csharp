using System;
using System.Net;
using System.Net.Http;
using NUnit.Framework;
using SendGrid;
using Newtonsoft.Json;

namespace UnitTest
{
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

        private void TestGet(string startDate, string endDate = null, string aggregatedBy = null)
        {
            HttpResponseMessage response = client.GlobalStats.Get(startDate, endDate, aggregatedBy).Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject(rawString);
            Assert.IsNotNull(jsonObject);
        }
    }
}