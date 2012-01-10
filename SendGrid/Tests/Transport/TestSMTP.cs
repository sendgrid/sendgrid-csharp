using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Moq;
using NUnit.Framework;
using SendGridMail.Transport;

namespace Tests.Transport
{
    [TestFixture]
    public class TestSMTP
    {
        [Test]
        public void Deliver()
        {
            var mock = new Mock<SMTP.ISmtpClient>();
            var client = mock.Object;
            var credentials = new NetworkCredential("username", "password");
            SMTP.GenerateInstance(client, credentials);
        }
    }
}
