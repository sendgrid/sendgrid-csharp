```csharp
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using SendGrid.Helpers.Mail;

namespace Example
{
    internal class Example
    {
        private static void Main()
        {
            String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            dynamic sg = new SendGrid.SendGridAPIClient(apiKey);

            string queryParams = @"{
                'limit': 100
            }";
            dynamic response = sg.client.api_keys.get(queryParams: queryParams);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
            Console.WriteLine(response.ResponseHeaders.ToString());

            Console.WriteLine("\n\nPress any key to continue to POST.");
            Console.ReadLine();

            // POST
            string requestBody = @"{
                'name': 'My API Key 5',
                'scopes': [
                    'mail.send',
                    'alerts.create',
                    'alerts.read'
                ]
            }";
            response = sg.client.api_keys.post(requestBody: requestBody);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
            Console.WriteLine(response.ResponseHeaders.ToString());
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var ds_response = jss.Deserialize<Dictionary<string, dynamic>>(response.ResponseBody.ReadAsStringAsync().Result);
            string api_key_id = ds_response["api_key_id"];

            Console.WriteLine("\n\nPress any key to continue to GET single.");
            Console.ReadLine();

            // GET Single
            response = sg.client.api_keys._(api_key_id).get();
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
            Console.WriteLine(response.ResponseHeaders.ToString());

            Console.WriteLine("\n\nPress any key to continue to PATCH.");
            Console.ReadLine();

            // PATCH
            requestBody = @"{
                'name': 'A New Hope'
            }";
            response = sg.client.api_keys._(api_key_id).patch(requestBody: requestBody);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
            Console.WriteLine(response.ResponseHeaders.ToString());

            Console.WriteLine("\n\nPress any key to continue to PUT.");
            Console.ReadLine();

            // PUT
            requestBody = @"{
                'name': 'A New Hope',
                'scopes': [
                '   user.profile.read',
                '   user.profile.update'
                ]
            }";
            response = sg.client.api_keys._(api_key_id).put(requestBody: requestBody);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
            Console.WriteLine(response.ResponseHeaders.ToString());

            Console.WriteLine("\n\nPress any key to continue to DELETE.");
            Console.ReadLine();

            // DELETE
            response = sg.client.api_keys._(api_key_id).delete();
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.ResponseHeaders.ToString());

            Console.WriteLine("\n\nPress any key to exit.");
            Console.ReadLine();
        }
    }
}
```