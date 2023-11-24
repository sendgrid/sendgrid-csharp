This document provides examples for specific use cases.

# Table of Contents

- [Table of Contents](#table-of-contents)
- [Attachments](#attachments)
- [Kitchen Sink - an example with all settings used](#kitchen-sink---an-example-with-all-settings-used)
- [Send a Single Email to Multiple Recipients](#send-a-single-email-to-multiple-recipients)
- [Send a Single Email to a Single Recipient](#send-a-single-email-to-a-single-recipient)
- [Send Multiple Emails to Multiple Recipients](#send-multiple-emails-to-multiple-recipients)
- [Send Multiple Emails with Personalizations](#send-multiple-emails-with-personalizations)
- [Transactional Templates](#transactional-templates)
    - [With Mail Helper Class](#with-mail-helper-class)
    - [Without Mail Helper Class](#without-mail-helper-class)
- [_Legacy_ Transactional Templates](#legacy-transactional-templates)
    - [Legacy Template With Mail Helper Class](#legacy-template-with-mail-helper-class)
    - [Legacy Template Without Mail Helper Class](#legacy-template-without-mail-helper-class)
- [Transient Fault Handling](#transient-fault-handling)
        - [RetryCount](#retrycount)
        - [MinimumBackOff](#minimumbackoff)
        - [MaximumBackOff](#maximumbackoff)
        - [DeltaBackOff](#deltabackoff)
    - [Examples](#examples)
- [How to Setup a Domain Authentication](#how-to-setup-a-domain-authentication)
- [How to View Email Statistics](#how-to-view-email-statistics)
- [How to transform HTML into plain text](#how-to-transform-html-into-plain-text)
- [Send an Email With Twilio Email (Pilot)](#send-an-email-with-twilio-email-pilot)
- [Send an SMS Message](#send-an-sms-message)
- [Working With Permissions](#working-with-permissions)

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
            ExecuteManualAttachmentAdd().Wait();
            ExecuteStreamAttachmentAdd().Wait();
        }

        static async Task ExecuteManualAttachmentAdd()
        {
            var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@example.com");
            var subject = "Subject";
            var to = new EmailAddress("test@example.com");
            var body = "Email Body";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, "");
            var bytes = File.ReadAllBytes("C:\\Users\\username\\file.txt");
            var file = Convert.ToBase64String(bytes);
            msg.AddAttachment("file.txt", file);
            var response = await client.SendEmailAsync(msg);
        }

        static async Task ExecuteStreamAttachmentAdd()
        {
            var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@example.com");
            var subject = "Subject";
            var to = new EmailAddress("test@example.com");
            var body = "Email Body";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, "");

            using (var fileStream = File.OpenRead("C:\\Users\\username\\file.txt"))
            {
                await msg.AddAttachmentAsync("file.txt", fileStream);
                var response = await client.SendEmailAsync(msg);
            }
        }
    }
}
```

<a name="kitchensink"></a>
# Kitchen Sink - an example with all settings used

```csharp
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
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
            var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage();

            // For a detailed description of each of these settings, please see the [documentation](https://sendgrid.com/docs/API_Reference/api_v3.html).

            msg.AddTo(new EmailAddress("test1@example.com", "Example User 1"));
            var to_emails = new List<EmailAddress>
            {
                new EmailAddress("test2@example.com", "Example User2"),
                new EmailAddress("test3@example.com", "Example User3")
            };
            msg.AddTos(to_emails);

            msg.AddCc(new EmailAddress("test4@example.com", "Example User 4"));
            var cc_emails = new List<EmailAddress>
            {
                new EmailAddress("test5@example.com", "Example User5"),
                new EmailAddress("test6@example.com", "Example User6")
            };
            msg.AddCcs(cc_emails);

            msg.AddBcc(new EmailAddress("test7@example.com", "Example User 7"));
            var bcc_emails = new List<EmailAddress>
            {
                new EmailAddress("test8@example.com", "Example User8"),
                new EmailAddress("test9@example.com", "Example User9")
            };
            msg.AddBccs(bcc_emails);

            msg.AddHeader("X-Test1", "Test1");
            msg.AddHeader("X-Test2", "Test2");
            var headers = new Dictionary<string, string>()
            {
                { "X-Test3", "Test3" },
                { "X-Test4", "Test4" }
            };
            msg.AddHeaders(headers);

            // If you require complex substitutions this [use case](USE_CASES.md#transactional-templates).
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

            msg.AddCustomArg("marketing1", "false");
            msg.AddCustomArg("transactional1", "true");
            var customArgs = new Dictionary<string, string>()
            {
                { "marketing2", "true" },
                { "transactional2", "false" }
            };
            msg.AddCustomArgs(customArgs);

            msg.SetSendAt(1461775051);

            // If you need to add more [Personalizations](https://sendgrid.com/docs/Classroom/Send/v3_Mail_Send/personalizations.html), here is an example of adding another Personalization by passing in a personalization index

            msg.AddTo(new EmailAddress("test10@example.com", "Example User 10"), 1);
            var to_emails1 = new List<EmailAddress>
            {
                new EmailAddress("test11@example.com", "Example User11"),
                new EmailAddress("test12@example.com", "Example User12")
            };
            msg.AddTos(to_emails1, 1);

            msg.AddCc(new EmailAddress("test13@example.com", "Example User 13"), 1);
            var cc_emails1 = new List<EmailAddress>
            {
                new EmailAddress("test14@example.com", "Example User14"),
                new EmailAddress("test15@example.com", "Example User15")
            };
            msg.AddCcs(cc_emails1, 1);

            msg.AddBcc(new EmailAddress("test16@example.com", "Example User 16"), 1);
            var bcc_emails1 = new List<EmailAddress>
            {
                new EmailAddress("test17@example.com", "Example User17"),
                new EmailAddress("test18@example.com", "Example User18")
            };
            msg.AddBccs(bcc_emails1, 1);

            msg.AddHeader("X-Test5", "Test5", 1);
            msg.AddHeader("X-Test6", "Test6", 1);
            var headers1 = new Dictionary<string, string>()
            {
                { "X-Test7", "Test7" },
                { "X-Test8", "Test8" }
            };
            msg.AddHeaders(headers1, 1);

            // For a full transactional template example, please see this [use case](USE_CASES.md#transactional-templates).
            var dynamicTemplateData2 = new ExampleTemplateData
            {
                Subject = "Hi 2!",
                Name = "Example User 2",
                Location = new Location
                {
                    City = "Birmingham 2",
                    Country = "United Kingdom 2"
                }
            };

            msg.SetTemplateData(dynamicTemplateData2, 1);

            msg.AddCustomArg("marketing3", "true", 1);
            msg.AddCustomArg("transactional3", "false", 1);
            var customArgs1 = new Dictionary<string, string>()
            {
                { "marketing4", "false" },
                { "transactional4", "true" }
            };
            msg.AddCustomArgs(customArgs1, 1);

            msg.SetSendAt(1461775052, 1);

            // The values below this comment are global to an entire message

            msg.SetFrom("test@example.com", "Example User 0");

            msg.SetSubject("this subject overrides the Global Subject");

            msg.SetGlobalSubject("Sending with Twilio SendGrid is Fun");

            msg.AddContent(MimeType.Text, "and easy to do anywhere, even with C#");
            msg.AddContent(MimeType.Html, "<strong>and easy to do anywhere, even with C#</strong>");
            var contents = new List<Content>()
            {
                new Content("text/calendar", "Party Time!!"),
                new Content("text/calendar2", "Party Time2!!")
            };
            msg.AddContents(contents);

            // For base64 encoding, see [`Convert.ToBase64String`](https://msdn.microsoft.com/en-us/library/system.convert.tobase64string(v=vs.110).aspx)
            // For an example using an attachment, please see this [use case](USE_CASES.md#attachments).
            msg.AddAttachment("balance_001.pdf",
                              "base64 encoded string",
                              "application/pdf",
                              "attachment",
                              "Balance Sheet");
            var attachments = new List<Attachment>()
            {
                new Attachment()
                {
                    Content = "base64 encoded string",
                    Type = "image/png",
                    Filename = "banner.png",
                    Disposition = "inline",
                    ContentId = "Banner"
                },
                new Attachment()
                {
                    Content = "base64 encoded string",
                    Type = "image/png",
                    Filename = "banner2.png",
                    Disposition = "inline",
                    ContentId = "Banner 2"
                }
            };
            msg.AddAttachments(attachments);

            // For a full transactional template example, please see this [use case](USE_CASES.md#transactional-templates).
            msg.SetTemplateId("d-d42b0eea09964d1ab957c18986c01828");

            msg.AddGlobalHeader("X-Day", "Monday");
            var globalHeaders = new Dictionary<string, string>
            {
                { "X-Month", "January" },
                { "X-Year", "2017" }
            };
            msg.AddGlobalHeaders(globalHeaders);

            msg.AddSection("%section1", "Substitution for Section 1 Tag");
            var sections = new Dictionary<string, string>
            {
                {"%section2%", "Substitution for Section 2 Tag"},
                {"%section3%", "Substitution for Section 3 Tag"}
            };
            msg.AddSections(sections);

            msg.AddCategory("customer");
            var categories = new List<string>
            {
                "vip",
                "new_account"
            };
            msg.AddCategories(categories);

            msg.AddGlobalCustomArg("campaign", "welcome");
            var globalCustomArgs = new Dictionary<string, string>
            {
                { "sequence2", "2" },
                { "sequence3", "3" }
            };
            msg.AddGlobalCustomArgs(globalCustomArgs);

            msg.SetAsm(3, new List<int>() { 1, 4, 5 });

            msg.SetGlobalSendAt(1461775051);

            msg.SetIpPoolName("23");

            // This must be a valid [batch ID](https://sendgrid.com/docs/API_Reference/SMTP_API/scheduling_parameters.html)
            //msg.SetBatchId("some_batch_id");

            msg.SetBccSetting(true, "test@example.com");

            // Note: Bypass Spam, Bounce, and Unsubscribe management cannot be combined with Bypass List Management
            msg.BypassSpamManagement(true);

            msg.BypassBounceManagement(true);

            msg.BypassUnsubscribeManagement(true);
            
            // OR
            msg.SetBypassListManagement(true);

            msg.SetFooterSetting(true, "Some Footer HTML", "Some Footer Text");

            msg.SetSandBoxMode(true);

            msg.SetSpamCheck(true, 1, "https://gotchya.example.com");

            msg.SetClickTracking(true, false);

            msg.SetOpenTracking(true, "Optional tag to replace with the open image in the body of the message");

            msg.SetSubscriptionTracking(true,
                                       "HTML to insert into the text / html portion of the message",
                                       "text to insert into the text/plain portion of the message",
                                       "substitution tag");

            msg.SetGoogleAnalytics(true,
                                   "some campaign",
                                   "some content",
                                   "some medium",
                                   "some source",
                                   "some term");

            msg.SetReplyTo(new EmailAddress("test+reply@example.com", "Reply To Me"));

            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(msg.Serialize());
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers);
            Console.ReadLine();
        }
    }
}
```

<a name="singleemailmultiplerecipients"></a>
# Send a Single Email to Multiple Recipients

```csharp
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
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
            var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("test@example.com", "Example User");
            var tos = new List<EmailAddress>
            {
                new EmailAddress("test1@example.com", "Example User1"),
                new EmailAddress("test2@example.com", "Example User2"),
                new EmailAddress("test3@example.com", "Example User3")
            };
            var subject = "Sending with Twilio SendGrid is Fun";
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var showAllRecipients = false; // Set to true if you want the recipients to see each others email addresses

            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from,
                                                                       tos,
                                                                       subject,
                                                                       plainTextContent,
                                                                       htmlContent,
                                                                       showAllRecipients
                                                                       );
            var response = await client.SendEmailAsync(msg);
        }
    }
}
```

<a name="singleemailsinglerecipient"></a>
# Send a Single Email to a Single Recipient

```csharp
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
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
            var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@example.com", "Example User");
            var subject = "Sending with Twilio SendGrid is Fun";
            var to = new EmailAddress("test@example.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
```

<a name="multipleemailsmultiplerecipients"></a>
# Send Multiple Emails to Multiple Recipients

```csharp
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
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
            var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("test@example.com", "Example User");
            var tos = new List<EmailAddress>
            {
                new EmailAddress("test1@example.com", "Example User1"),
                new EmailAddress("test2@example.com", "Example User2"),
                new EmailAddress("test3@example.com", "Example User3")
            };
            var subjects = new List<string> { "Test Subject1", "Test Subject2", "Test Subject3" };
            var plainTextContent = "Hello -name-";
            var htmlContent = "Goodbye -name-";
            var substitutions = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>() {{"-name-", "Name1"}},
                new Dictionary<string, string>() {{"-name-", "Name2"}},
                new Dictionary<string, string>() {{"-name-", "Name3"}}
            };

            var msg = MailHelper.CreateMultipleEmailsToMultipleRecipients(from,
                                                                          tos,
                                                                          subjects,
                                                                          plainTextContent,
                                                                          htmlContent,
                                                                          substitutions
                                                                          );
            var response = await client.SendEmailAsync(msg);
        }
    }
}
```

<a name="multipleemailspersonalization"></a>
# Send Multiple Emails with Personalizations

```csharp
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
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
            var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@example.com");
            var subject = "Hello from Twilio SendGrid!";

            // Note that the domain for all from addresses must match
            var msg = new SendGridMessage();
            msg.Subject = subject;
            msg.AddContent(MimeType.Text, "Easy to use, even with C#!");
            msg.SetFrom(from);

            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Tos = new List<EmailAddress>() {
                        new EmailAddress("test1@example.com")
                    }
                },
                new Personalization() {
                    Tos = new List<EmailAddress>() {
                        new EmailAddress("test2@example.com")
                    },
                    From = new EmailAddress("test3@example.com")
                },
            };
            var response = await client.SendEmailAsync(msg);

            Console.WriteLine(msg.Serialize());
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers.ToString());
            Console.WriteLine("\n\nPress any key to exit.");
            Console.ReadLine();
        }
    }
}
```

<a name="transactional-templates"></a>
# Transactional Templates

For this example, we assume you have created a [dynamic transactional template](https://sendgrid.com/docs/ui/sending-email/how-to-send-an-email-with-dynamic-transactional-templates/) in the UI or via the API. Following is the template content we used for testing.

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

<a name="legacy-transactional-templates"></a>
# _Legacy_ Transactional Templates

For this example, we assume you have created a [legacy transactional template](https://sendgrid.com/docs/User_Guide/Transactional_Templates/index.html) in the UI or via the API. Following is the template content we used for testing.

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

<a name="transient-faults"></a>
# Transient Fault Handling

The SendGridClient provides functionality for handling transient errors that might occur when sending an HttpRequest. This includes client-side timeouts while sending the mail, or certain errors returned within the 500 range. Errors within the 500 range are limited to 500 Internal Server Error, 502 Bad Gateway, 503 Service unavailable and 504 Gateway timeout.

By default, retry behavior is off, you must explicitly enable it by setting the retry count to a value greater than zero. To set the retry count, you must use the SendGridClient construct that takes a **SendGridClientOptions** object, allowing you to configure the **ReliabilitySettings**

### RetryCount

The number of times to retry the operation before reporting an exception to the caller. This is in addition to the initial attempt so setting a value of 1 would result in 2 attempts, the initial attempt, and the retry. Defaults to zero, retry behavior is not enabled. The maximum amount of retries permitted is 5.

### MinimumBackOff

The minimum amount of time to wait between retries.

### MaximumBackOff

The maximum possible amount of time to wait between retries. The maximum value allowed is 30 seconds

### DeltaBackOff

The value that will be used to calculate a random delta in the exponential delay between retries.  A random element of time is factored into the delta calculation as this helps avoid many clients retrying at regular intervals.

## Examples

In this example, we are setting RetryCount to 2, with a minimum wait time of 1 second, a maximum of 10 seconds and a delta of 3 seconds

```csharp

var options = new SendGridClientOptions
{
    ApiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY"),
    ReliabilitySettings = new ReliabilitySettings(2, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(3))
};

var client = new SendGridClient(options);

```

The SendGridClientOptions object defines all the settings that can be set for the client, e.g.

```csharp

var options = new SendGridClientOptions
{
    ApiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY"),
    ReliabilitySettings = new ReliabilitySettings(2, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(3)),
    Host = "Your-Host",
    UrlPath = "Url-Path",
    Version = "3",
    RequestHeaders = new Dictionary<string, string>() {{"header-key", "header-value"}}
};

var client = new SendGridClient(options);

```

The SendGridClientOptions object can also be used to set the region to "eu", which will send the 
request to https://api.eu.sendgrid.com/. By default it is set to https://api.sendgrid.com/, e.g.

```csharp

var options = new SendGridClientOptions
{
    ApiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY"),
    ReliabilitySettings = new ReliabilitySettings(2, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(3)),
    Host = "Your-Host",
    UrlPath = "Url-Path",
    Version = "3",
    RequestHeaders = new Dictionary<string, string>() {{"header-key", "header-value"}}
};
options.SetDataResidency("eu");
var client = new SendGridClient(options);

OR

options.SetDataResidency("global");
var client = new SendGridClient(options);

```

<a name="domain-authentication"></a>
# How to Setup a Domain Authentication

You can find documentation for how to setup a domain authentication via the UI [here](https://sendgrid.com/docs/ui/account-and-settings/how-to-set-up-domain-authentication/) and via API [here](USAGE.md#sender-authentication).

Find more information about all of SendGrid's authentication related documentation [here](https://sendgrid.com/docs/ui/account-and-settings/).

<a name="email-stats"></a>
# How to View Email Statistics

You can find documentation for how to view your email statistics via the UI [here](https://app.sendgrid.com/statistics) and via API [here](USAGE.md#stats).

Alternatively, we can post events to a URL of your choice via our [Event Webhook](https://sendgrid.com/docs/API_Reference/Webhooks/event.html) about events that occur as Twilio SendGrid processes your email.

<a name="html-into-plain-text"></a>
# How to transform HTML into plain text

Although the HTML tags could be removed using regular expressions, the best solution is parsing the HTML code with a specific library, such as [HTMLAgilityPack](http://html-agility-pack.net/).

The following code shows how to parse an input string with HTML code and remove all tags:

```csharp
using HtmlAgilityPack;

namespace Example {

    internal class Example
    {
		/// <summary>
		/// Convert the HTML content to plain text
		/// </summary>
		/// <param name="html">The html content which is going to be converted</param>
		/// <returns>A string</returns>
		public static string HtmlToPlainText(string html)
		{
			HtmlDocument document = new HtmlDocument();
			document.LoadHtml(html);
			return document.DocumentNode == null ? string.Empty : document.DocumentNode.InnerText;
		}
	}
}

```

# Send an Email With Twilio Email (Pilot)

### 1. Obtain a Free Twilio Account

Sign up for a free Twilio account [here](https://www.twilio.com/try-twilio?source=sendgrid-csharp).

### 2. Set Up Your Environment Variables

The Twilio API allows for authentication using with either an API key/secret or your Account SID/Auth Token. You can create an API key [here](https://twil.io/get-api-key) or obtain your Account SID and Auth Token [here](https://twil.io/console).

Once you have those, follow the steps below based on your operating system.

#### Linux/Mac

```bash
echo "export TWILIO_API_KEY='YOUR_TWILIO_API_KEY'" > twilio.env
echo "export TWILIO_API_SECRET='YOUR_TWILIO_API_SECRET'" >> twilio.env

# or

echo "export TWILIO_ACCOUNT_SID='YOUR_TWILIO_ACCOUNT_SID'" > twilio.env
echo "export TWILIO_AUTH_TOKEN='YOUR_TWILIO_AUTH_TOKEN'" >> twilio.env
```

Then:

```bash
echo "twilio.env" >> .gitignore
source ./twilio.env
```

#### Windows

Temporarily set the environment variable (accessible only during the current CLI session):

```bash
set TWILIO_API_KEY=YOUR_TWILIO_API_KEY
set TWILIO_API_SECRET=YOUR_TWILIO_API_SECRET

: or

set TWILIO_ACCOUNT_SID=YOUR_TWILIO_ACCOUNT_SID
set TWILIO_AUTH_TOKEN=YOUR_TWILIO_AUTH_TOKEN
```

Or permanently set the environment variable (accessible in all subsequent CLI sessions):

```bash
setx TWILIO_API_KEY "YOUR_TWILIO_API_KEY"
setx TWILIO_API_SECRET "YOUR_TWILIO_API_SECRET"

: or

setx TWILIO_ACCOUNT_SID "YOUR_TWILIO_ACCOUNT_SID"
setx TWILIO_AUTH_TOKEN "YOUR_TWILIO_AUTH_TOKEN"
```

### 3. Initialize the Twilio Email Client

```csharp
var mailClient = new TwilioEmailClient(Environment.GetEnvironmentVariable("TWILIO_API_KEY"), Environment.GetEnvironmentVariable("TWILIO_API_SECRET"));

// or

var mailClient = new TwilioEmailClient(Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID"), Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN"));
```

This client has the same interface as the `SendGrid` client.

# Send an SMS Message

First, follow the above steps for creating a Twilio account and setting up environment variables with the proper credentials.

Then, install the Twilio Helper Library by following the [installation steps](https://github.com/twilio/twilio-csharp#installation).

Finally, send a message.

```csharp
using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TwilioTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var twilioAccountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            var twilioAuthToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
            TwilioClient.Init(twilioAccountSid, twilioAuthToken);

            var message = MessageResource.Create(
                new PhoneNumber("+11234567890"),
                from: new PhoneNumber("+10987654321"),
                body: "Hello World!"
            );
            Console.WriteLine(message.Sid);
        }
    }
}
```

<a name="working-with-permissions" ></a>
# Working with permissions

The permissions builder is a convenient way to manipulate API key permissions when creating new API keys or managing existing API keys. 

You can use the enums named according to the various [permissions](https://sendgrid.api-docs.io/v3.0/api-key-permissions) to add the scopes required for those permissions. 

By default, all scopes for a given permission are added; however, You can filter out certain scopes by passing a ScopeOptions parameter or adding some filtering functions.

For example, to create an API key for all *Alerts* scopes and read only *Marketing Campaigns*:

```
var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
var client = new SendGridClient(apiKey);
var builder = new SendGridPermissionsBuilder();
builder.AddPermissionsFor(SendGridPermission.Alerts);
builder.AddPermissionsFor(SendGridPermission.MarketingCampaigns, ScopeOptions.ReadOnly);

