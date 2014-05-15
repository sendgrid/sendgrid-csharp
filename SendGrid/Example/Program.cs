using System;
using System.Net;
using System.Net.Mail;
using SendGridMail;

namespace Example
{
	internal class Program
	{
		// this code is used for the SMTPAPI examples
		private static void Main()
		{
			// Create the email object first, then add the properties.
			var myMessage = SendGrid.GetInstance();
			myMessage.AddTo("anna@example.com");
			myMessage.From = new MailAddress("john@example.com", "John Smith");
			myMessage.Subject = "Testing the SendGrid Library";
			myMessage.Text = "Hello World!";

			// Create credentials, specifying your user name and password.
			var credentials = new NetworkCredential("username", "password");

			// Create a Web transport for sending email.
			var transportWeb = Web.GetInstance(credentials);

			// Send the email.
			if (transportWeb != null)
				transportWeb.DeliverAsync(myMessage);

			Console.WriteLine("Done!");
			Console.ReadLine();
		}

		private static SendGrid UseMailBuilder() 
		{
			// Fluently create the mail builder, add the properties, then build the email object
			return MailBuilder.Create()
				.To("anna@example.com")
				.From("john@example.com", "John Smith")
				.Subject("Testing the SendGrid Library")
				.Text("Hello World!")
				.Build();
		}
	}
}