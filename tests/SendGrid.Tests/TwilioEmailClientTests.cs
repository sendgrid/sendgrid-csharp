namespace SendGrid.Tests
{
    using System;
    using Xunit;

    public class TwilioEmailClientTests
    {
        [Fact]
        public void TestClientOptionsConstruction()
        {
            var options = new TwilioEmailClientOptions("username", "password");
            Assert.Equal("https://email.twilio.com", options.Host);
            Assert.Equal("Basic", options.Auth.Scheme);
            Assert.Equal("dXNlcm5hbWU6cGFzc3dvcmQ=", options.Auth.Parameter);
        }

        [Fact]
        public void TestNullUsernameOrPassword()
        {
            Assert.Throws<ArgumentNullException>(() => new TwilioEmailClientOptions(null, "password"));
            Assert.Throws<ArgumentNullException>(() => new TwilioEmailClientOptions("username", null));
        }
    }
}
