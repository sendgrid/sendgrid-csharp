This documentation provides examples for specific use cases. Please [open an issue](https://github.com/sendgrid/sendgrid-sharp/issues) or make a pull request for any use cases you would like us to document here. Thank you!

# Table of Contents

* [Attachments](#attachments)
* [Custom Personalization Usage]
    * [Multiple tos, ccs and bccs](#multiple_tos_ccs_bccs)
* [Transactional Templates](#transactional_templates)

<a name="attachments"></a>
# Attachments

```csharp
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using System.IO;

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
			var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
			var client = new SendGridClient(apiKey);
			var from = new EmailAddress("test@example.com");
			var subject = "Subject";
			var to = new EmailAddress("test@example.com");
			var body = "Email Body";
			var msg = MailHelper.CreateSingleEmail(from, to, subject, body, "");
			var bytes = File.ReadAllBytes("/Users/username/file.txt");
			var file = Convert.ToBase64String(bytes);
			msg.AddAttachment("file.txt", file);
			var response = await client.SendEmailAsync(msg);
		}
	}
}
```

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

<a name="multiple_tos_ccs_bccs"></a>
# Custom Personalization Usage - Multiple tos, ccs and bccs

In this example, each email recipient receives the same email and they can see each others email address, except for the bccs.

For more on [Personalizations](https://sendgrid.com/docs/Classroom/Send/v3_Mail_Send/personalizations.html), please see the documentation.

```csharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Execute().Wait();
        }

        static async Task Execute()
        {
            var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("test1@example.com", "Test1"));
            var personalization = new Personalization()
            {
                Subject = "Best Subject Line Ever",
                Tos = new List<EmailAddress>()
                    {
                       new EmailAddress() { Email = "test2@example.com", Name="Test2" },
                       new EmailAddress() { Email = "test3@example.com", Name="Test3" },
                       new EmailAddress() { Email = "test4@example.com", Name="Test4" },
                    },
                Bccs = new List<EmailAddress>()
                    {
                       new EmailAddress() { Email = "test5@example.com", Name="Test5" },
                       new EmailAddress() { Email = "test6@example.com", Name="Test6" },
                       new EmailAddress() { Email = "test7@example.com", Name="Test7" },
                    },
                Ccs = new List<EmailAddress>()
                    {
                       new EmailAddress() { Email = "test8@example.com", Name="Test8" },
                       new EmailAddress() { Email = "test9@example.com", Name="Test9" },
                       new EmailAddress() { Email = "test10@example.com", Name="Test10" },
                    }
            }
            msg.AddTo(new EmailAddress("test1@example.com"), 0, personalization);
            msg.AddContent(MimeType.Text, "Hello Email from the SendGrid C# Library");
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers.ToString());
            Console.WriteLine("\n\nPress any key to exit.");
            Console.ReadLine();
        }
    }
}
```
