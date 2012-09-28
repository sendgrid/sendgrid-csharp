using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Moq;
using NUnit.Framework;
using SendGrid;
using SendGrid.Transport;

namespace SendGrid.Tests.Transport
{
    [TestFixture]
    class TestWeb
    {
        [Test]
        public void TestFetchFileBodies()
        {
            var test = Web.GetInstance(new NetworkCredential("foo", "bar"));
            var message = new Mock<IMail>();
            message.SetupProperty(foo => foo.Attachments, null);
            var result = test.FetchFileBodies(message.Object);
            Assert.AreEqual(0, result.Count);

            message.SetupProperty(foo => foo.Attachments, new string[] {"foo", "bar", "raz"});
            result = test.FetchFileBodies(message.Object);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(result[0].Key, "foo");
            Assert.AreEqual(result[1].Key, "bar");
            Assert.AreEqual(result[2].Key, "raz");
            Assert.AreEqual(result[0].Value.Name, "foo");
            Assert.AreEqual(result[1].Value.Name, "bar");
            Assert.AreEqual(result[2].Value.Name, "raz");
        }

        [Test]
        public void TestFetchFormParams()
        {
            var bar = Web.GetInstance(new NetworkCredential("usr", "psswd"));
            var message = Mail.GetInstance();
            message.AddTo("foo@bar.com");
            message.AddCc("cc@bar.com");
            message.AddBcc("bcc@bar.com");
            message.From = new MailAddress("from@raz.com");
            message.Subject = "subject";
            message.Text = "text";
            message.Html = "html";
            message.AddHeaders(new Dictionary<string, string>{{"headerkey", "headervalue"}});
            message.Header.SetCategory("cat");

            var result = bar.FetchFormParams(message);
            Assert.True(result.Any(r => r.Key == "api_user" && r.Value == "usr"));
            Assert.True(result.Any(r => r.Key == "api_key" && r.Value == "psswd"));
            Assert.True(result.Any(r => r.Key == "to[]" && r.Value == "foo@bar.com"));
            Assert.True(result.Any(r => r.Key == "cc[]" && r.Value == "cc@bar.com"));
            Assert.True(result.Any(r => r.Key == "bcc[]" && r.Value == "bcc@bar.com"));
            Assert.True(result.Any(r => r.Key == "from" && r.Value == "from@raz.com"));
            Assert.True(result.Any(r => r.Key == "subject" && r.Value == "subject"));
            Assert.True(result.Any(r => r.Key == "text" && r.Value == "text"));
            Assert.True(result.Any(r => r.Key == "html" && r.Value == "html"));
            Assert.True(result.Any(r => r.Key == "headers" && r.Value == "{\"headerkey\":\"headervalue\"}"));
            Assert.True(result.Any(r => r.Key == "x-smtpapi" && r.Value == "{\"category\" : \"cat\"}"));
            Assert.True(result.Any(r => r.Key == "html" && r.Value == "html"));
        }
    }
}
