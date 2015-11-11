using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using Newtonsoft.Json.Linq;
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
		    string _api_key_id;

            // GET
            HttpResponseMessage responseGet = client.ApiKeys.Get();
            Console.WriteLine(responseGet.StatusCode);
            Console.WriteLine(responseGet.Content.ReadAsStringAsync().Result);
            Console.WriteLine("These are your current API Keys. Press any key to continue.");
            Console.ReadKey();
     
            // POST
            HttpResponseMessage responsePost = client.ApiKeys.Post("CSharpTestKey");
            string rawString = responsePost.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            _api_key_id = jsonObject.api_key_id.ToString();
            Console.WriteLine(responsePost.StatusCode);
            Console.WriteLine(responsePost.Content.ReadAsStringAsync().Result);
            Console.WriteLine("API Key created. Press any key to continue.");
            Console.ReadKey();

            // PATCH
            HttpResponseMessage responsePatch = client.ApiKeys.Patch(_api_key_id, "CSharpTestKeyPatched");
            Console.WriteLine(responsePatch.StatusCode);
            Console.WriteLine(responsePatch.Content.ReadAsStringAsync().Result);
            Console.WriteLine("API Key patched. Press any key to continue.");
            Console.ReadKey();

            // DELETE
            Console.WriteLine("Deleting API Key, please wait.");
            client.ApiKeys.Delete(_api_key_id);
            HttpResponseMessage responseFinal = client.ApiKeys.Get();
            Console.WriteLine(responseFinal.StatusCode);
            Console.WriteLine(responseFinal.Content.ReadAsStringAsync().Result);
            Console.WriteLine("API Key Deleted, press any key to end");
            Console.ReadKey();

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