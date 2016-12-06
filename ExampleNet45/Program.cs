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
            string apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
            Client client = new Client(apiKey);

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
            Response response = await client.RequestAsync(Client.Methods.POST,
                                                          json.ToString(),
                                                          urlPath: "mail/send");
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers);
            Console.ReadLine();

            Email from = new Email("dx@sendgrid");
            string subject = "Hello World from the SendGrid CSharp Library Helper!";
            Email to = new Email("elmer@sendgrid.com");
            Content content = new Content("text/plain", "Hello, Email from the helper!");
            Mail mail = new Mail(from, subject, to, content);

            response = await client.RequestAsync(Client.Methods.POST,
                                                 mail.Get(),
                                                 urlPath: "mail/send");
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers);
            Console.ReadLine();

            // GET Collection
            string queryParams = @"{
                'limit': 100
            }";
            response = await client.RequestAsync(method: Client.Methods.GET,
                                                          urlPath: "asm/groups",
                                                          queryParams: queryParams);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers.ToString());
            Console.WriteLine("\n\nPress any key to continue to POST.");
            Console.ReadLine();

            // POST
            string requestBody = @"{
              'description': 'Suggestions for products our users might like.', 
              'is_default': false, 
              'name': 'Magic Products'
            }";
            json = JsonConvert.DeserializeObject<object>(requestBody);
            response = await client.RequestAsync(method: Client.Methods.POST,
                                                 urlPath: "asm/groups",
                                                 requestBody: json.ToString());
            var ds_response = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response.Body.ReadAsStringAsync().Result);
            string group_id = ds_response["id"].ToString();
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers.ToString());
            Console.WriteLine("\n\nPress any key to continue to GET single.");
            Console.ReadLine();

            // GET Single
            response = await client.RequestAsync(method: Client.Methods.GET,
                                                 urlPath: string.Format("asm/groups/{0}", group_id));
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers.ToString());
            Console.WriteLine("\n\nPress any key to continue to PATCH.");
            Console.ReadLine();

            // PATCH
            requestBody = @"{
                'name': 'Cool Magic Products'
            }";
            json = JsonConvert.DeserializeObject<object>(requestBody);

            response = await client.RequestAsync(method: Client.Methods.PATCH,
                                                 urlPath: string.Format("asm/groups/{0}", group_id),
                                                 requestBody: json.ToString());
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers.ToString());

            Console.WriteLine("\n\nPress any key to continue to PUT.");
            Console.ReadLine();

            // DELETE
            response = await client.RequestAsync(method: Client.Methods.DELETE,
                                                 urlPath: string.Format("asm/groups/{0}", group_id));
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers.ToString());
            Console.WriteLine("\n\nPress any key to exit.");
            Console.ReadLine();
        }
    }
}
