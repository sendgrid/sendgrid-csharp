using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using SendGridMail;
using SendGridMail.Transport;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var header = new Header();
            var transport = SMTP.GenerateInstance(new NetworkCredential("sgrid_username", "sgrid_password"));

            var message = new SendGrid(header);
            message.AddTo("eric.becking@sendgrid.com");
            message.From = new MailAddress("eric@sendgrid.com");
            message.Text = "This is a test message.";
            message.Html = "<html><p>This is a <b>test</b> message.</p></html>";
            message.Subject = "hazaah!";
            transport.Deliver(message);
        }
    }
}
