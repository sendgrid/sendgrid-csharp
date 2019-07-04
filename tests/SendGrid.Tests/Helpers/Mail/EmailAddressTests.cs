using SendGrid.Helpers.Mail;
using Xunit;

namespace SendGrid.Tests.Helpers.Mail
{
    public class EmailAddressTests
    {
        [Fact]
        public void TestEmailAddressEquality()
        {
            var left = new EmailAddress("test@sendgrid.com", "Test");
            var right = new EmailAddress("test@sendGrid.com", "Test");
            var up = new EmailAddress("test@sendgrid.com", "test");
            var down = new EmailAddress("test@sendgrid.com", "Test");

            Assert.True(left.Equals(left));
            Assert.True(left.Equals(down));
            Assert.False(left.Equals(right));
            Assert.False(left.Equals(up));
        }
    }
}
