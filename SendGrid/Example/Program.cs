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
        // this code is used for the SMTPAPI examples
        static void Main(string[] args)
        {
            var username = "brandonmwest";
            var password = "!!s3ndgr1d";
            var from = "brandon.west@sendgrid.com";
            var to = new List<String>
                         {
                             "@gmail.com",
                         };

            var restapi = new WEBAPI(username, password, from, to);

            //use this section to test out our Web and SMTP examples!
            restapi.SimpleHTMLEmail();

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

    }
}
