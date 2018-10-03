using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SendGrid.Tests.Helpers.Mail
{
    public class MailHelperTests
    {
        [Theory]
        [InlineData("Name Of A Person+", "send@grid.com", "Name Of A Person+ <   send@grid.com  >   ")]
        [InlineData("", "send@grid.com", "   send@grid.com  ")]
        [InlineData(null, "notAValidEmail", "notAValidEmail")]
        public void TestStringToEmail(string expectedName, string expectedEmail, string rf2822Email)
        {
            var address = MailHelper.StringToEmailAddress(rf2822Email);
            Assert.Equal(expectedEmail, address.Email);
            Assert.Equal(expectedName, address.Name);
        }

        [Fact]
        public void TestCreateSingleTemplateEmail()
        {
            var from = new EmailAddress("from@email.com", "FromName");
            var to = new EmailAddress("to@email.com");
            var templateId = "d-template1";
            var dynamicTemplateData = new Dictionary<string, object>
            {
                { "key1", "value1" }
            };

            var sendGridMessage = MailHelper.CreateSingleTemplateEmail(
                from,
                to,
                templateId,
                dynamicTemplateData);

            Assert.Equal(from, sendGridMessage.From);
            Assert.Equal(to, sendGridMessage.Personalizations.Single().Tos.Single());
            Assert.Equal(templateId, sendGridMessage.TemplateId);
            Assert.Equal(dynamicTemplateData, sendGridMessage.Personalizations.Single().TemplateData);
        }

        [Fact]
        public void TestCreateSingleTemplateEmailToMultipleRecipients()
        {
            var from = new EmailAddress("from@email.com", "FromName");
            var tos = new List<EmailAddress>
            {
                new EmailAddress("to1@email.com"),
                new EmailAddress("to2@email.com")
            };

            var templateId = "d-template2";
            var dynamicTemplateData = new Dictionary<string, object>
            {
                { "key1", "value1" }
            };

            var sendGridMessage = MailHelper.CreateSingleTemplateEmailToMultipleRecipients(
                from,
                tos,
                templateId,
                dynamicTemplateData);

            Assert.Equal(from, sendGridMessage.From);
            Assert.Equal(tos[0], sendGridMessage.Personalizations.ElementAt(0).Tos.Single());
            Assert.Equal(tos[1], sendGridMessage.Personalizations.ElementAt(1).Tos.Single());
            Assert.Equal(templateId, sendGridMessage.TemplateId);
            Assert.Equal(dynamicTemplateData, sendGridMessage.Personalizations.ElementAt(0).TemplateData);
            Assert.Equal(dynamicTemplateData, sendGridMessage.Personalizations.ElementAt(1).TemplateData);
        }

        [Fact]
        public void TestCreateSingleTemplateEmailToMultipleDuplicateRecipients()
        {
            var from = new EmailAddress("from@email.com", "FromName");
            var tos = new List<EmailAddress>
            {
                new EmailAddress("to1@email.com"),
                new EmailAddress("to2@email.com"),
                new EmailAddress("to1@email.com")
            };

            var templateId = "d-template2";
            var dynamicTemplateData = new Dictionary<string, object>
            {
                { "key1", "value1" }
            };

            var sendGridMessage = MailHelper.CreateSingleTemplateEmailToMultipleRecipients(
                from,
                tos,
                templateId,
                dynamicTemplateData);

            Assert.Equal(from, sendGridMessage.From);
            Assert.Equal(tos[0], sendGridMessage.Personalizations.ElementAt(0).Tos.Single());
            Assert.Equal(tos[1], sendGridMessage.Personalizations.ElementAt(1).Tos.Single());
            Assert.Equal(2, sendGridMessage.Personalizations.Count);
            Assert.Equal(templateId, sendGridMessage.TemplateId);
            Assert.Equal(dynamicTemplateData, sendGridMessage.Personalizations.ElementAt(0).TemplateData);
            Assert.Equal(dynamicTemplateData, sendGridMessage.Personalizations.ElementAt(1).TemplateData);
        }

        [Fact]
        public void TestCreateSingleEmailToMultipleDuplicateRecipients()
        {
            var from = new EmailAddress("from@email.com", "FromName");
            var tos = new List<EmailAddress>
            {
                new EmailAddress("to1@email.com"),
                new EmailAddress("to2@email.com"),
                new EmailAddress("to1@email.com")
            };

            var subject = "Mail Subject";
            var plainText = "Plain Text";

            var sendGridMessage = MailHelper.CreateSingleEmailToMultipleRecipients(
                from,
                tos,
                subject,
                plainText,
                null);

            Assert.Equal(from, sendGridMessage.From);
            Assert.Equal(tos[0], sendGridMessage.Personalizations.ElementAt(0).Tos.Single());
            Assert.Equal(tos[1], sendGridMessage.Personalizations.ElementAt(1).Tos.Single());
            Assert.Equal(2, sendGridMessage.Personalizations.Count);
        }

        [Fact]
        public void TestCreateMultipleTemplateEmailsToMultipleRecipients()
        {
            var from = new EmailAddress("from@email.com", "FromName");
            var tos = new List<EmailAddress>
            {
                new EmailAddress("to1@email.com"),
                new EmailAddress("to2@email.com")
            };

            var templateId = "d-template2";
            var dynamicTemplateData = new List<object>
            {
                new { key1 = "value1" },
                new { key2 = "value2" }
            };

            var sendGridMessage = MailHelper.CreateMultipleTemplateEmailsToMultipleRecipients(
                from,
                tos,
                templateId,
                dynamicTemplateData);

            Assert.Equal(from, sendGridMessage.From);
            Assert.Equal(tos[0], sendGridMessage.Personalizations.ElementAt(0).Tos.Single());
            Assert.Equal(tos[1], sendGridMessage.Personalizations.ElementAt(1).Tos.Single());
            Assert.Equal(templateId, sendGridMessage.TemplateId);
            Assert.Equal(dynamicTemplateData[0], sendGridMessage.Personalizations.ElementAt(0).TemplateData);
            Assert.Equal(dynamicTemplateData[1], sendGridMessage.Personalizations.ElementAt(1).TemplateData);
        }

        [Fact]
        public void TestCreateMultipleTemplateEmailsToMultipleDuplicateRecipients()
        {
            var from = new EmailAddress("from@email.com", "FromName");
            var tos = new List<EmailAddress>
            {
                new EmailAddress("to1@email.com"),
                new EmailAddress("to2@email.com"),
                new EmailAddress("to2@email.com")
            };

            var templateId = "d-template2";
            var dynamicTemplateData = new List<object>
            {
                new { key1 = "value1" },
                new { key2 = "value2" }
            };

            var sendGridMessage = MailHelper.CreateMultipleTemplateEmailsToMultipleRecipients(
                from,
                tos,
                templateId,
                dynamicTemplateData);

            Assert.Equal(from, sendGridMessage.From);
            Assert.Equal(tos[0], sendGridMessage.Personalizations.ElementAt(0).Tos.Single());
            Assert.Equal(tos[1], sendGridMessage.Personalizations.ElementAt(1).Tos.Single());
            Assert.Equal(2, sendGridMessage.Personalizations.Count);
            Assert.Equal(templateId, sendGridMessage.TemplateId);
            Assert.Equal(dynamicTemplateData[0], sendGridMessage.Personalizations.ElementAt(0).TemplateData);
            Assert.Equal(dynamicTemplateData[1], sendGridMessage.Personalizations.ElementAt(1).TemplateData);
        }
    }
}
