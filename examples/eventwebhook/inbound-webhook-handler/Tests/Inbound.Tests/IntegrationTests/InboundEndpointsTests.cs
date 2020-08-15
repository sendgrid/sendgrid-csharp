using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Inbound.Tests.IntegrationTests
{
    public class InboundEndpointsTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> applicationFactory;

        public InboundEndpointsTests(WebApplicationFactory<Startup> factory)
            => applicationFactory = factory;

        [Fact]
        public async Task Get_IndexPageReturnsSuccessAndCorrectContentType()
        {
            const string URL = "/";

            var client = applicationFactory.CreateClient();
            var response = await client.GetAsync(URL);
            response.EnsureSuccessStatusCode();
            response.Content.Headers.ContentType.MediaType.ShouldBe("text/html");
        }

        [Fact]
        public async Task Get_InboundEndpointReturnsNotFound()
        {
            const string URL = "/inbound";
            var client = applicationFactory.CreateClient();
            var response = await client.GetAsync(URL);
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Post_InboundEndpointWithDefaultPayload()
        {
            const string URL = "/inbound";
            var data = File.ReadAllTextAsync("sample_data/default_data.txt").Result;

            var content = new StringContent(data);
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "multipart/form-data; boundary=xYzZY");

            var client = applicationFactory.CreateClient();
            var response = await client.PostAsync(URL, content);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Post_InboundEndpointWithRawPayloadWithAttachments()
        {
            const string URL = "/inbound";
            var data = File.ReadAllTextAsync("sample_data/raw_data_with_attachments.txt").Result;

            var content = new StringContent(data);
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "multipart/form-data; boundary=xYzZY");

            var client = applicationFactory.CreateClient();
            var response = await client.PostAsync(URL, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
