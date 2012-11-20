using System;
using System.Collections.Generic;
using System.IO;
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
        private static String userName = "sgrid_username";
        private static String password = "sgrid_password";

        static void Main(string[] args)
        {
            Console.WriteLine("This is the SendGrid examples test console.");
            RunGetCredentials();

            while (true)
            {
                try
                {
                    Console.Write("Pick a command to test (? for help):");
                    String cmd = Console.ReadLine().ToLower().Trim();
                    //TODO: ? make this a command pattern to eliminate code smell of these else-ifs.  Separate class for each command, name property, ask each command if it recognizes the cmd text
                    if (cmd == "?")
                        RunHelp();
                    else if (cmd == "bounces")
                        RunBounces();
                    else if (cmd == "cred")
                        RunGetCredentials();
                    else if (cmd == "send")
                        RunSendEmail();
                    else if (cmd == "exit")
                        break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("");
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("");
                }
            }
        }
        static void RunGetCredentials()
        {
            Console.WriteLine("What are your SendGrid username to use for this session?");
            userName = PromptString("User Name:", userName);
            password = PromptString("Password:", password);
            Console.WriteLine("");
        }
        static void RunHelp()
        {
            Console.WriteLine("Command List");
            Console.WriteLine("? - this help screen");
            Console.WriteLine("bounces - list or delete bounces");
            Console.WriteLine("cred - enter a new set of credentials to use");
            Console.WriteLine("send - sends an email");
            Console.WriteLine("");
            Console.WriteLine("exit - ends the program");
            Console.WriteLine("");
        }
        static void RunSendEmail()
        {
            Console.WriteLine("Send an email");
            var from = PromptString("From:", null);
            var to = PromptDelimitedList("To (; delimited):", ";");
            var cc = PromptDelimitedList("CC (; delimited):", ";");
            var bcc = PromptDelimitedList("BCC (; delimited):", ";");
            var subject = PromptString("Subject ('Test email' default):", "Test email");
            var htmlBody = PromptString("Html Body (none for default test, space for none):", "<p>Hello World HTML Test</p>");
            var textBody = PromptString("Text Body (none for default test, space for none):", "Hello World Text Test");

            var method = PromptString("Transport SMTP or Web? ('Web' default):", "Web");
            var gravatar = PromptBoolean("Enable gravatar (n)?", false);
            var openTrack = PromptBoolean("Enable open tracking (n)?", false);
            var clickTrack = PromptBoolean("Enable click tracking (n)?", false);
            var spamCheck = PromptBoolean("Enable spam checking (n)?", false);
            var spamMaxScore = 0;
            if (spamCheck)
                spamMaxScore = PromptInt32("Max spam score (5)?", 5);

            ITransport transport = null;
            if (method.ToLower().StartsWith("w"))
                transport = Web.GetInstance(new NetworkCredential(userName, password));
            else
                transport = SMTP.GetInstance(new NetworkCredential(userName, password));

            var message = SendGrid.GetInstance();
            message.From = new MailAddress(from);
            if (to != null)
                message.AddTo(to);
            if (cc != null)
                message.AddCc(cc);
            if (bcc != null)
                message.AddBcc(bcc);
            message.Subject = subject;
            if (!String.IsNullOrWhiteSpace(htmlBody))
                message.Html = htmlBody;
            if (!String.IsNullOrWhiteSpace(textBody))
                message.Text = textBody;

            if (gravatar)
                message.EnableGravatar();
            if (openTrack)
                message.EnableOpenTracking();
            if (clickTrack)
                message.EnableClickTracking();
            if (spamCheck)
                message.EnableSpamCheck(spamMaxScore);

            transport.Deliver(message);
            Console.WriteLine("Sent without error.");
        }

        static void RunBounces()
        {
            while (true)
            {
                Console.WriteLine("Bounces: enter 'l' for list, or 'd' for delete, or 'exit'");
                var cmd = Console.ReadLine();
                if (cmd == "l")
                    RunBouncesList();
                else if (cmd == "d")
                    RunBouncesDelete();
                else if (cmd == "exit")
                    break;
            }
        }
        static void RunBouncesList()
        {
            Console.WriteLine("Bounce List");
            var includeDate = PromptBoolean("include date (y)?", true);
            var days = PromptInt32("(optional) Number of days back from today?", 0);
            DateTime? startDate = null;
            DateTime? endDate = null;
            if (days <= 0)
            {
                startDate = PromptDate("(optional) Starting date:", null);
                endDate = PromptDate("(optional) Ending date:", null);
            }
            var limit = PromptInt32("(optional) Max records to return:", 0);
            var offset = 0;
            if (limit > 0)
                offset = PromptInt32("(optional) starting record:", 0);
            var type = PromptInt32("(optional) Type (0=All, 1=Hard, 2=Soft):", 0);
            var email = PromptString("(optional) Email filter:", null);

            SendGridMail.WebApi.WebBounce api = SendGridMail.WebApi.WebBounce.GetInstance(new NetworkCredential(userName, password));
            var items = api.GetBounces(includeDate, days, startDate, endDate, limit, offset, (BounceType)type, email);
            Console.WriteLine();
            if (items.Count == 0)
                Console.WriteLine("No items found.");
            else
            {
                foreach (var item in items)
                {
                    Console.WriteLine(String.Format("{0};  {1};  {2}", item.Created, item.Status, item.Email));
                    Console.WriteLine(item.Reason);
                    Console.WriteLine();
                }
                Console.WriteLine(String.Format("{0} items found.", items.Count));
            }
            Console.WriteLine();
        }
        static void RunBouncesDelete()
        {
            Console.WriteLine("Bounce Delete");
            DateTime? startDate = PromptDate("(optional) Starting date:", null);
            DateTime? endDate = PromptDate("(optional) Ending date:", null);
            var type = PromptInt32("(optional) Type (0=All, 1=Hard, 2=Soft):", 0);
            var email = PromptString("(optional) Email filter:", null);

            SendGridMail.WebApi.WebBounce api = SendGridMail.WebApi.WebBounce.GetInstance(new NetworkCredential(userName, password));
            api.DeleteBounces(startDate, endDate, (BounceType)type, email);
            Console.WriteLine("Done, no errors.");
            Console.WriteLine();
        }


        private static Int32 PromptInt32(string prompt, Int32 defaultValue)
        {
            Console.Write(prompt);
            String sVal = Console.ReadLine();
            Int32 val;
            if (int.TryParse(sVal, out val))
                return val;

            return defaultValue;
        }
        private static Boolean PromptBoolean(string prompt, Boolean defaultValue)
        {
            Console.Write(prompt);
            String sVal = Console.ReadLine();
            Boolean val;
            if (sVal.ToLower().StartsWith("y"))
                return true;
            else if (sVal.ToLower().StartsWith("n"))
                return false;
            else if (Boolean.TryParse(sVal, out val))
                return val;

            return defaultValue;
        }
        private static String PromptString(string prompt, String defaultValue)
        {
            Console.Write(prompt);
            String sVal = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(sVal))
                return defaultValue;
            return sVal;
        }
        private static DateTime? PromptDate(string prompt, DateTime? defaultValue)
        {
            Console.Write(prompt);
            String sVal = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(sVal))
                return defaultValue;
            
            DateTime parsed;
            if (DateTime.TryParse(sVal, out parsed))
                return parsed;
            return defaultValue;
        }
        private static List<String> PromptDelimitedList(string prompt, string delimiter)
        {
            return PromptDelimitedList(prompt, new string[] { delimiter });
        }
        private static List<String> PromptDelimitedList(string prompt, string[] delimiter)
        {
            List<String> vals = new List<string>();
            Console.Write(prompt);
            String sVal = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(sVal))
                return vals;

            var list = sVal.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            vals.AddRange(list);
            return vals;
        }
    }
}
