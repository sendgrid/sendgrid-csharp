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
        /*static void Main(String[] args)
        {
            var restInstance = REST.GetInstance(new NetworkCredential("cjbuchmann", "gadget15"));

            //create a new message object
            var message = SendGrid.GenerateInstance();

            message.AddTo("cj.buchmann@sendgrid.com");
            message.From = new MailAddress("cj.buchmann@sendgrid.com");
            message.Html = "<div>hello world</div>";
            message.Subject = "Hello World SUbject";
            message.AddAttachment(@"C:\Users\Public\Pictures\Sample Pictures\Koala.jpg");
            message.AddAttachment(@"C:\Users\Public\Pictures\Sample Pictures\Penguins.jpg");

            restInstance.Deliver(message);

            Console.WriteLine("Message Sent");
            Console.WriteLine("DONE!");
        }*/

        // this code is used for the SMTPAPI examples
        static void Main(string[] args)
        {
            var username = "cjbuchmann";
            var password = "gadget15";
            var from = "cj.buchmann@sendgrid.com";
            var to = new List<String>
                         {
                             "cj.buchmann@sendgrid.com"
                         };

            var bcc = new List<string>
                          {
                              "eric@sendgrid.com"
                          };            
            
            var cc = new List<string>
                          {
                              "eric@sendgrid.com"
                          };

            //initialize the SMTPAPI example class
            var smtpapi = new SMTPAPI(username, password, from, to);

            //send a simple HTML encoded email.
            //smtpapi.SimpleHTMLEmail();

            //send a simple Plaintext email.
            //smtpapi.SimplePlaintextEmail();

            //send a gravatar enabled email.
            //smtpapi.EnableGravatarEmail();

            //send an open tracking enabled email.
            //smtpapi.EnableOpenTrackingEmail();

            //send an open tracking enabled email.
            smtpapi.EnableClickTrackingEmail();

            Console.ReadLine();
        }
    }
}
