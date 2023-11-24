namespace SendGrid.Tests
{
    using System;
    using System.Threading.Tasks;
    using SendGrid.Helpers.Mail;
    using Xunit;

    public class SendgridEmailClientTests
    {
        [Fact]
        public void TestClientOptionsConstruction()
        {
            var options = new SendGridClientOptions();
            Assert.Equal("https://api.sendgrid.com", options.Host);
        }
        [Fact]
        public void TestClientOptionsSetDataResidency()
        {
            var options = new SendGridClientOptions();
            options.SetDataResidency("eu");
            Assert.Equal("https://api.eu.sendgrid.com/", options.Host);
        }
        [Fact]
        public void TestClientOptionsSetDataResidencyEU()
        {
            var options = new SendGridClientOptions();
            options.SetDataResidency("eu");
            Assert.Equal("https://api.eu.sendgrid.com/", options.Host);
        }

        [Fact]
        public void TestClientOptionsSetViaSendgridClient()
        {
            var options = new SendGridClientOptions();
            options.SetDataResidency("eu");
            var sg = new SendGridClient(options);
            Assert.Equal("https://api.eu.sendgrid.com/", options.Host);
        }

        [Fact]
        public void TestErrorClientOptions()
        {
            var options = new SendGridClientOptions();
            Assert.Throws<ArgumentNullException>(() => options.SetDataResidency(""));
            Assert.Throws<ArgumentNullException>(() => options.SetDataResidency(null));
        }
    }
}
