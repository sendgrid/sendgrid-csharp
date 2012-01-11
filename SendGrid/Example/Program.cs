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
            var credentials = new NetworkCredential("cjbuchmann", "Gadget_15");
            var transport = SMTP.GenerateInstance(credentials);
            var header = new Header();
            var message = new SendGrid(header);
            message.AddTo("tyler.bischel@sendgrid.com");
            message.From = new MailAddress("eric@sendgrid.com");
            message.Text = "This is a test message.";
            message.Subject = "hazaah!";
            transport.Deliver(message);
        }
    }
}
