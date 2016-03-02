using System;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Exceptions;
using NUnit.Framework;
using SendGrid;

namespace UnitTest
{
    [TestFixture]
    public class Attachment
    {
        static readonly string ApiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");

        [Test]
        public async Task Can_Have_International_Characters()
        {
            var myMessage = new SendGridMessage();
            myMessage.AddTo("attachment@example.com");
            myMessage.From = new MailAddress("from@example.com", "First Last");
            myMessage.Subject = "Sending with SendGrid is Fun With attachments";
            myMessage.Text = "and easy to do anywhere, even with attachments!";

            using (var fs = new MemoryStream())
            {
                using(var writer = new StreamWriter(fs,Encoding.UTF8, 4096, leaveOpen:true))
                {
                    writer.Write("Ærfugl");
                    writer.Flush();
                    fs.Position = 0;
                }

                myMessage.AddAttachment(fs, "MPGÖÉËæøåÆØÅ.txt");
                var transportWeb = new SendGrid.Web(ApiKey);

                try
                {
                    await transportWeb.DeliverAsync(myMessage);
                }
                catch (InvalidApiRequestException iare)
                {
                    Assert.Fail(string.Join("\n", iare.Errors));
                }
            }
        }
    }
}
