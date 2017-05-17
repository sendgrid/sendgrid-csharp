using SendGrid.Helpers.Mail;
using Xunit;

namespace SendGrid.Tests.Helpers.Mail
{
    public class MailHelperTests
    {
        [Theory]
        [InlineData("Name Of A Person+", "send@grid.com", "Name Of A Person+ <   send@grid.com  >   ")]
        [InlineData("", "send@grid.com", "   send@grid.com  ")]
        [InlineData(null, "notAValidEmail", "notAValidEmail")]
        public void StringToEmail(string expectedName, string expectedEmail, string rf2822Email)
        {
            var address = MailHelper.StringToEmailAddress(rf2822Email);
            Assert.Equal(expectedEmail, address.Email);
            Assert.Equal(expectedName, address.Name);
        }
    }
}
