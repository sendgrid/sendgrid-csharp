using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SendGrid.Permissions;
using Xunit;

namespace SendGrid.Tests
{
    public class SendGridClientExtensionsTests
    {
        [Fact]
        public async Task MasksPermissionsForAGivenClient()
        {
            var content = "{\"scopes\": [\"alerts.read\"]}";
            var response = new Response(HttpStatusCode.OK, new StringContent(content), null);
            var mockClient = new Mock<ISendGridClient>();
            mockClient.Setup(x => x.RequestAsync(BaseClient.Method.GET, null, null, "scopes", default))
                .ReturnsAsync(response);

            var builder = await mockClient.Object.CreateMaskedPermissionsBuilderForClient();

            builder.Include("mail.send", "alerts.read");
            var scopes = builder.Build().ToArray();

            Assert.Single(scopes);
            Assert.Contains("alerts.read", scopes);
        }

        [Fact]
        public async Task CallsApiWithBuiltScopes()
        {
            string[] requestedScopes = null;
            string requestedApiKeyName = null;
            var mockClient = new Mock<ISendGridClient>();
            mockClient.Setup(x => x.RequestAsync(BaseClient.Method.POST, It.IsAny<string>(), null, "api_keys", default))
                .Callback<BaseClient.Method, string, string, string, CancellationToken>((method, body, query, path, token) =>
                {
                    JObject json = JsonConvert.DeserializeObject(body) as JObject;
                    requestedScopes = (json["scopes"] as JArray).Select(x => x.Value<string>()).ToArray();
                    requestedApiKeyName = (json["name"]).Value<string>();
                })
                .ReturnsAsync(new Response(HttpStatusCode.Created, null, null));

            var builder = new SendGridPermissionsBuilder();
            builder.AddPermissionsFor(SendGridPermission.Alerts);
            var response = await mockClient.Object.CreateApiKey(builder, "Alerts Test");

            Assert.Equal(4, requestedScopes.Length);
            Assert.Contains("alerts.create", requestedScopes);
            Assert.Contains("alerts.delete", requestedScopes);
            Assert.Contains("alerts.read", requestedScopes);
            Assert.Contains("alerts.update", requestedScopes);

            Assert.Equal("Alerts Test", requestedApiKeyName);
        }
    }
}
