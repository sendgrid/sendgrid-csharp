using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace Inbound.Tests.IntegrationTests
{
    public class InboundEndpointsTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public InboundEndpointsTests(WebApplicationFactory<Startup> factory)
            => _factory = factory;

        [Fact]
        public async Task Get_IndexPageReturnsSuccessAndCorrectContentType()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            response.Content.Headers.ContentType.MediaType.ShouldBe("text/html");
        }

        [Fact]
        public async Task Get_InboundEndpointShouldNotReturnsOk()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/inbound");
            response.StatusCode.ShouldNotBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Post_InboundEndpointWithDefaultPayload()
        {
            var data = await File.ReadAllTextAsync("sample_data/default_data.txt");

            using var content = new StringContent(data);
            content.Headers.Clear();
            content.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data")
            {
                Parameters = { new NameValueHeaderValue("boundary","xYzZY") }
            };

            var client = _factory.CreateClient();
            var response = await client.PostAsync("/inbound", content);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Post_InboundEndpointWithRawPayloadWithAttachments()
        {
            var data = await File.ReadAllTextAsync("sample_data/raw_data_with_attachments.txt");

            using var content = new StringContent(data);
            content.Headers.Clear();
            content.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data")
            {
                Parameters = { new NameValueHeaderValue("boundary","xYzZY") }
            };

            var client = _factory.CreateClient();
            var response = await client.PostAsync("/inbound", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
