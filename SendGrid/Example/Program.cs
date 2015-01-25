using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using SendGrid;

namespace Example
{
	internal class Program
	{
		// this code is used for the SMTPAPI examples
		private static void Main()
		{
			// Create the email object first, then add the properties.
			var myMessage = new SendGridMessage();
			myMessage.AddTo("anna@example.com");
			myMessage.From = new MailAddress("john@example.com", "John Smith");
			myMessage.Subject = "Testing the SendGrid Library";
			myMessage.Text = "Hello World! %tag%";

            var subs = new List<String> { "私はラーメンが大好き" };
            myMessage.AddSubstitution("%tag%",subs);

		    SendAsync(myMessage);

			Console.ReadLine();
		}

        private static async void SendAsync(SendGridMessage message)
	    {
            // Create credentials, specifying your user name and password.
            var credentials = new NetworkCredential("username", "password");

            // Create a Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            try
            {
                await transportWeb.DeliverAsync(message);
                Console.WriteLine("Sent!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
	    }
	}
}