/*
The above builder will emit the following scopes:

alerts.create
alerts.delete
alerts.read
alerts.update
marketing_campaigns.read
*/

// now use the provided extension method to create an API key

await client.CreateApiKey(builder, "Alerts & Read-Only Marketing Campaigns API Key");
```

There are also some methods to easily create API keys for various common use cases.

For example, to create a Mail Send API key scoped for sending email:

```
var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
var client = new SendGridClient(apiKey);
var builder = new SendGridPermissionsBuilder();
builder.AddPermissionsFor(SendGridPermission.Mail);

/*
The above builder will emit the following scope:

mail.batch.create
mail.batch.delete
mail.batch.read
mail.batch.update
mail.send
*/

await client.CreateApiKey(builder, "Mail Send API Key");
```

The builder filters out duplicate scopes by default but you can also add filters to the builder so that your application will never create keys with certain scopes.

For example, you may want to allow an API key to do just about anything EXCEPT create more API keys.

```
var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
var client = new SendGridClient(apiKey);
var builder = new SendGridPermissionsBuilder();
builder.Exclude(scope => scope.StartsWith("api_keys"));
builder.AddPermissionsFor(SendGridPermission.Admin);

await client.CreateApiKey(builder, "Admin API Key that cannot manage other API keys");
```

The builder can also include individual scopes without having to use the AddPermissionsFor method.
```
var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
var client = new SendGridClient(apiKey);
var builder = new SendGridPermissionsBuilder();
/// use the method overload that accepts an IEnumerable<string>
var myScopes = new []{
    "mail.send", "alerts.read"
}
builder.Include(myScopes);

/// or use the method overload that accepts a params string[]
builder.Include("newsletter.read");

await client.CreateApiKey(builder, "Mail send, Alerts & Newletter read");
```
