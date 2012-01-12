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
            var sendgrid = new SendGrid(header);

            Console.WriteLine("testing");

            var text = "My Text <%%>";
            var html = "<body><p>hello,</p></body>";
            var replace = "John";
            var url = "http://www.example.com";
            var landing = "this_landing";
            sendgrid.EnableUnsubscribe(text, html, replace, url, landing);

            Console.WriteLine(header.AsJson());

        }
    }
}
