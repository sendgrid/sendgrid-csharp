using System;
using SendGrid;

namespace UnitTest
{
    public class BaseIntegrationTest
    {
        private readonly string _baseUri = "https://api.sendgrid.com/";
        private readonly string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
        protected readonly Client Client;

        protected BaseIntegrationTest()
        {
            Client = new Client(_apiKey, _baseUri);
        }
    }
}