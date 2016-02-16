﻿using Newtonsoft.Json.Linq;
using SendGrid.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Security;

namespace Example
{
    internal class Program
    {
        private static void Main()
        {
            // Do you want to proxy requests through fiddler?
            var useFiddler = false;

            if (useFiddler)
            {
                // This is necessary to ensure HTTPS traffic can be proxied through Fiddler without any certificate validation error.
                // This is fine for testing but not advisable in production
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
            }

            // Direct all traffic through fiddler running on the local machine
            var httpClient = new HttpClient(
                new HttpClientHandler()
                {
                    Proxy = new WebProxy("http://localhost:8888"),
                    UseProxy = useFiddler
                }
            );

            // Test sending email 
            var to = "example@example.com";
            var from = "example@example.com";
            var fromName = "Jane Doe";
            SendEmail(to, from, fromName);
            // Test viewing, creating, modifying and deleting API keys through our v3 Web API 
            ApiKeys(httpClient);
            UnsubscribeGroups(httpClient);
            GlobalSuppressions(httpClient);
            GlobalStats(httpClient);
            Lists(httpClient);
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

        private static void ApiKeys(HttpClient httpClient)
        {
            String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            var client = new SendGrid.Client(apiKey: apiKey, httpClient: httpClient);

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

        private static void UnsubscribeGroups(HttpClient httpClient)
        {
            Console.WriteLine("\n***** UNSUBSCRIBE GROUPS *****");

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            var client = new SendGrid.Client(apiKey: apiKey, httpClient: httpClient);

            // CREATE A NEW SUPPRESSION GROUP
            var newGroup = client.UnsubscribeGroups.CreateAsync("New group", "This is a new group for testing purposes", false).Result;
            Console.WriteLine("Unique ID of the new unsubscribe group: {0}", newGroup.Id);

            // UPDATE A SUPPRESSION GROUP
            var updatedGroup = client.UnsubscribeGroups.UpdateAsync(newGroup.Id, "This is the updated name").Result;
            Console.WriteLine("Unsubscribe group {0} updated", updatedGroup.Id);

            // GET UNSUBSCRIBE GROUPS
            var groups = client.UnsubscribeGroups.GetAllAsync().Result;
            Console.WriteLine("There are {0} unsubscribe groups", groups.Length);

            // GET A PARTICULAR UNSUBSCRIBE GROUP
            var group = client.UnsubscribeGroups.GetAsync(newGroup.Id).Result;
            Console.WriteLine("Retrieved unsubscribe group {0}: {1}", group.Id, group.Name);

            // ADD A FEW ADDRESSES TO UNSUBSCRIBE GROUP
            client.Suppressions.AddAddressToUnsubscribeGroupAsync(group.Id, "test1@example.com").Wait();
            Console.WriteLine("Added test1@example.com to unsubscribe group {0}", group.Id);
            client.Suppressions.AddAddressToUnsubscribeGroupAsync(group.Id, "test2@example.com").Wait();
            Console.WriteLine("Added test2@example.com to unsubscribe group {0}", group.Id);

            // GET THE ADDRESSES IN A GROUP
            var unsubscribedAddresses = client.Suppressions.GetUnsubscribedAddressesAsync(group.Id).Result;
            Console.WriteLine("There are {0} unsubscribed addresses in group {1}", unsubscribedAddresses.Length, group.Id);

            // REMOVE ALL ADDRESSES FROM UNSUBSCRIBE GROUP
            foreach (var address in unsubscribedAddresses)
            {
                client.Suppressions.RemoveAddressFromSuppressionGroupAsync(group.Id, address).Wait();
                Console.WriteLine("{0} removed from unsubscribe group {1}", address, group.Id);
            }

            // MAKE SURE THERE ARE NO ADDRESSES IN THE GROUP
            unsubscribedAddresses = client.Suppressions.GetUnsubscribedAddressesAsync(group.Id).Result;
            if (unsubscribedAddresses.Length == 0)
            {
                Console.WriteLine("As expected, there are no more addresses in group {0}", group.Id);
            }
            else
            {
                Console.WriteLine("We expected the group {1} to be empty but instead we found {0} unsubscribed addresses.", unsubscribedAddresses.Length, group.Id);
            }

            // DELETE UNSUBSCRIBE GROUP
            client.UnsubscribeGroups.DeleteAsync(newGroup.Id).Wait();
            Console.WriteLine("Suppression group {0} deleted", newGroup.Id);

            Console.WriteLine("\n\nPress any key to continue");
            Console.ReadKey();
        }

        private static void GlobalSuppressions(HttpClient httpClient)
        {
            Console.WriteLine("\n***** GLOBAL SUPPRESSION *****");

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            var client = new SendGrid.Client(apiKey: apiKey, httpClient: httpClient);

            // ADD EMAILS TO THE GLOBAL SUPPRESSION LIST
            var emails = new[] { "example@example.com", "example2@example.com" };
            client.GlobalSuppressions.AddAsync(emails).Wait();
            Console.WriteLine("The following emails have been added to the global suppression list: {0}", string.Join(", ", emails));
            Console.WriteLine("Is {0} unsubscribed (should be true): {1}", emails[0], client.GlobalSuppressions.IsUnsubscribedAsync(emails[0]).Result);
            Console.WriteLine("Is {0} unsubscribed (should be true): {1}", emails[1], client.GlobalSuppressions.IsUnsubscribedAsync(emails[1]).Result);

            // DELETE EMAILS FROM THE GLOBAL SUPPRESSION GROUP
            client.GlobalSuppressions.RemoveAsync(emails[0]).Wait();
            Console.WriteLine("{0} has been removed from the global suppression list", emails[0]);
            client.GlobalSuppressions.RemoveAsync(emails[1]).Wait();
            Console.WriteLine("{0} has been removed from the global suppression list", emails[1]);

            Console.WriteLine("Is {0} unsubscribed (should be false): {1}", emails[0], client.GlobalSuppressions.IsUnsubscribedAsync(emails[0]).Result);
            Console.WriteLine("Is {0} unsubscribed (should be false): {1}", emails[1], client.GlobalSuppressions.IsUnsubscribedAsync(emails[1]).Result);

            Console.WriteLine("\n\nPress any key to continue");
            Console.ReadKey();
        }

        private static void GlobalStats(HttpClient httpClient)
        {
            Console.WriteLine("\n***** GLOBAL STATS *****");

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            var client = new SendGrid.Client(apiKey: apiKey, httpClient: httpClient);

            // Global Stats provide all of your user’s email statistics for a given date range.
            var startDate = new DateTime(2015, 11, 01);
            var stats = client.GlobalStats.GetAsync(startDate).Result;
            Console.WriteLine("Number of stats with start date {0} and no end date: {1}", startDate.ToShortDateString(), stats.Length);

            var endDate = new DateTime(2015, 12, 01);
            stats = client.GlobalStats.GetAsync(startDate, endDate).Result;
            Console.WriteLine("Number of stats with start date {0} and end date {1}: {2}", startDate.ToShortDateString(), endDate.ToShortDateString(), stats.Length);

            stats = client.GlobalStats.GetAsync(startDate, endDate, AggregateBy.Day).Result;
            Console.WriteLine("Number of stats with start date {0} and end date {1} and aggregated by day: {2}", startDate.ToShortDateString(), endDate.ToShortDateString(), stats.Length);

            stats = client.GlobalStats.GetAsync(startDate, endDate, AggregateBy.Week).Result;
            Console.WriteLine("Number of stats with start date {0} and end date {1} and aggregated by week: {2}", startDate.ToShortDateString(), endDate.ToShortDateString(), stats.Length);

            stats = client.GlobalStats.GetAsync(startDate, endDate, AggregateBy.Month).Result;
            Console.WriteLine("Number of stats with start date {0} and end date {1} and aggregated by month: {2}", startDate.ToShortDateString(), endDate.ToShortDateString(), stats.Length);

            Console.WriteLine("\n\nPress any key to continue");
            Console.ReadKey();
        }

        private static void Lists(HttpClient httpClient)
        {
            Console.WriteLine("\n***** LISTS *****");

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            var client = new SendGrid.Client(apiKey: apiKey, httpClient: httpClient);

            var firstList = client.Lists.CreateAsync("My first list").Result;
            Console.WriteLine("List '{0}' created. Id: {1}", firstList.Name, firstList.Id);

            var secondList = client.Lists.CreateAsync("My second list").Result;
            Console.WriteLine("List '{0}' created. Id: {1}", secondList.Name, secondList.Id);

            client.Lists.UpdateAsync(firstList.Id, "New name").Wait();
            Console.WriteLine("List '{0}' updated", firstList.Id);

            var lists = client.Lists.GetAllAsync().Result;
            Console.WriteLine("All lists retrieved. There are {0} lists", lists.Length);

            client.Lists.DeleteAsync(firstList.Id).Wait();
            Console.WriteLine("List {0} deleted", firstList.Id);

            client.Lists.DeleteAsync(secondList.Id).Wait();
            Console.WriteLine("List {0} deleted", secondList.Id);

            lists = client.Lists.GetAllAsync().Result;
            Console.WriteLine("All lists retrieved. There are {0} lists", lists.Length);

            Console.WriteLine("\n\nPress any key to continue");
            Console.ReadKey();
        }
    }
}
