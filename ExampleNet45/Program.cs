using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;

namespace Example
{
    internal class Example
    {
        private static void Main()
        {
            Execute().Wait();
        }

        static async Task Execute()
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
            var client = new SendGridClient(apiKey);

            // Send a Single Email using the Mail Helper
            var from = new EmailAddress("dx@sendgrid.com", "DX Team");
            var subject = "Hello World from the SendGrid CSharp Library Helper!";
            var to = new EmailAddress("elmer@sendgrid.com", "Elmer Thomas");
            var plainTextContent = "Hello, Email from the helper [SendSingleEmailAsync]!";
            var htmlContent = "<strong>Hello, Email from the helper! [SendSingleEmailAsync]</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(msg.Serialize());
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress any key to continue.");
            Console.ReadLine();

            // Send a Single Email using the Mail Helper with convenience methods
            msg = new SendGridMessage()
            {
                From = new EmailAddress("dx@sendgrid.com", "DX Team"),
                Subject = "Hello World from the SendGrid CSharp Library Helper!",
                PlainTextContent = "Hello, Email from the helper [SendSingleEmailAsync]!",
                HtmlContent = "<strong>Hello, Email from the helper! [SendSingleEmailAsync]</strong>"
            };
            msg.AddTo(new EmailAddress("elmer.thomas+test001@sendgrid.com", "Elmer Thomas"));

            response = await client.SendEmailAsync(msg);
            Console.WriteLine(msg.Serialize());
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress any key to continue.");
            Console.ReadLine();

            // Send a Single Email Without the Mail Helper
            string data = @"{
              'personalizations': [
                {
                  'to': [
                    {
                      'email': 'elmer@sendgrid.com'
                    }
                  ],
                  'subject': 'Hello World from the SendGrid C# Library!'
                }
              ],
              'from': {
                'email': 'dx@sendgrid.com'
              },
              'content': [
                {
                  'type': 'text/plain',
                  'value': 'Hello, Email!'
                }
              ]
            }";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            response = await client.RequestAsync(SendGridClient.Method.POST,
                                                 json.ToString(),
                                                 urlPath: "mail/send");
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress any key to continue.");
            Console.ReadLine();

            // Generic, direct object access, Hello World Send using the Mail Helper
            msg = new SendGridMessage()
            {
                From = new EmailAddress("dx@sendgrid.com", "DX Team"),
                Personalizations = new List<Personalization>() {
                    new Personalization() {
                        Tos = new List<EmailAddress>() {
                            new EmailAddress("elmer.thomas@sendgrid.com", "Elmer Thomas")
                        }
                    }
                },
                Subject = "Hello World from the SendGrid CSharp Library Helper!",
                Contents = new List<Content>() {
                    new PlainTextContent("Hello, Email from the helper [SendEmailAsync]!"),
                    new HtmlContent("<strong>Hello, Email from the helper! [SendEmailAsync]</strong>")
                }
            };

            response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress any key to continue.");
            Console.ReadLine();

            // GET Collection
            string queryParams = @"{
                'limit': 100
            }";
            response = await client.RequestAsync(method: SendGridClient.Method.GET,
                                                          urlPath: "asm/groups",
                                                          queryParams: queryParams);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress any key to continue to POST.");
            Console.ReadLine();

            // POST
            string requestBody = @"{
              'description': 'Suggestions for products our users might like.', 
              'is_default': false, 
              'name': 'Magic Products'
            }";
            json = JsonConvert.DeserializeObject<object>(requestBody);
            response = await client.RequestAsync(method: SendGridClient.Method.POST,
                                                 urlPath: "asm/groups",
                                                 requestBody: json.ToString());
            var ds_response = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response.Body.ReadAsStringAsync().Result);
            string group_id = ds_response["id"].ToString();
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress any key to continue to GET single.");
            Console.ReadLine();

            // GET Single
            response = await client.RequestAsync(method: SendGridClient.Method.GET,
                                                 urlPath: string.Format("asm/groups/{0}", group_id));
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress any key to continue to PATCH.");
            Console.ReadLine();

            // PATCH
            requestBody = @"{
                'name': 'Cool Magic Products'
            }";
            json = JsonConvert.DeserializeObject<object>(requestBody);

            response = await client.RequestAsync(method: SendGridClient.Method.PATCH,
                                                 urlPath: string.Format("asm/groups/{0}", group_id),
                                                 requestBody: json.ToString());
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers.ToString());

            Console.WriteLine("\n\nPress any key to continue to PUT.");
            Console.ReadLine();

            // DELETE
            response = await client.RequestAsync(method: SendGridClient.Method.DELETE,
                                                 urlPath: string.Format("asm/groups/{0}", group_id));
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers.ToString());
            Console.WriteLine("\n\nPress any key to exit.");
            Console.ReadLine();
        }
    }
}
