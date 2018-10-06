
<a name="legacy-transactional-templates"></a>
# _Legacy_ Transactional Templates

For this example, we assume you have created a [legacy transactional template](https://sendgrid.com/docs/User_Guide/Transactional_Templates/index.html). Following is the template content we used for testing.

Template ID (replace with your own):

```text
13b8f94f-bcae-4ec6-b752-70d6cb59f932
```

Email Subject:

```text
<%subject%>
```

Template Body:

```html
<html>
<head>
    <title></title>
</head>
<body>
Hello -name-,
<br /><br/>
I'm glad you are trying out the template feature!
<br /><br/>
<%body%>
<br /><br/>
I hope you are having a great day in -city- :)
<br /><br/>
</body>
</html>
```

## Legacy Template With Mail Helper Class

```csharp
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
            msg.SetSubject("I'm replacing the subject tag");
            msg.AddTo(new EmailAddress("test@example.com", "Example User"));
            msg.AddContent(MimeType.Text, "I'm replacing the <strong>body tag</strong>");
            msg.SetTemplateId("13b8f94f-bcae-4ec6-b752-70d6cb59f932");
            msg.AddSubstitution("-name-", "Example User");
            msg.AddSubstitution("-city-", "Denver");
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers.ToString());
            Console.WriteLine("\n\nPress any key to exit.");
            Console.ReadLine();
        }
    }
}
```

## Legacy Template Without Mail Helper Class

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
                  'substitutions': {
                    '-name-': 'Example User',
                    '-city-': 'Denver'
                  },
                  'subject': 'I\'m replacing the subject tag'
                }
              ],
              'from': {
                'email': 'test@example.com'
              },
              'content': [
                {
                  'type': 'text/html',
                  'value': 'I\'m replacing the <strong>body tag</strong>'
                }
              ],
              'template_id': '13b8f94f-bcae-4ec6-b752-70d6cb59f932'
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
