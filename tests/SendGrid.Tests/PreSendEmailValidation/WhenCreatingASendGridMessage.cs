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
    }
}