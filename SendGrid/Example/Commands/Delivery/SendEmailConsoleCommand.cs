using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using SendGridMail;
using SendGridMail.Transport;

namespace Example.Commands.Delivery
{
    public class SendEmailConsoleCommand : IConsoleCommand
    {
        public NetworkCredential Credential { get; set; }

        public string Name
        {
            get
            {
                return "sendEmail";
            }
        }

        public string HelpText
        {
            get
            {
                return "Sends a newly composed email";
            }
        }

        public void Run()
        {
            this.Execute();
        }

        public void Execute()
        {
            Console.WriteLine("Send an email");
            var from = ConsoleCommandParser.PromptString("From:", null);
            var to = ConsoleCommandParser.PromptDelimitedList("To (; delimited):", ";");
            var cc = ConsoleCommandParser.PromptDelimitedList("CC (; delimited):", ";");
            var bcc = ConsoleCommandParser.PromptDelimitedList("BCC (; delimited):", ";");
            var subject = ConsoleCommandParser.PromptString("Subject ('Test email' default):", "Test email");
            var htmlBody = ConsoleCommandParser.PromptString("Html Body (none for default test, space for none):", "<p>Hello World HTML Test</p>");
            var textBody = ConsoleCommandParser.PromptString("Text Body (none for default test, space for none):", "Hello World Text Test");

            var method = ConsoleCommandParser.PromptString("Transport SMTP or Web? ('Web' default):", "Web");
            var gravatar = ConsoleCommandParser.PromptBoolean("Enable gravatar (n)?", false);
            var openTrack = ConsoleCommandParser.PromptBoolean("Enable open tracking (n)?", false);
            var clickTrack = ConsoleCommandParser.PromptBoolean("Enable click tracking (n)?", false);
            var spamCheck = ConsoleCommandParser.PromptBoolean("Enable spam checking (n)?", false);
            var spamMaxScore = 0;
            if (spamCheck)
            {
                spamMaxScore = ConsoleCommandParser.PromptInt32("Max spam score (5)?", 5);
            }

            ITransport transport = null;
            if (method.ToLower().StartsWith("w"))
            {
                transport = Web.GetInstance(this.Credential);
            }
            else
            {
                transport = SMTP.GetInstance(this.Credential);
            }

            var message = SendGrid.GetInstance();
            message.From = new MailAddress(from);
            if (to != null)
            {
                message.AddTo(to);
            }
            if (cc != null)
            {
                message.AddCc(cc);
            }
            if (bcc != null)
            {
                message.AddBcc(bcc);
            }
            message.Subject = subject;
            if (!String.IsNullOrWhiteSpace(htmlBody))
            {
                message.Html = htmlBody;
            }
            if (!String.IsNullOrWhiteSpace(textBody))
            {
                message.Text = textBody;
            }

            if (gravatar)
            {
                message.EnableGravatar();
            }
            if (openTrack)
            {
                message.EnableOpenTracking();
            }
            if (clickTrack)
            {
                message.EnableClickTracking();
            }
            if (spamCheck)
            {
                message.EnableSpamCheck(spamMaxScore);
            }

            transport.Deliver(message);
            Console.WriteLine("Sent without error.");
        }
    }
}