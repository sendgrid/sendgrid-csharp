<a name="transactional-templates"></a>
# Transactional Templates

For this example, we assume you have created a [transactional template](https://sendgrid.com/docs/User_Guide/Transactional_Templates/Create_and_edit_dynamic_transactional_templates.html).
Following is the template content we used for testing.

Template ID (replace with your own):

```text
d-d42b0eea09964d1ab957c18986c01828
```

Email Subject:

```text
Dynamic Subject: {{subject}}
```

Template Body:

```html
<html>
<head>
    <title></title>
</head>
<body>
Hello {{name}},
<br /><br/>
I'm glad you are trying out the dynamic template feature!
<br /><br/>
I hope you are having a great day in {{location.city}} :)
<br /><br/>
</body>
</html>
```

## With Mail Helper Class

```csharp
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System;

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
            var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("test@example.com", "Example User"));
            msg.AddTo(new EmailAddress("test@example.com", "Example User"));
            msg.SetTemplateId("d-d42b0eea09964d1ab957c18986c01828");

            var dynamicTemplateData = new ExampleTemplateData
            {
                Subject = "Hi!",
                Name = "Example User",
                Location = new Location
                {
                    City = "Birmingham",
                    Country = "United Kingdom"
                }
            };

            msg.SetTemplateData(dynamicTemplateData);
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers.ToString());
            Console.WriteLine("\n\nPress any key to exit.");
            Console.ReadLine();
        }

        private class ExampleTemplateData
        {
            [JsonProperty("subject")]
            public string Subject { get; set; }
            
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("location")]
            public Location Location { get; set; }
        }

        private class Location
        {
            [JsonProperty("city")]
            public string City { get; set; }
            
            [JsonProperty("country")]
            public string Country { get; set; }
        }
    }
}
```

Methods also exist on `MailHelper` to create dynamic template emails:
* `CreateSingleTemplateEmail`
* `CreateSingleTemplateEmailToMultipleRecipients`
* `CreateMultipleTemplateEmailsToMultipleRecipients`

## Without Mail Helper Class

```csharp
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;
using SendGrid;

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
            var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);

            string data = @"{
              'personalizations': [
                {
                  'to': [
                    {
                      'email': 'test@example.com'
                    }
                  ],
                  'dynamic_template_data': {
                    'subject': 'Hi!',
                    'name': 'Example User',
                    'location': {
                        'city': 'Birmingham',
                        'country': 'United Kingdom'
                    }
                  }
                }
              ],
              'from': {
                'email': 'test@example.com'
              },
              'template_id': 'd-d42b0eea09964d1ab957c18986c01828'
            }";
            var json = JsonConvert.DeserializeObject<Object>(data);
            var response = await client.RequestAsync(method: SendGridClient.Method.POST,
                                                     requestBody: json.ToString(),
                                                     urlPath: "mail/send");
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers.ToString());
            Console.WriteLine("\n\nPress any key to exit.");
            Console.ReadLine();
        }
    }
}
```
