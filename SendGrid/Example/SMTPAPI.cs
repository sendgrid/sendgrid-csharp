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
        private IEnumerable<String> _to;

        public SMTPAPI(String username, String password, String from, IEnumerable<String> recipients)
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
            var message = SendGrid.GetInstance();

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
            message.Subject = "Hello World HTML Test";

            //create an instance of the SMTP transport mechanism
            var transportInstance = SMTP.GetInstance(new NetworkCredential(_username, _password));

            //send the mail
            transportInstance.Deliver(message);
        }

        /// <summary>
        /// Send a simple Plain Text email
        /// </summary>
        public void SimplePlaintextEmail()
        {
            //create a new message object
            var message = SendGrid.GetInstance();

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
            message.Subject = "Hello World Plain Text Test";

            //create an instance of the SMTP transport mechanism
            var transportInstance = SMTP.GetInstance(new NetworkCredential(_username, _password));

            //send the mail
            transportInstance.Deliver(message);
        }

        /// <summary>
        /// Enable The Gravatar Filter. 
        /// Currently the filter generates a 1x1 pixel gravatar image.
        /// http://docs.sendgrid.com/documentation/apps/gravatar/
        /// </summary>
        public void EnableGravatarEmail()
        {
            //create a new message object
            var message = SendGrid.GetInstance();

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
            var transportInstance = SMTP.GetInstance(new NetworkCredential(_username, _password));
            
            //enable gravatar
            message.EnableGravatar();

            //send the mail
            transportInstance.Deliver(message);
        }

        /// <summary>
        /// Enable the Open Tracking to track when emails are opened.
        /// http://docs.sendgrid.com/documentation/apps/open-tracking/
        /// </summary>
        public void EnableOpenTrackingEmail()
        {
            //create a new message object
            var message = SendGrid.GetInstance();

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
            var transportInstance = SMTP.GetInstance(new NetworkCredential(_username, _password));

            //enable gravatar
            message.EnableOpenTracking();

            //send the mail
            transportInstance.Deliver(message);
        }

        /// <summary>
        /// Point the urls to Sendgrid Servers so that the clicks can be logged before 
        /// being directed to the appropriate link
        /// http://docs.sendgrid.com/documentation/apps/click-tracking/
        /// </summary>
        public void EnableClickTrackingEmail()
        {
            //create a new message object
            var message = SendGrid.GetInstance();

            //set the message recipients
            foreach (string recipient in _to)
            {
                message.AddTo(recipient);
            }

            //set the sender
            message.From = new MailAddress(_from);

            //set the message body
            var timestamp = DateTime.Now.ToString("HH:mm:ss tt");
            message.Html = "<p style='color:red';>Hello World HTML </p> <a href='http://microsoft.com'>Checkout Microsoft!!</a>";
            message.Html += "<p>Sent At : " + timestamp + "</p>";

            message.Text = "hello world http://microsoft.com";

            //set the message subject
            message.Subject = "Hello World Click Tracking Test";

            //create an instance of the SMTP transport mechanism
            var transportInstance = SMTP.GetInstance(new NetworkCredential(_username, _password));

            //enable clicktracking
            message.EnableClickTracking(false);

            //send the mail
            transportInstance.Deliver(message);
        }

        /// <summary>
        /// The Spam Checker filter, is useful when your web application allows your end users 
        /// to create content that is then emailed through your SendGrid account. 
        /// http://docs.sendgrid.com/documentation/apps/spam-checker-filter/
        /// </summary>
        public void EnableSpamCheckEmail()
        {
            //create a new message object
            var message = SendGrid.GetInstance();

            //set the message recipients
            foreach (string recipient in _to)
            {
                message.AddTo(recipient);
            }

            //set the sender
            message.From = new MailAddress(_from);

            //set the message body
            var timestamp = DateTime.Now.ToString("HH:mm:ss tt");
            message.Html = "<p style='color:red';>VIAGRA!!!!!! Viagra!!! CHECKOUT THIS VIAGRA!!!! MALE ENHANCEMENT!!! </p>";
            message.Html += "<p>Sent At : " + timestamp + "</p>";

            //set the message subject
            message.Subject = "WIN A MILLION DOLLARS TODAY! WORK FROM HOME! A NIGERIAN PRINCE WANTS YOU!";

            //create an instance of the SMTP transport mechanism
            var transportInstance = SMTP.GetInstance(new NetworkCredential(_username, _password));

            //enable spamcheck
            message.EnableSpamCheck();

            //send the mail
            transportInstance.Deliver(message);
        }

        /// <summary>
        /// Add automatic unsubscribe links to the bottom of emails.
        /// http://docs.sendgrid.com/documentation/apps/subscription-tracking/
        /// </summary>
        public void EnableUnsubscribeEmail()
        {
            //create a new message object
            var message = SendGrid.GetInstance();

            //set the message recipients
            foreach (string recipient in _to)
            {
                message.AddTo(recipient);
            }

            //set the sender
            message.From = new MailAddress(_from);

            //set the message body
            var timestamp = DateTime.Now.ToString("HH:mm:ss tt");
            message.Html = "This is the HTML body";

            message.Text = "This is the plain text body";

            //set the message subject
            message.Subject = "Hello World Unsubscribe Test";

            //create an instance of the SMTP transport mechanism
            var transportInstance = SMTP.GetInstance(new NetworkCredential(_username, _password));

            //enable spamcheck
            //or optionally, you can specify 'replace' instead of the text and html in order to 
            //place the link wherever you want.
            message.EnableUnsubscribe("Please click the following link to unsubscribe: <% %>", "Please click <% here %> to unsubscribe");

            //send the mail
            transportInstance.Deliver(message);
        }

        /// <summary>
        /// The Footer App will insert a custom footer at the bottom of the text and HTML bodies.
        /// http://docs.sendgrid.com/documentation/apps/footer/
        /// </summary>
        public void EnableFooterEmail()
        {
            //create a new message object
            var message = SendGrid.GetInstance();

            //set the message recipients
            foreach (string recipient in _to)
            {
                message.AddTo(recipient);
            }

            //set the sender
            message.From = new MailAddress(_from);

            //set the message body
            var timestamp = DateTime.Now.ToString("HH:mm:ss tt");
            message.Html = "<p style='color:red';>Hello World</p>";
            message.Html += "<p>Sent At : " + timestamp + "</p>";

            message.Text = "Hello World plain text";

            //set the message subject
            message.Subject = "Hello World Footer Test";

            //create an instance of the SMTP transport mechanism
            var transportInstance = SMTP.GetInstance(new NetworkCredential(_username, _password));

            //Enable Footer
            message.EnableFooter("PLAIN TEXT FOOTER", "<p color='blue'>HTML FOOTER TEXT</p>");

            //send the mail
            transportInstance.Deliver(message);
        }

        /// <summary>
        /// The Footer App will insert a custom footer at the bottom of the text and HTML bodies.
        /// http://docs.sendgrid.com/documentation/apps/google-analytics/
        /// </summary>
        public void EnableGoogleAnalytics()
        {
            //create a new message object
            var message = SendGrid.GetInstance();

            //set the message recipients
            foreach (string recipient in _to)
            {
                message.AddTo(recipient);
            }

            //set the sender
            message.From = new MailAddress(_from);

            //set the message body
            var timestamp = DateTime.Now.ToString("HH:mm:ss tt");
            message.Html = "<p style='color:red';>Hello World</p>";
            message.Html += "<p>Sent At : " + timestamp + "</p>";
            message.Html += "Checkout my page at <a href=\"http://microsoft.com\">Microsoft</a>";

            message.Text = "Hello World plain text";

            //set the message subject
            message.Subject = "Hello World Footer Test";

            //create an instance of the SMTP transport mechanism
            var transportInstance = SMTP.GetInstance(new NetworkCredential(_username, _password));

            //enable Google Analytics
            message.EnableGoogleAnalytics("SendGridTest", "EMAIL", "Sendgrid", "ad-one", "My SG Campaign");

            //send the mail
            transportInstance.Deliver(message);
        }

        /// <summary>
        /// This feature wraps an HTML template around your email content. 
        /// This can be useful for sending out newsletters and/or other HTML formatted messages.
        /// http://docs.sendgrid.com/documentation/apps/email-templates/
        /// </summary>
        public void EnableTemplateEmail()
        {
            //create a new message object
            var message = SendGrid.GetInstance();

            //set the message recipients
            foreach (string recipient in _to)
            {
                message.AddTo(recipient);
            }

            //set the sender
            message.From = new MailAddress(_from);

            //set the message body
            var timestamp = DateTime.Now.ToString("HH:mm:ss tt");
            message.Html = "<p style='color:red';>Hello World</p>";
            message.Html += "<p>Sent At : " + timestamp + "</p>";

            message.Text = "Hello World plain text";

            //set the message subject
            message.Subject = "Hello World Template Test";

            //create an instance of the SMTP transport mechanism
            var transportInstance = SMTP.GetInstance(new NetworkCredential(_username, _password));

            //enable template
            message.EnableTemplate("<p>My Email Template <% body %> is awesome!</p>");

            //send the mail
            transportInstance.Deliver(message);
        }

        /// <summary>
        /// This feature wraps an HTML template around your email content. 
        /// This can be useful for sending out newsletters and/or other HTML formatted messages.
        /// hhttp://docs.sendgrid.com/documentation/apps/email-templates/
        /// </summary>
        public void EnableBypassListManagementEmail()
        {
            //create a new message object
            var message = SendGrid.GetInstance();

            //set the message recipients
            foreach (string recipient in _to)
            {
                message.AddTo(recipient);
            }

            //set the sender
            message.From = new MailAddress(_from);

            //set the message body
            var timestamp = DateTime.Now.ToString("HH:mm:ss tt");
            message.Html = "<p style='color:red';>Hello World</p>";
            message.Html += "<p>Sent At : " + timestamp + "</p>";

            message.Text = "Hello World plain text";

            //set the message subject
            message.Subject = "Hello World Bypass List Management Test";

            //create an instance of the SMTP transport mechanism
            var transportInstance = SMTP.GetInstance(new NetworkCredential(_username, _password));

            //enable bypass list management
            message.EnableBypassListManagement();

            //send the mail
            transportInstance.Deliver(message);
        }

    }
}
