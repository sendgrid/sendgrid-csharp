using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using SendGridMail;
using SendGridMail.Transport;

namespace Example
{
    class SMTPAPI
    {
        private String _username;
        private String _password;
        private String _from;
        private IEnumerable<string> _to;

        public SMTPAPI(String username, String password, String from, IEnumerable<string> recipients)
        {
            _username = username;
            _password = password;
            _from = from;
            _to = recipients;
        }

        /// <summary>
        /// Send a simple HTML based email
        /// </summary>
        public void SimpleHTMLEmail()
        {
            //create a new message object
            var message = SendGrid.GenerateInstance();

            //set the message recipients
            foreach(string recipient in _to)
            {
                message.AddTo(recipient);
            }

            //set the sender
            message.From = new MailAddress(_from);

            //set the message body
            message.Html = "<html><p>Hello</p><p>World</p></html>";

            //set the message subject
            message.Subject = "Hello World Simple Test";

            //create an instance of the SMTP transport mechanism
            var smtpInstance = SMTP.GenerateInstance(new NetworkCredential(_username, _password));

            //send the mail
            smtpInstance.Deliver(message);
        }

        /// <summary>
        /// Send a simple Plain Text email
        /// </summary>
        public void SimplePlaintextEmail()
        {
            //create a new message object
            var message = SendGrid.GenerateInstance();

            //set the message recipients
            foreach(string recipient in _to)
            {
                message.AddTo(recipient);
            }

            //set the sender
            message.From = new MailAddress(_from);

            //set the message body
            message.Text = "Hello World Plain Text";

            //set the message subject
            message.Subject = "Hello World Simple Test";

            //create an instance of the SMTP transport mechanism
            var smtpInstance = SMTP.GenerateInstance(new NetworkCredential(_username, _password));

            //send the mail
            smtpInstance.Deliver(message);
        }

        /// <summary>
        /// Enable The Gravatar Filter. 
        /// Currently the filter a 1x1 pixel gravatar image.
        /// Find more info at http://docs.sendgrid.com/documentation/apps/gravatar/
        /// </summary>
        public void EnableGravatarEmail()
        {
            //create a new message object
            var message = SendGrid.GenerateInstance();

            //set the message recipients
            foreach (string recipient in _to)
            {
                message.AddTo(recipient);
            }

            //set the sender
            message.From = new MailAddress(_from);

            //set the message body
            message.Html = "<p style='color:red';>Hello World Gravatar Email</p>";

            //set the message subject
            message.Subject = "Hello World Gravatar Test";

            //create an instance of the SMTP transport mechanism
            var smtpInstance = SMTP.GenerateInstance(new NetworkCredential(_username, _password));
            
            //enable gravatar
            message.EnableGravatar();

            //send the mail
            smtpInstance.Deliver(message);
        }

        /// <summary>
        /// Enable the Open Tracking to track when emails are opened.
        /// http://docs.sendgrid.com/documentation/apps/open-tracking/
        /// </summary>
        public void EnableOpenTrackingEmail()
        {
            var header = new Header();
            //create a new message object
            var message = SendGrid.GenerateInstance();

            //set the message recipients
            foreach (string recipient in _to)
            {
                message.AddTo(recipient);
            }

            //set the sender
            message.From = new MailAddress(_from);

            //set the message body
            message.Html = "<p style='color:red';>Hello World Plain Text</p>";

            //set the message subject
            message.Subject = "Hello World Open Tracking Test";

            //create an instance of the SMTP transport mechanism
            var smtpInstance = SMTP.GenerateInstance(new NetworkCredential(_username, _password));

            //enable gravatar
            message.EnableOpenTracking();

            Console.WriteLine(header.AsJson());

            //send the mail
            smtpInstance.Deliver(message);

            Console.WriteLine("done");
        }

        /// <summary>
        /// Enable the Open Tracking to track when emails are opened.
        /// http://docs.sendgrid.com/documentation/apps/open-tracking/
        /// </summary>
        public void EnableClickTrackingEmail()
        {
            var header = new Header();
            //create a new message object
            var message = SendGrid.GenerateInstance();

            //set the message recipients
            foreach (string recipient in _to)
            {
                message.AddTo(recipient);
            }

            //set the sender
            message.From = new MailAddress(_from);

            //set the message body
            var timestamp = DateTime.Now.ToString("HH:mm:ss tt");
            message.Html = "<p style='color:red';>Hello World Plain Text</p> <a href = 'http://microsoft.com'>Checkout Microsoft!!</a>";
            message.Html += "<p>Sent At : " + timestamp + "</p>";

            //set the message subject
            message.Subject = "Hello World Click Tracking Test";

            //create an instance of the SMTP transport mechanism
            var smtpInstance = SMTP.GenerateInstance(new NetworkCredential(_username, _password));

            //enable clicktracking
            message.EnableClickTracking("1");

            Console.WriteLine(header.AsJson());

            //send the mail
            smtpInstance.Deliver(message);

            Console.WriteLine("done");
            Console.WriteLine("Sent at : "+timestamp);
        }

    }
}
