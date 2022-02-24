using Newtonsoft.Json.Linq;

using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Xunit;

namespace SendGrid.Tests
{
    public class ResponseTests
    {
        [Fact]
        public async Task DeserializeResponseBodyAsync_NullHttpContent_ReturnsEmptyDictionary()
        {
            var response = new Response(HttpStatusCode.OK, null, null);
            Dictionary<string, dynamic> responseBody = await response.DeserializeResponseBodyAsync();
            Assert.Empty(responseBody);
        }

        [Fact]
        public async Task DeserializeResponseBodyAsync_JsonHttpContent_ReturnsBodyAsDictionary()
        {
            var content = "{\"scopes\": [\"alerts.read\"]}";
            var response = new Response(HttpStatusCode.OK, new StringContent(content), null);
            Dictionary<string, dynamic> responseBody = await response.DeserializeResponseBodyAsync();
            Assert.Equal(new JArray() { "alerts.read" }, responseBody["scopes"]);
        }

        [Fact]
        public void DeserializeResponseHeaders_NullHttpResponseHeaders_ReturnsEmptyDictionary()
        {
            var response = new Response(HttpStatusCode.OK, null, null);
            Dictionary<string, string> responseHeadersDeserialized = response.DeserializeResponseHeaders();
            Assert.Empty(responseHeadersDeserialized);
        }

        [Fact]
        public void DeserializeResponseHeaders_NullHttpResponseHeaders_ReturnsHeadersAsDictionary()
        {
            var message = new HttpResponseMessage();
            message.Headers.Add("HeaderKey", "HeaderValue");
            var response = new Response(HttpStatusCode.OK, null, message.Headers);
            Dictionary<string, string> responseHeadersDeserialized = response.DeserializeResponseHeaders();
            Assert.Equal("HeaderValue", responseHeadersDeserialized["HeaderKey"]);
        }
    }
}
