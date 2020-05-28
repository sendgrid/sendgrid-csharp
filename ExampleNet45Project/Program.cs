using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Example
{
    internal class Program
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use.
        // See https://docs.microsoft.com/dotnet/api/system.net.http.httpclient#remarks
        private static readonly HttpClient HttpClient = new HttpClient();

        private static async Task Main()
        {
            var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";
            var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddJsonFile($"appsettings.{env}.json", optional: true)
                    .Build();

            // Retrieve the API key.
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY") ?? configuration["SendGrid:ApiKey"];

            var client = new SendGridClient(HttpClient, apiKey);

            // Send a Single Email using the Mail Helper
            var from = new EmailAddress(configuration.GetValue("SendGrid:From", "test@example.com"), "Example User");
            var subject = "Hello World from the Twilio SendGrid CSharp Library Helper!";
            var to = new EmailAddress(configuration.GetValue("SendGrid:To", "test@example.com"), "Example User");
            var plainTextContent = "Hello, Email from the helper [SendSingleEmailAsync]!";
            var htmlContent = "<strong>Hello, Email from the helper! [SendSingleEmailAsync]</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(msg.Serialize());
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress <Enter> to continue.");
            Console.ReadLine();

            // Send a Single Email using the Mail Helper with convenience methods and initialized SendGridMessage object
            msg = new SendGridMessage
            {
                From = from,
                Subject = subject,
                PlainTextContent = plainTextContent,
                HtmlContent = htmlContent
            };
            msg.AddTo(to);
            response = await client.SendEmailAsync(msg);
            Console.WriteLine(msg.Serialize());
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress <Enter> to continue.");
            Console.ReadLine();

            // Send a Single Email using the Mail Helper, entirely with convenience methods
            msg = new SendGridMessage();
            msg.SetFrom(from);
            msg.SetSubject(subject);
            msg.AddContent(MimeType.Text, plainTextContent);
            msg.AddContent(MimeType.Html, htmlContent);
            msg.AddTo(to);
            response = await client.SendEmailAsync(msg);
            Console.WriteLine(msg.Serialize());
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress <Enter> to continue.");
            Console.ReadLine();

            // Send a Single Email Without the Mail Helper
            var data = @"{
              'personalizations': [
                {
                  'to': [
                    {
                      'email': 'test@example.com'
                    }
                  ],
                  'subject': 'Hello World from the Twilio SendGrid C# Library!'
                }
              ],
              'from': {
                'email': 'test@example.com'
              },
              'content': [
                {
                  'type': 'text/plain',
                  'value': 'Hello, Email!'
                }
              ]
            }";
            var json = JsonConvert.DeserializeObject<object>(data);
            response = await client.RequestAsync(BaseClient.Method.POST,
                                                 json.ToString(),
                                                 urlPath: "mail/send");
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress <Enter> to continue.");
            Console.ReadLine();

            // GET Collection
            var queryParams = @"{
                'limit': 100
            }";
            response = await client.RequestAsync(method: BaseClient.Method.GET,
                                                          urlPath: "asm/groups",
                                                          queryParams: queryParams);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress <Enter> to continue to POST.");
            Console.ReadLine();

            // POST
            var requestBody = @"{
              'description': 'Suggestions for products our users might like.', 
              'is_default': false, 
              'name': 'Magic Products'
            }";
            json = JsonConvert.DeserializeObject<object>(requestBody);
            response = await client.RequestAsync(method: BaseClient.Method.POST,
                                                 urlPath: "asm/groups",
                                                 requestBody: json.ToString());
            var dsResponse = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress <Enter> to continue to GET single.");
            Console.ReadLine();

            if (dsResponse != null && dsResponse.ContainsKey("id"))
            {
                var groupId = dsResponse["id"].ToString();

                // GET Single
                response = await client.RequestAsync(method: BaseClient.Method.GET,
                    urlPath: $"asm/groups/{groupId}");
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.Body.ReadAsStringAsync().Result);
                Console.WriteLine(response.Headers);
                Console.WriteLine("\n\nPress <Enter> to continue to PATCH.");
                Console.ReadLine();


                // PATCH
                requestBody = @"{
                    'name': 'Cool Magic Products'
                }";
                json = JsonConvert.DeserializeObject<object>(requestBody);

                response = await client.RequestAsync(method: BaseClient.Method.PATCH,
                    urlPath: $"asm/groups/{groupId}",
                    requestBody: json.ToString());
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.Body.ReadAsStringAsync().Result);
                Console.WriteLine(response.Headers.ToString());

                Console.WriteLine("\n\nPress <Enter> to continue to PUT.");
                Console.ReadLine();

                // DELETE
                response = await client.RequestAsync(method: BaseClient.Method.DELETE,
                    urlPath: $"asm/groups/{groupId}");
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.Headers.ToString());
                Console.WriteLine("\n\nPress <Enter> to DELETE and exit.");
                Console.ReadLine();
            }
        }
    }
}