using System;
using System.Net;
using System.Net.Mail;
using Moq;
using NUnit.Framework;
using SendGridMail;
using SendGridMail.Transport;

namespace Tests.Transport
{
    [TestFixture]
    public class TestSMTP
    {
        [Test]
        public void TestDeliver()
        {
            var mockMessage = new Mock<ISendGrid>();
            var mime = new MailMessage("test-from@sendgrid.com", "test-to@sendgrid.com", "this is a test", "it is only a test");
            mockMessage.Setup(foo => foo.CreateMimeMessage()).Returns(mime);
            var message = mockMessage.Object;

            var mockClient = new Mock<SMTP.ISmtpClient>();
            mockClient.Setup(foo => foo.Send(mime));
            var client = mockClient.Object;
            var credentials = new NetworkCredential("username", "password");
            var test = SMTP.GetInstance(client, credentials);
            test.Deliver(message);

            mockClient.Verify(foo => foo.Send(mime), Times.Once());
            mockMessage.Verify(foo => foo.CreateMimeMessage(), Times.Once());
        }

        [Test]
        public void TestConstructor()
        {
            //Test on defaults of port 25 and 
            var mock = new Mock<SMTP.ISmtpClient>();
            mock.SetupProperty(foo => foo.EnableSsl);
            var client = mock.Object;
            var credentials = new NetworkCredential("username", "password");
            var test = SMTP.GetInstance(client, credentials);
            mock.Verify(foo => foo.EnableSsl, Times.Never());

            mock = new Mock<SMTP.ISmtpClient>();
            mock.SetupProperty(foo => foo.EnableSsl);
            client = mock.Object;
            credentials = new NetworkCredential("username", "password");
            test = SMTP.GetInstance(client, credentials, port:SMTP.SslPort);
            mock.VerifySet(foo => foo.EnableSsl = true);

            mock = new Mock<SMTP.ISmtpClient>();
            mock.SetupProperty(foo => foo.EnableSsl);
            client = mock.Object;
            credentials = new NetworkCredential("username", "password");
            try
            {
                test = SMTP.GetInstance(client, credentials, port: SMTP.TlsPort);
                Assert.Fail("should have thrown an unsupported port exception");
            }
            catch (NotSupportedException ex)
            {
                Assert.AreEqual("TLS not supported", ex.Message);
            }
            
            
        }
    }
}
