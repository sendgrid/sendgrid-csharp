This documentation provides examples for specific use cases. Please [open an issue](https://github.com/sendgrid/sendgrid-sharp/issues) or make a pull request for any use cases you would like us to document here. Thank you!

# Table of Contents

* [Transactional Templates](#transactional_templates)

<a name="transactional_templates"></a>
# Transactional Templates

For this example, we assume you have created a [transactional template](https://sendgrid.com/docs/User_Guide/Transactional_Templates/index.html). Following is the template content we used for testing.

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

## With Mail Helper Class

```csharp
using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

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
            string apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY", EnvironmentVariableTarget.User);
            dynamic sg = new SendGridAPIClient(apiKey);

            Email from = new Email("test@example.com");
            String subject = "I'm replacing the subject tag";
            Email to = new Email("test@example.com");
            Content content = new Content("text/html", "I'm replacing the <strong>body tag</strong>");
            Mail mail = new Mail(from, subject, to, content);

            mail.TemplateId = "13b8f94f-bcae-4ec6-b752-70d6cb59f932";
            mail.Personalization[0].AddSubstitution("-name-", "Example User");
            mail.Personalization[0].AddSubstitution("-city-", "Denver");

            dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());
        }
    }
}
```

## Without Mail Helper Class

```csharp
using System;
using SendGrid;
using Newtonsoft.Json; // You can generate your JSON string yourelf or with another library if you prefer
using System.Threading.Tasks;

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
            String apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY", EnvironmentVariableTarget.User);
            dynamic sg = new SendGridAPIClient(apiKey);

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
            Object json = JsonConvert.DeserializeObject<Object>(data);
            dynamic response = await sg.client.mail.send.post(requestBody: json.ToString());
        }
    }
}
```
