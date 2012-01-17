using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Moq;
using NUnit.Framework;
using SendGridMail;
using SendGridMail.Transport;

namespace Tests.Transport
{
    [TestFixture]
    class TestREST
    {
        [Test]
        public void TestFetchFileBodies()
        {
            var test = REST.GetInstance(new NetworkCredential("foo", "bar"));
            var message = new Mock<ISendGrid>();
            message.SetupProperty(foo => foo.Attachments, null);
            var result = test.FetchFileBodies(message.Object);
            Assert.AreEqual(0, result.Count);

            message.SetupProperty(foo => foo.Attachments, new string[] {"foo", "bar", "raz"});
            result = test.FetchFileBodies(message.Object);
            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public void TestFetchFormParams()
        {
            var bar = REST.GetInstance(new NetworkCredential("foo", "bar"));
            //bar.FetchFormParams();
        }

        [Test]
        public void TestInitializeTransport()
        {
            var bar = REST.GetInstance(new NetworkCredential("foo", "bar"));
            //bar.InitializeTransport();
        }
    }
}
