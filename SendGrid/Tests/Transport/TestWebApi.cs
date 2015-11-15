﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SendGrid;

namespace Tests.Transport
{
    public class FakeHttpSendMessageHandler : HttpMessageHandler
    {
        public virtual HttpResponseMessage Send(HttpRequestMessage request)
        {
            throw new NotImplementedException("Ensure you setup this method as part of your test.");
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Send(request));
        }
    }

	[TestFixture]
	internal class TestWebApi
	{
		private const string TestUsername = "username";
		private const string TestPassword = "password";

        private static readonly NetworkCredential Credentials = new NetworkCredential(TestUsername , TestPassword);

		[Test]
		public void TestFetchFileBodies()
		{
			var webApi = new Web(Credentials);
			var message = new Mock<ISendGrid>();
			var attachments = new[] {"foo", "bar", "foobar"};
			message.SetupProperty(foo => foo.Attachments, null);
			var result = webApi.FetchFileBodies(message.Object);
			Assert.AreEqual(0, result.Count);

			message.SetupProperty(foo => foo.Attachments, attachments);
			result = webApi.FetchFileBodies(message.Object);
			Assert.AreEqual(attachments.Count(), result.Count);
			for (var index = 0; index < attachments.Length; index++)
				Assert.AreEqual(result[index].Value.Name, attachments[index]);
		}

		[Test]
		public void TestFetchFormParams()
		{
			// Test Variables
			const string toAddress = "foobar@outlook.com";
		    const string ccAddress = "cc@outlook.com";
		    const string bcc1Address = "bcc1@outlook.com";
		    const string bcc2Address = "bcc2@outlook.com";
		    MailAddress[] bccAddresses = {new MailAddress(bcc1Address), new MailAddress(bcc2Address)};
			const string fromAddress = "test@outlook.com";
			const string subject = "Test Subject";
			const string textBody = "Test Text Body";
			const string htmlBody = "<p>Test HTML Body</p>";
			const string headerKey = "headerkey";
			var testHeader = new Dictionary<string, string> { { headerKey, "headervalue" } };
			const string categoryName = "Example Category";

			var message = new SendGridMessage();
			message.AddTo(toAddress);
            message.AddCc(ccAddress);
		    message.Bcc = bccAddresses;
			message.From = new MailAddress(fromAddress);
			message.Subject = subject;
			message.Text = textBody;
			message.Html = htmlBody;
			message.AddHeaders(testHeader);
			message.Header.SetCategory(categoryName);

			var webApi = new Web(Credentials);
			var result = webApi.FetchFormParams(message);
			Assert.True(result.Any(r => r.Key == "api_user" && r.Value == TestUsername));
			Assert.True(result.Any(r => r.Key == "api_key" && r.Value == TestPassword));
			Assert.True(result.Any(r => r.Key == "to[]" && r.Value == toAddress));
            Assert.True(result.Any(r => r.Key == "cc[]" && r.Value == ccAddress));
            Assert.True(result.Any(r => r.Key == "bcc[]" && r.Value == bcc1Address));
            Assert.True(result.Any(r => r.Key == "bcc[]" && r.Value == bcc2Address));
			Assert.True(result.Any(r => r.Key == "from" && r.Value == fromAddress));
			Assert.True(result.Any(r => r.Key == "subject" && r.Value == subject));
			Assert.True(result.Any(r => r.Key == "text" && r.Value == textBody));
			Assert.True(result.Any(r => r.Key == "html" && r.Value == htmlBody));
			Assert.True(
				result.Any(
					r => r.Key == "headers" && r.Value == String.Format("{{\"{0}\":\"{1}\"}}", headerKey, testHeader[headerKey])));
			Assert.True(
				result.Any(r => r.Key == "x-smtpapi" && r.Value == String.Format("{{\"category\" : \"{0}\"}}", categoryName)));
		}

	    [Test]
	    public void Test_ConstructWithClient_DelviersAMessage()
	    {
	        var handler = new Mock<FakeHttpSendMessageHandler> { CallBase = true };
            handler
                .Setup(h => h.Send(It.IsNotNull<HttpRequestMessage>()))
                .Returns(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("<xml><result>success</result></xml>")
                });

	        using (var client = new HttpClient(handler.Object))
	        {
                var webApi = new Web(Credentials, client);
                var message = new SendGridMessage
                {
                    Subject = "Hello Test",
                    From = new MailAddress("test@test.com")
                };

                Assert.DoesNotThrow(async () => await webApi.DeliverAsync(message));
	        }

            handler.VerifyAll();
	    }
	}
}