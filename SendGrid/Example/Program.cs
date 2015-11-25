using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using Newtonsoft.Json.Linq;

namespace Example
{
    internal class Program
    {
        private static void Main()
        {
            // Test sending email 
            var to = "example@example.com";
            var from = "example@example.com";
            var fromName = "Jane Doe";
            SendEmail(to, from, fromName);
            // Test viewing, creating, modifying and deleting API keys through our v3 Web API 
            ApiKeys();
        }

        private static void SendAsync(SendGrid.SendGridMessage message)
        {
            string apikey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
            // Create a Web transport for sending email.
            var transportWeb = new SendGrid.Web(apikey);

            // Send the email.
            try
            {
                transportWeb.DeliverAsync(message).Wait();
                Console.WriteLine("Email sent to " + message.To.GetValue(0));
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }

        private static void SendEmail(string to, string from, string fromName)
        {
            // Create the email object first, then add the properties.
            var myMessage = new SendGrid.SendGridMessage();
            myMessage.AddTo(to);
            myMessage.From = new MailAddress(from, fromName);
            myMessage.Subject = "Testing the SendGrid Library";
            myMessage.Text = "Hello World! %tag%";

            var subs = new List<String> { "私は%type%ラーメンが大好き" };
            myMessage.AddSubstitution("%tag%", subs);
            myMessage.AddSection("%type%", "とんこつ");

            SendAsync(myMessage);
        }

        private static void ApiKeys()
        {
            String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            var client = new SendGrid.Client(apiKey);

            // GET API KEYS
            HttpResponseMessage responseGet = client.ApiKeys.Get().Result;
            Console.WriteLine(responseGet.StatusCode);
            Console.WriteLine(responseGet.Content.ReadAsStringAsync().Result);
            Console.WriteLine("These are your current API Keys. Press any key to continue.");
            Console.ReadKey();

            // POST API KEYS
            HttpResponseMessage responsePost = client.ApiKeys.Post("CSharpTestKey").Result;
            var rawString = responsePost.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            var apiKeyId = jsonObject.api_key_id.ToString();
            Console.WriteLine(responsePost.StatusCode);
            Console.WriteLine(responsePost.Content.ReadAsStringAsync().Result);
            Console.WriteLine("API Key created. Press any key to continue.");
            Console.ReadKey();

            // PATCH API KEYS
            HttpResponseMessage responsePatch = client.ApiKeys.Patch(apiKeyId, "CSharpTestKeyPatched").Result;
            Console.WriteLine(responsePatch.StatusCode);
            Console.WriteLine(responsePatch.Content.ReadAsStringAsync().Result);
            Console.WriteLine("API Key patched. Press any key to continue.");
            Console.ReadKey();

            // DELETE API KEYS
            Console.WriteLine("Deleting API Key, please wait.");
            HttpResponseMessage responseDelete = client.ApiKeys.Delete(apiKeyId).Result;
            Console.WriteLine(responseDelete.StatusCode);
            HttpResponseMessage responseFinal = client.ApiKeys.Get().Result;
            Console.WriteLine(responseFinal.StatusCode);
            Console.WriteLine(responseFinal.Content.ReadAsStringAsync().Result);
            Console.WriteLine("API Key Deleted, press any key to end");
            Console.ReadKey();
        }
    }
}
