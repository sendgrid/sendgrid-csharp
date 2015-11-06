using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using SendGrid.Resources;

namespace Example
{
	internal class Program
	{
		// this code is used for the SMTPAPI examples
		private static void Main()
		{  
            String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            var client = new SendGrid.Client(apiKey);

            // GET
            HttpResponseMessage response = client.ApiKeys.Get();
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.ReadKey();

            // POST
            /*
            HttpResponseMessage response = client.ApiKeys.Post("CSharpTestKey5");
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.ReadKey();
            */

            // DELETE
            /*
            HttpResponseMessage response = client.ApiKeys.Delete("<api_key_id>");
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.ReadKey();
            */

            // PATCH
            /*
            HttpResponseMessage response = client.ApiKeys.Patch("<api_key_id>", "CSharpTestKey7");
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.ReadKey();
            */

            // SEND EMAIL
            /*
			// Create the email object first, then add the properties.
			var myMessage = new SendGrid.SendGridMessage();
			myMessage.AddTo("elmer.thomas@sendgrid.com");
			myMessage.From = new MailAddress("dx@sendgrid.com", "Elmer Thomas");
			myMessage.Subject = "Testing the SendGrid Library";
			myMessage.Text = "Hello World! %tag%";

			var subs = new List<String> { "私は%type%ラーメンが大好き" };
			myMessage.AddSubstitution("%tag%",subs);
			myMessage.AddSection("%type%", "とんこつ");

		    SendAsync(myMessage);
            */
        }

        private static async void SendAsync(SendGrid.SendGridMessage message)
	    {
            // Create credentials, specifying your user name and password.
            var credentials = new NetworkCredential("<sendgrid_username>", "<sendgrid_password>");

            // Create a Web transport for sending email.
            var transportWeb = new SendGrid.Web(credentials);

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