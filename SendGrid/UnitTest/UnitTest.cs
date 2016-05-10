using NUnit.Framework;
using SendGrid;
using SendGrid.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var apiKeys = client.ApiKeys.GetAsync().Result;
            Assert.IsNotNull(apiKeys);
        }

        private void TestPost()
        {
            var keyName = "CSharpTestKey";
            var apiKey = client.ApiKeys.CreateAsync(keyName).Result;
            _api_key_id = apiKey.KeyId;
            Assert.IsNotNull(apiKey);
            Assert.IsNotNull(apiKey.KeyId);
            Assert.AreEqual(keyName, apiKey.Name);
        }

        private void TestPatch()
        {
            var updatedKeyName = "CSharpTestKeyPatched";
            var apiKey = client.ApiKeys.UpdateAsync(_api_key_id, updatedKeyName).Result;
            Assert.IsNotNull(apiKey.KeyId);
            Assert.AreEqual(updatedKeyName, apiKey.Name);
        }

        private void TestDelete()
        {
            client.ApiKeys.DeleteAsync(_api_key_id).Wait();
        }

        [Test]
        public void TestGetOnce()
        {
            var responseGet = client.ApiKeys.GetAsync().Result;
        }

        [Test]
        public void TestGetTenTimes()
        {
            IEnumerable<ApiKey> apiKeys;
            for (int i = 0; i < 10; i++)
            {
                apiKeys = client.ApiKeys.GetAsync().Result;
            }
        }

        [Test]
        public void TestGetTenTimesAsync()
        {
            var tasks = new Task[10];
            for (int i = 0; i < 10; i++)
            {
                tasks[i] = client.ApiKeys.GetAsync();
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
        private static int _unsubscribe_groups_key_id;

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
            var unsubscribeGroups = client.UnsubscribeGroups.GetAllAsync().Result;
            Assert.IsNotNull(unsubscribeGroups);
        }

        private void TestGetUnique(int unsubscribeGroupId)
        {
            var unsubscribeGroup = client.UnsubscribeGroups.GetAsync(unsubscribeGroupId).Result;
            Assert.IsNotNull(unsubscribeGroup);
        }

        private void TestPost()
        {
            var unsubscribeGroup = client.UnsubscribeGroups.CreateAsync("C Sharp Unsubscribes", "Testing the C Sharp Library", false).Result;
            Assert.IsNotNull(unsubscribeGroup);
            _unsubscribe_groups_key_id = unsubscribeGroup.Id;
            Assert.IsNotNull(unsubscribeGroup.Name);
            Assert.IsNotNull(unsubscribeGroup.Description);
            Assert.IsNotNull(_unsubscribe_groups_key_id);
            Assert.IsFalse(unsubscribeGroup.IsDefault);
        }

        private void TestDelete()
        {
            client.UnsubscribeGroups.DeleteAsync(_unsubscribe_groups_key_id).Wait();
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
            var emailAddresses = client.Suppressions.GetUnsubscribedAddressesAsync(unsubscribeGroupId).Result;
            Assert.IsNotNull(emailAddresses);
        }

        private void TestPost(int unsubscribeGroupId, string[] emails)
        {
            client.Suppressions.AddAddressToUnsubscribeGroupAsync(unsubscribeGroupId, emails).Wait();
        }

        private void TestDelete(int unsubscribeGroupId, string email)
        {
            client.Suppressions.RemoveAddressFromSuppressionGroupAsync(unsubscribeGroupId, email).Wait();
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
            var isUnsubscribed = client.GlobalSuppressions.IsUnsubscribedAsync(email).Result;
            Assert.IsFalse(isUnsubscribed);
        }

        private void TestPost(string[] emails)
        {
            client.GlobalSuppressions.AddAsync(emails).Wait();
        }

        private void TestDelete(string email)
        {
            client.GlobalSuppressions.RemoveAsync(email).Wait();
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
            var startDate = new DateTime(2015, 11, 1);
            var endDate = new DateTime(2015, 12, 1);

            TestGet(startDate);
            TestGet(startDate, endDate);
            TestGet(startDate, endDate, AggregateBy.Day);
            TestGet(startDate, endDate, AggregateBy.Week);
            TestGet(startDate, endDate, AggregateBy.Month);
        }

        private void TestGet(DateTime startDate, DateTime? endDate = null, AggregateBy aggregatedBy = AggregateBy.None)
        {
            var globalStats = client.GlobalStats.GetAsync(startDate, endDate, aggregatedBy).Result;
            Assert.IsNotNull(globalStats);
        }
    }
}
