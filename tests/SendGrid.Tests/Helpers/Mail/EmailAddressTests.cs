using SendGrid.Helpers.Mail;
using Xunit;

namespace SendGrid.Tests.Helpers.Mail
{
    public class EmailAddressTests
    {
        [Fact]
        public void TestEmailAddressEquality()
        {
            var left = new EmailAddress("test1@sendgrid.com", "test");
            var right = new EmailAddress("test1@sendGrid.com", "Test");
            var up = new EmailAddress("test2@sendgrid.com", "test");
            var down = new EmailAddress("test2@sendgrid.com", "Test");

            Assert.True(left.Equals(left));
            Assert.True(left.Equals(right));
            Assert.False(left.Equals(up));
            Assert.False(left.Equals(down));
        }
    }
}
