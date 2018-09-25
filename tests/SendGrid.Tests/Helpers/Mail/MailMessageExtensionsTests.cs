using System.IO;
using System.Linq;
using System.Net.Mail;
using SendGrid.Helpers.Mail;
using Xunit;

namespace SendGrid.Tests.Helpers.Mail
{
    public class MailMessageExtensionsTests
    {
        [Theory]
        [InlineData("Example@example.com")]
        [InlineData("Example+data@example.com")]
        public void ConvertEmailAddresses(string email)
        {
            var address = new MailAddress(email).ToSendGridAddress();
            Assert.Equal(email, address.Email);
        }

        [Theory]
        [InlineData("Example@example.com", "Ben@example.com", "Widget Factory Production", "Ben, widget production is down 15%, could you investigate?")]
        public void TestSimpleMailMessageEmail(string from, string to, string subject, string body)
        {
            var mail = new MailMessage(from, to)
            {
                Subject = subject,

            };

            mail.Body = body;

            var message = mail.ToSendGridMessage();

            Assert.Equal(from, message.From.Email);
            Assert.Equal(subject, message.Subject);
            Assert.Contains(message.Personalizations.SelectMany(x => x.Tos), x => x.Email == to);
            Assert.Contains(message.Contents, x => x.Value == body);
        }

        [Theory]
        [InlineData("Example@example.com", "Ben@example.com", "Widget Factory Production", "Management@example.com", "HR@example.com")]
        public void TestMailMessageEmailWithCC(string from, string to, string subject, string cc, string bcc)
        {
            var mail = new MailMessage(from, to)
            {
                Subject = subject
            };

            mail.CC.Add(cc);
            mail.Bcc.Add(bcc);

            var message = mail.ToSendGridMessage();


            Assert.Equal(from, message.From.Email);
            Assert.Equal(subject, message.Subject);
            Assert.Contains(message.Personalizations.SelectMany(x => x.Tos), x => x.Email == to);
            Assert.Contains(message.Personalizations.SelectMany(x => x.Ccs), x => x.Email == cc);
            Assert.Contains(message.Personalizations.SelectMany(x => x.Bccs), x => x.Email == bcc);
        }

        [Theory]
        [InlineData("Example@example.com", "Ben@example.com", "invoicing@example.com")]
        public void TestSimpleMailMessageReplyTo(string from, string to, string replyTo)
        {
            var mail = new MailMessage(from, to);
            mail.ReplyToList.Add(replyTo);

            var message = mail.ToSendGridMessage();

            Assert.Equal(replyTo, message.ReplyTo.Email);
        }

        [Fact]
        public void TestMailMessageShouldConvertAttachements()
        {
            var mail = new MailMessage("Ben@Example.com", "HR@Example.com");
            var data = new byte[] { 0x1, 0x2, 0x3, 0x4 };

            SendGridMessage message = null;

            using (var stream = new MemoryStream(data))
            {
                mail.Attachments.Add(new System.Net.Mail.Attachment(stream, "Example.pdf"));
                message = mail.ToSendGridMessage();
            }

            Assert.NotEmpty(message.Attachments);
            var attachment = message.Attachments.Single();
            Assert.Equal("Example.pdf", attachment.Filename);
            Assert.False(string.IsNullOrEmpty(attachment.Content));
        }

        [Fact]
        public void TestMailMessageHeaderConverted()
        {
            var mail = new MailMessage("Ben@Example.com", "HR@Example.com");
            mail.Headers.Add("Example", "Example");

            var message = mail.ToSendGridMessage();
            var headers = message.Personalizations.SelectMany(x => x.Headers);
            Assert.Contains(headers, x => x.Key == "Example" && x.Value == "Example");
        }
    }
}
