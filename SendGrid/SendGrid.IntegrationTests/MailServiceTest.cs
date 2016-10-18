using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace SendGrid.IntegrationTests
{
    [TestClass]
    public class MailServiceTest
    {
        private string _apiKey;
        private SendGrid.SendGridAPIClient _client;

        public MailServiceTest()
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
            SendGrid.Models.Mail mail = new SendGrid.Models.Mail(from, subject, to, content);


            //Act
            _client.Mail.MailRequest = mail;
            var response = await _client.Mail.SendAsync();

            //Assert
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public async Task SendMailToMultipleRecipientTest_Success()
        {
            //Arrange
            Email from = new Email("test@example.com");
            String subject = "Hello World from the SendGrid CSharp Library";
            Email to = new Email("test@example.com");
            Content content = new Content("text/plain", "Textual content");
            SendGrid.Models.Mail mail = new SendGrid.Models.Mail(from, subject, to, content);

            Personalization personalitzation = new Personalization();
            Email email = new Email();
            email.Name = "Example User";
            email.Address = "test1@example.com";
            personalitzation.AddTo(email);
            mail.AddPersonalization(personalitzation);

            //Act
            _client.Mail.MailRequest = mail;
            var response = await _client.Mail.SendAsync();

            //Assert
            Assert.IsTrue(response.Success);
        }
    }
}
