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
            Email to = new Email("rogercalaf@gmail.com");
            Content content = new Content("text/plain", "Textual content");
            SendGrid.Models.Mail mail = new SendGrid.Models.Mail(from, subject, to, content);
            _client.Mail.MailRequest = mail;

            //Act
            var response = await _client.Mail.SendAsync();

            //Assert
            Assert.IsTrue(response.Success);
        }
    }
}
