using System;
using System.Collections.Generic;
using SendGrid.Helpers.Mail;
using Xunit;

namespace SendGrid.Tests.PreSendEmailValidation
{
    public class WhenCreatingASendGridMessage
    {
        [Fact]
        public void WithAnEmptyListOfBlindCarbonCopiesThenAnExceptionIsThrown()
        {
            var sendGridMessage = MailHelper.CreateSingleEmail(new EmailAddress(), new EmailAddress(), string.Empty, string.Empty, string.Empty);
            Assert.Throws<InvalidOperationException>(() => { sendGridMessage.AddBccs(new List<EmailAddress>()); });
        }

        [Fact]
        public void WithANullListOfBlindCarbonCopiesThenAnExceptionIsThrown()
        {
            var sendGridMessage = MailHelper.CreateSingleEmail(new EmailAddress(), new EmailAddress(), string.Empty, string.Empty, string.Empty);
            Assert.Throws<ArgumentNullException>(() => { sendGridMessage.AddBccs(null); });
        }

        [Fact]
        public void WithANullBlindCarbonCopyThenAnExceptionIsThrown()
        {
            var sendGridMessage = MailHelper.CreateSingleEmail(new EmailAddress(), new EmailAddress(), string.Empty, string.Empty, string.Empty);
            Assert.Throws<ArgumentNullException>(() => { sendGridMessage.AddBcc(null, 0); });
        }

        [Fact]
        public void WithAnEmptyListOfCarbonCopiesThenAnExceptionIsThrown()
        {
            var sendGridMessage = MailHelper.CreateSingleEmail(new EmailAddress(), new EmailAddress(), string.Empty, string.Empty, string.Empty);
            Assert.Throws<InvalidOperationException>(() => { sendGridMessage.AddCcs(new List<EmailAddress>()); });
        }

        [Fact]
        public void WithANullListOfCarbonCopiesThenAnExceptionIsThrown()
        {
            var sendGridMessage = MailHelper.CreateSingleEmail(new EmailAddress(), new EmailAddress(), string.Empty, string.Empty, string.Empty);
            Assert.Throws<InvalidOperationException>(() => { sendGridMessage.AddCcs(new List<EmailAddress>()); });
        }

        [Fact]
        public void WithANullCarbonCopyThenAnExceptionIsThrown()
        {
            var sendGridMessage = MailHelper.CreateSingleEmail(new EmailAddress(), new EmailAddress(), string.Empty, string.Empty, string.Empty);
            Assert.Throws<ArgumentNullException>(() => { sendGridMessage.AddCc(null, 0); });
        }
    }
}