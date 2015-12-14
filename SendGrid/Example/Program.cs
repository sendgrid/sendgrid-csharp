using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mail;
using Newtonsoft.Json.Linq;

namespace Example
{
    internal class Program
    {
        private static void Main()
        {
            /*
            // Test sending email 
            var to = "example@example.com";
            var from = "example@example.com";
            var fromName = "Jane Doe";
            SendEmail(to, from, fromName);
            // Test viewing, creating, modifying and deleting API keys through our v3 Web API 
            ApiKeys();
            UnsubscribeGroups();
            Suppressions();
            */
            GlobalSuppressions();
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
            Console.WriteLine("API Key Deleted, press any key to end.");
            Console.ReadKey();
        }

        private static void UnsubscribeGroups()
        {
            String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            var client = new SendGrid.Client(apiKey);

            // GET UNSUBSCRIBE GROUPS
            HttpResponseMessage responseGet = client.UnsubscribeGroups.Get().Result;
            Console.WriteLine(responseGet.StatusCode);
            Console.WriteLine(responseGet.Content.ReadAsStringAsync().Result);
            Console.WriteLine("These are your current Unsubscribe Groups. Press any key to continue.");
            Console.ReadKey();

            // GET A PARTICULAR UNSUBSCRIBE GROUP
            int unsubscribeGroupID = 69;
            HttpResponseMessage responseGetUnique = client.UnsubscribeGroups.Get(unsubscribeGroupID).Result;
            Console.WriteLine(responseGetUnique.StatusCode);
            Console.WriteLine(responseGetUnique.Content.ReadAsStringAsync().Result);
            Console.WriteLine("This is an Unsubscribe Group with ID: " + unsubscribeGroupID.ToString() + ". Press any key to continue.");
            Console.ReadKey();

            // POST UNSUBSCRIBE GROUP
            HttpResponseMessage responsePost = client.UnsubscribeGroups.Post("C Sharp Unsubscribes", "Testing the C Sharp Library", false).Result;
            var rawString = responsePost.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            var unsubscribeGroupId = jsonObject.id.ToString();
            Console.WriteLine(responsePost.StatusCode);
            Console.WriteLine(responsePost.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Unsubscribe Group created. Press any key to continue.");
            Console.ReadKey();

            // DELETE UNSUBSCRIBE GROUP
            Console.WriteLine("Deleting Unsubscribe Group, please wait.");
            HttpResponseMessage responseDelete = client.UnsubscribeGroups.Delete(unsubscribeGroupId).Result;
            Console.WriteLine(responseDelete.StatusCode);
            HttpResponseMessage responseFinal = client.UnsubscribeGroups.Get().Result;
            Console.WriteLine(responseFinal.StatusCode);
            Console.WriteLine(responseFinal.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Unsubscribe Group Deleted, press any key to end.");
            Console.ReadKey();
        }
        
        private static void Suppressions()
        {
            String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            var client = new SendGrid.Client(apiKey);

            // GET SUPPRESSED ADDRESSES FOR A GIVEN GROUP
            int groupID = 69;
            HttpResponseMessage responseGetUnique = client.Suppressions.Get(groupID).Result;
            Console.WriteLine(responseGetUnique.StatusCode);
            Console.WriteLine(responseGetUnique.Content.ReadAsStringAsync().Result);
            Console.WriteLine("These are the suppressed emails with group ID: " + groupID.ToString() + ". Press any key to continue.");
            Console.ReadKey();

            // ADD EMAILS TO A SUPPRESSION GROUP
            string[] emails = { "example@example.com", "example2@example.com" };
            HttpResponseMessage responsePost = client.Suppressions.Post(groupID, emails).Result;
            var rawString = responsePost.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            Console.WriteLine(responsePost.StatusCode);
            Console.WriteLine(responsePost.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Emails added to Suppression Group:" + groupID.ToString() + ". Press any key to continue.");
            Console.ReadKey();

            // DELETE EMAILS FROM A SUPPRESSION GROUP
            Console.WriteLine("Deleting emails from Suppression Group, please wait.");
            HttpResponseMessage responseDelete1 = client.Suppressions.Delete(groupID, "example@example.com").Result;
            Console.WriteLine(responseDelete1.StatusCode);
            HttpResponseMessage responseDelete2 = client.Suppressions.Delete(groupID, "example2@example.com").Result;
            Console.WriteLine(responseDelete2.StatusCode);
            HttpResponseMessage responseFinal = client.Suppressions.Get(groupID).Result;
            Console.WriteLine(responseFinal.StatusCode);
            Console.WriteLine(responseFinal.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Emails removed from Suppression Group" + groupID.ToString() + "Deleted. Press any key to end.");
            Console.ReadKey();
        }

        private static void GlobalSuppressions()
        {
            String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            var client = new SendGrid.Client(apiKey);

            // GET SUPPRESSED ADDRESSES FOR A GIVEN GROUP
            var email = "elmer.thomas+test_global@gmail.com";
            HttpResponseMessage responseGetUnique = client.GlobalSuppressions.Get(email).Result;
            Console.WriteLine(responseGetUnique.StatusCode);
            Console.WriteLine(responseGetUnique.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Determines if the given email is listed on the Global Suppressions list. Press any key to continue.");
            Console.ReadKey();

            // ADD EMAILS TO A SUPPRESSION GROUP
            string[] emails = { "example@example.com", "example2@example.com" };
            HttpResponseMessage responsePost = client.GlobalSuppressions.Post(emails).Result;
            var rawString = responsePost.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            Console.WriteLine(responsePost.StatusCode);
            Console.WriteLine(responsePost.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Emails added to Global Suppression Group. Press any key to continue.");
            Console.ReadKey();

            // DELETE EMAILS FROM A SUPPRESSION GROUP
            Console.WriteLine("Deleting emails from Global Suppression Group, please wait.");
            HttpResponseMessage responseDelete1 = client.GlobalSuppressions.Delete("example@example.com").Result;
            Console.WriteLine(responseDelete1.StatusCode);
            HttpResponseMessage responseDelete2 = client.GlobalSuppressions.Delete("example2@example.com").Result;
            Console.WriteLine(responseDelete2.StatusCode);
            HttpResponseMessage responseFinal = client.GlobalSuppressions.Get("example@example.com").Result;
            Console.WriteLine(responseFinal.StatusCode);
            Console.WriteLine(responseFinal.Content.ReadAsStringAsync().Result);
            HttpResponseMessage responseFinal2 = client.GlobalSuppressions.Get("example2@example.com").Result;
            Console.WriteLine(responseFinal2.StatusCode);
            Console.WriteLine(responseFinal2.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Emails removed from Global Suppression Group. Press any key to end.");
            Console.ReadKey();
        }

    }
}
