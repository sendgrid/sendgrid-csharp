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
            // Test sending email 
            var to = "example@example.com";
            var from = "example@example.com";
            var fromName = "Jane Doe";
            SendEmail(to, from, fromName);
            // Test viewing, creating, modifying and deleting API keys through our v3 Web API 
            ApiKeys();
            UnsubscribeGroups();
            Suppressions();
            GlobalSuppressions();
            GlobalStats();
            Templates();
            Versions();
            Batches();
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
                Console.WriteLine("\n\nPress any key to continue.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("\n\nPress any key to continue.");
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
            Console.WriteLine("These are your current API Keys.\n\nPress any key to continue.");
            Console.ReadKey();

            // POST API KEYS
            HttpResponseMessage responsePost = client.ApiKeys.Post("CSharpTestKey").Result;
            var rawString = responsePost.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            var apiKeyId = jsonObject.api_key_id.ToString();
            Console.WriteLine(responsePost.StatusCode);
            Console.WriteLine(responsePost.Content.ReadAsStringAsync().Result);
            Console.WriteLine("API Key created.\n\nPress any key to continue.");
            Console.ReadKey();

            // PATCH API KEYS
            HttpResponseMessage responsePatch = client.ApiKeys.Patch(apiKeyId, "CSharpTestKeyPatched").Result;
            Console.WriteLine(responsePatch.StatusCode);
            Console.WriteLine(responsePatch.Content.ReadAsStringAsync().Result);
            Console.WriteLine("API Key patched.\n\nPress any key to continue.");
            Console.ReadKey();

            // DELETE API KEYS
            Console.WriteLine("Deleting API Key, please wait.");
            HttpResponseMessage responseDelete = client.ApiKeys.Delete(apiKeyId).Result;
            Console.WriteLine(responseDelete.StatusCode);
            HttpResponseMessage responseFinal = client.ApiKeys.Get().Result;
            Console.WriteLine(responseFinal.StatusCode);
            Console.WriteLine(responseFinal.Content.ReadAsStringAsync().Result);
            Console.WriteLine("API Key Deleted.\n\nPress any key to end.");
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
            Console.WriteLine("This is an Unsubscribe Group with ID: " + unsubscribeGroupID.ToString() + ".\n\nPress any key to continue.");
            Console.ReadKey();

            // POST UNSUBSCRIBE GROUP
            HttpResponseMessage responsePost = client.UnsubscribeGroups.Post("C Sharp Unsubscribes", "Testing the C Sharp Library", false).Result;
            var rawString = responsePost.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            var unsubscribeGroupId = jsonObject.id.ToString();
            Console.WriteLine(responsePost.StatusCode);
            Console.WriteLine(responsePost.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Unsubscribe Group created.\n\nPress any key to continue.");
            Console.ReadKey();

            // DELETE UNSUBSCRIBE GROUP
            Console.WriteLine("Deleting Unsubscribe Group, please wait.");
            HttpResponseMessage responseDelete = client.UnsubscribeGroups.Delete(unsubscribeGroupId).Result;
            Console.WriteLine(responseDelete.StatusCode);
            HttpResponseMessage responseFinal = client.UnsubscribeGroups.Get().Result;
            Console.WriteLine(responseFinal.StatusCode);
            Console.WriteLine(responseFinal.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Unsubscribe Group Deleted.\n\nPress any key to end.");
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
            Console.WriteLine("Emails added to Suppression Group:" + groupID.ToString() + ".\n\nPress any key to continue.");
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
            Console.WriteLine("Emails removed from Suppression Group" + groupID.ToString() + "Deleted.\n\nPress any key to end.");
            Console.ReadKey();
        }

        private static void GlobalSuppressions()
        {
            String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            var client = new SendGrid.Client(apiKey);

            // CHECK IF EMAIL IS ON THE GLOBAL SUPPRESSION LIST
            var email = "elmer.thomas+test_global@gmail.com";
            HttpResponseMessage responseGetUnique = client.GlobalSuppressions.Get(email).Result;
            Console.WriteLine(responseGetUnique.StatusCode);
            Console.WriteLine(responseGetUnique.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Determines if the given email is listed on the Global Suppressions list. Press any key to continue.");
            Console.ReadKey();

            // ADD EMAILS TO THE GLOBAL SUPPRESSION LIST
            string[] emails = { "example@example.com", "example2@example.com" };
            HttpResponseMessage responsePost = client.GlobalSuppressions.Post(emails).Result;
            var rawString = responsePost.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            Console.WriteLine(responsePost.StatusCode);
            Console.WriteLine(responsePost.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Emails added to Global Suppression Group.\n\nPress any key to continue.");
            Console.ReadKey();

            // DELETE EMAILS FROM THE GLOBAL SUPPRESSION GROUP
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
            Console.WriteLine("Emails removed from Global Suppression Group.\n\nPress any key to end.");
            Console.ReadKey();
        }

        private static void GlobalStats()
        {
            String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            var client = new SendGrid.Client(apiKey);

            // Global Stats provide all of your user’s email statistics for a given date range.
            var startDate = "2015-11-01";
            HttpResponseMessage response = client.GlobalStats.Get(startDate).Result;
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Display global email stats, with start date " + startDate + "and no end date.\n\nPress any key to continue.");
            Console.ReadKey();

            var endDate = "2015-12-01";
            response = client.GlobalStats.Get(startDate, endDate).Result;
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Display global email stats, with start date " + startDate + "and end date " + endDate + ".\n\nPress any key to continue.");
            Console.ReadKey();

            var aggregatedBy = "day";
            response = client.GlobalStats.Get(startDate, endDate, aggregatedBy).Result;
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Display global email stats, with start date " + startDate + "and end date " + endDate + " and aggregated by " + aggregatedBy + ".\n\nPress any key to continue.");
            Console.ReadKey();

            aggregatedBy = "week";
            response = client.GlobalStats.Get(startDate, endDate, aggregatedBy).Result;
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Display global email stats, with start date " + startDate + "and end date " + endDate + " and aggregated by " + aggregatedBy + ".\n\nPress any key to continue.");
            Console.ReadKey();

            aggregatedBy = "month";
            response = client.GlobalStats.Get(startDate, endDate, aggregatedBy).Result;
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Display global email stats, with start date " + startDate + "and end date " + endDate + " and aggregated by " + aggregatedBy + ".\n\nPress any key to continue.");
            Console.ReadKey();
        }

        private static void Templates()
        {
            String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            var client = new SendGrid.Client(apiKey);

            // GET TEMPLATES
            HttpResponseMessage responseGet = client.Templates.Get().Result;
            Console.WriteLine(responseGet.StatusCode);
            Console.WriteLine(responseGet.Content.ReadAsStringAsync().Result);
            Console.WriteLine("These are your current Templates. Press any key to continue.");
            Console.ReadKey();

            // GET A PARTICULAR TEMPLATE
            string templateID = "";
            HttpResponseMessage responseGetUnique = client.Templates.Get(templateID).Result;
            Console.WriteLine(responseGetUnique.StatusCode);
            Console.WriteLine(responseGetUnique.Content.ReadAsStringAsync().Result);
            Console.WriteLine("This is a Template with ID: " + templateID + ".\n\nPress any key to continue.");
            Console.ReadKey();

            // POST TEMPLATE
            HttpResponseMessage responsePost = client.Templates.Post("C Sharp Templates").Result;
            var rawString = responsePost.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            var templateId = jsonObject.id.ToString();
            Console.WriteLine(responsePost.StatusCode);
            Console.WriteLine(responsePost.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Template created.\n\nPress any key to continue.");
            Console.ReadKey();

            // PATCH TEMPLATE
            HttpResponseMessage responsePatch = client.Templates.Patch(templateId, "CSharpTestTemplatePatched").Result;
            Console.WriteLine(responsePatch.StatusCode);
            Console.WriteLine(responsePatch.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Template patched.\n\nPress any key to continue.");
            Console.ReadKey();

            // DELETE TEMPLATE
            Console.WriteLine("Deleting Template, please wait.");
            HttpResponseMessage responseDelete = client.Templates.Delete(templateId).Result;
            Console.WriteLine(responseDelete.StatusCode);
            HttpResponseMessage responseFinal = client.Templates.Get().Result;
            Console.WriteLine(responseFinal.StatusCode);
            Console.WriteLine(responseFinal.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Template Deleted.\n\nPress any key to end.");
            Console.ReadKey();
        }

        private static void Versions()
        {
            String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            var client = new SendGrid.Client(apiKey);

            // GET A PARTICULAR VERSION
            string templateID = "";
            string versionId = "";
            HttpResponseMessage responseGetUnique = client.Versions.Get(templateID, versionId).Result;
            Console.WriteLine(responseGetUnique.StatusCode);
            Console.WriteLine(responseGetUnique.Content.ReadAsStringAsync().Result);
            Console.WriteLine("This is a Template Version with ID: " + templateID + "/" + versionId + ".\n\nPress any key to continue.");
            Console.ReadKey();

            // POST VERSION
            HttpResponseMessage responsePost = client.Versions.Post(templateID, "C Sharp Template", "C Sharp <%subject%>", "C Sharp Html <%body%>", "C Sharp Plain <%body%>", true).Result;
            var rawString = responsePost.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            var templateId = jsonObject.id.ToString();
            Console.WriteLine(responsePost.StatusCode);
            Console.WriteLine(responsePost.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Template Version created.\n\nPress any key to continue.");
            Console.ReadKey();

            // PATCH VERSION
            HttpResponseMessage responsePatch = client.Versions.Patch(templateId, versionId, "C Sharp Template Patched", "C Sharp <%subject%> Patched", "C Sharp Html <%body%> Patched", "C Sharp Plain <%body%> Patched", true).Result;
            Console.WriteLine(responsePatch.StatusCode);
            Console.WriteLine(responsePatch.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Template Version patched.\n\nPress any key to continue.");
            Console.ReadKey();

            // DELETE VERSION
            Console.WriteLine("Deleting Template Version, please wait.");
            HttpResponseMessage responseDelete = client.Versions.Delete(templateId, versionId).Result;
            Console.WriteLine(responseDelete.StatusCode);
            HttpResponseMessage responseFinal = client.Templates.Get().Result;
            Console.WriteLine(responseFinal.StatusCode);
            Console.WriteLine(responseFinal.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Template Version Deleted.\n\nPress any key to end.");
            Console.ReadKey();
        }

        private static void Batches()
        {
            String apiKey = Environment.GetEnvironmentVariable( "SENDGRID_APIKEY", EnvironmentVariableTarget.User );
            var client = new SendGrid.Client( apiKey );

            // GET A PARTICULAR BATCH
            string batchID = "";
            HttpResponseMessage responseGetUnique = client.Batches.Get( batchID ).Result;
            Console.WriteLine( responseGetUnique.StatusCode );
            Console.WriteLine( responseGetUnique.Content.ReadAsStringAsync().Result );
            Console.WriteLine( "This is a Batch with ID: " + batchID + ".\n\nPress any key to continue." );
            Console.ReadKey();

            // POST BATCH
            HttpResponseMessage responsePost = client.Batches.Post().Result;
            var rawString = responsePost.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse( rawString );
            var batchId = jsonObject.id.ToString();
            Console.WriteLine( responsePost.StatusCode );
            Console.WriteLine( responsePost.Content.ReadAsStringAsync().Result );
            Console.WriteLine( "Batch created.\n\nPress any key to continue." );
            Console.ReadKey();
        }
    }
}
