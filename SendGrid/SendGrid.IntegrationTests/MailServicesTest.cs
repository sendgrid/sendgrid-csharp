using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace SendGrid.IntegrationTests
{
    [TestClass]
    public class MailServicesTest
    {
        private string _apiKey;
        private SendGrid.SendGridAPIClient _client;

        public MailServicesTest()
        {
            _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            _client = new SendGridAPIClient(_apiKey, "https://api.sendgrid.com");
        }

        [TestMethod]
        public async Task SendMailToSingleRecipientTest_Success()
        {

            //Arrange
            Email from = new Email("test@example.com");
            String subject = "Hello World from the SendGrid CSharp Library";
            Email to = new Email("test@example.com");
            Content content = new Content("text/plain", "Textual content");
            Mail mail = new Mail(from, subject, to, content);
            Email email = new Email("test2@example.com");
            mail.Personalization[0].AddTo(email);
            _client.Mail.MailRequest = mail;

            //Act
            var response = await _client.Mail.SendAsync();

            //Assert
            Assert.IsTrue(response.Success);
        }
    }
}
