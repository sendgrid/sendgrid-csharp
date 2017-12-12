This documentation provides examples for specific use cases. Please [open an issue](https://github.com/sendgrid/sendgrid-sharp/issues) or make a pull request for any use cases you would like us to document here. Thank you!

# Table of Contents

* [Email - Attachments](#attachments)
* [Email - Kitchen Sink - an example with all settings used](#kitchensink)
* [Email - Send a Single Email to Multiple Recipients](#singleemailmultiplerecipients)
* [Email - Send a Single Email to a Single Recipient](#singleemailsinglerecipient)
* [Email - Send Multiple Emails to Multiple Recipients](#multipleemailsmultiplerecipients)
* [Email - Transactional Templates](#transactional-templates)
* [Transient Fault Handling](#transient-faults)
* [How to Setup a Domain Whitelabel](#domain-whitelabel)
* [How to View Email Statistics](#email-stats)
* [Create and deploy a simple Hello Email app on Microsoft Azure with C#](#deployazure)

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
            var bytes = File.ReadAllBytes("/Users/username/file.txt");
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

            using (var fileStream = File.OpenRead("/Users/username/file.txt"))
            {
                msg.AddAttachment("file.txt", fileStream);
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

            msg.AddSubstitution("%name1%", "Example Name 1");
            msg.AddSubstitution("%city1%", "Denver");
            var substitutions = new Dictionary<string, string>()
            {
                { "%name2%", "Example Name 2" },
                { "%city2%", "Orange" }
            };
            msg.AddSubstitutions(substitutions);

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

            msg.AddSubstitution("%name3%", "Example Name 3", 1);
            msg.AddSubstitution("%city3%", "Redwood City", 1);
            var substitutions1 = new Dictionary<string, string>()
            {
                { "%name4%", "Example Name 4" },
                { "%city4%", "London" }
            };
            msg.AddSubstitutions(substitutions1, 1);

            msg.AddCustomArg("marketing3", "true", 1);
            msg.AddCustomArg("transactional3", "false", 1);
            var customArgs1 = new Dictionary<string, string>()
            {
                { "marketing4", "false" },
                { "transactional4", "true" }
            };
            msg.AddCustomArgs(customArgs1, 1);

            msg.SetSendAt(1461775052, 1);

            // The values below this comment are global to entire message

            msg.SetFrom("test@example.com", "Example User 0");

            msg.SetSubject("this subject overrides the Global Subject");

            msg.SetGlobalSubject("Sending with SendGrid is Fun");

            msg.AddContent(MimeType.Text, "and easy to do anywhere, even with C#");
            msg.AddContent(MimeType.Html, "<strong>and easy to do anywhere, even with C#</strong>");
            var contents = new List<Content>()
            {
                new Content("text/calendar", "Party Time!!"),
                new Content("text/calendar2", "Party Time2!!")
            };
            msg.AddContents(contents);

            // For base64 encoding, see [`Convert.ToBase64String`](https://msdn.microsoft.com/en-us/library/system.convert.tobase64string(v=vs.110).aspx)
            // For an example using an attachment, please see this [use case](https://github.com/sendgrid/sendgrid-csharp/blob/master/USE_CASES.md#attachments).
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

            // For a full transactional template example, please see this [use case](https://github.com/sendgrid/sendgrid-csharp/blob/master/USE_CASES.md#transactional-templates).
            msg.SetTemplateId("13b8f94f-bcae-4ec6-b752-70d6cb59f932");

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
            var subject = "Sending with SendGrid is Fun";
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
            var subject = "Sending with SendGrid is Fun";
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

<a name="transactional-templates"></a>
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

<a name="transient-faults"></a>
# Transient Fault Handling

The SendGridClient provides functionality for handling transient errors that might occur when sending an HttpRequest. This includes client side timeouts while sending the mail, or certain errors returned within the 500 range. Errors within the 500 range are limited to 500 Internal Server Error, 502 Bad Gateway, 503 Service unavailable and 504 Gateway timeout.

By default, retry behaviour is off, you must explicitly enable it by setting the retry count to a value greater than zero. To set the retry count, you must use the SendGridClient construct that takes a **SendGridClientOptions** object, allowing you to configure the **ReliabilitySettings**

### RetryCount

The amount of times to retry the operation before reporting an exception to the caller. This is in addition to the initial attempt so setting a value of 1 would result in 2 attempts, the initial attempt and the retry. Defaults to zero, retry behaviour is not enabled. The maximum amount of retries permitted is 5. 

### MinimumBackOff

The minimum amount of time to wait between retries. 

### MaximumBackOff

The maximum possible amount of time to wait between retries. The maximum value allowed is 30 seconds

### DeltaBackOff

The value that will be used to calculate a random delta in the exponential delay between retries.  A random element of time is factored into the delta calculation as this helps avoid many clients retrying at regular intervals.


## Examples

In this example we are setting RetryCount to 2, with a minimum wait time of 1 seconds, a maximum of 10 seconds and a delta of 3 seconds

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

<a name="domain-whitelabel"></a>
# How to Setup a Domain Whitelabel

You can find documentation for how to setup a domain whitelabel via the UI [here](https://sendgrid.com/docs/Classroom/Basics/Whitelabel/setup_domain_whitelabel.html) and via API [here](https://github.com/sendgrid/sendgrid-csharp/blob/master/USAGE.md#whitelabel).

Find more information about all of SendGrid's whitelabeling related documentation [here](https://sendgrid.com/docs/Classroom/Basics/Whitelabel/index.html).

<a name="email-stats"></a>
# How to View Email Statistics

You can find documentation for how to view your email statistics via the UI [here](https://app.sendgrid.com/statistics) and via API [here](https://github.com/sendgrid/sendgrid-csharp/blob/master/USAGE.md#stats).

Alternatively, we can post events to a URL of your choice via our [Event Webhook](https://sendgrid.com/docs/API_Reference/Webhooks/event.html) about events that occur as SendGrid processes your email.

<a name="deployazure"></a>
# Create and deploy a simple Hello Email app on Microsoft Azure with C# 
### Create and Store SendGrid API Key

Log into your SendGrid account and create an API. 

1.	Go to the API Keys page in the SendGrid UI, and click Create API Key.
2.	Give your API key a name.
3.	Select Full Access, Restricted Access, or Billing Access.
4.	If you’re selecting Restricted Access, or Billing Access, select the specific permissions to give each category. For more information, see API key permissions.
5.	Click Create a Key.
6.	Copy your API Key somewhere safe. For security reasons, do not put it directly in your code, or commit it somewhere like GitHub

Note: The API key will only be displayed once, make sure you copy it somewhere secure.

### Create your Email Application

Create and save your project using your preferred IDE, this example uses Visual Studio.

Add the SendGrid SDK to your Project
1.	Using the toolbar navigate to Project and choose Manage NuGet
2.	Click the Browse tab, locate the up-to-date Send Grid and install the latest stable version (current v9.9.0). 
3.	Review the changes and accept the license agreement.

You should now be able to see SendGrid in the Project references.

Below is a simple C# code example, copy and paste into your project.

ASP.NET
```
    <div>
        <button ID="btnSend" onclick="btnSend_Click">Send</button>
    </div>
```

C# Code behind
```csharp
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.UI;
using SendGrid;
using SendGrid.Helpers.Mail;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        Execute().Wait();
    }
    static async Task Execute()
    {
        var apiKey = ConfigurationManager.AppSettings["apiKey"].ToString();
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("test@example.com", "Example User");
        var subject = "Sending with SendGrid is Fun";
        var to = new EmailAddress("test@example.com", "Example User");
        var plainTextContent = "and easy to do anywhere, even with C#";
        var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
    }
}
```
Update the TO and FROM emails with your email addresses.

In the web.config, add a line for an external file that will store the secret API key.
```XML
</connectionStrings>
   <appSettings file="..\..\AppSettingsSecrets.config">          
   </appSettings>
  <system.web>
  ```
Create an external XML file, that will not be uploaded to source control. The markup in the external file (AppSettingsSecrets.config in this sample), is the same markup that would be found in the web.config file:
```XML
<appSettings>   
   <!-- SendGrid-->
   <add key="apiKey" value="My API key value" />
</appSettings>
```
Test your application to confirm all settings are correct and save it.

### Add your application to GitHub
Log into your GitHub account and create a new repository. This can be done from the toolbar at the top of the window next to your profile icon.

Enter a short name for the repository name, for example HelloWorldAzure. The description is optional by can be very helpful. Once the options are completed to your presence, click Create Repository. GitHub will then create the repository and display a customized quick setup guide. 

In this example, the git command line would be:
```git
echo "# HelloWorldAzure" >> README.md
git init
git add README.md
git add --all
git commit -m "initial commit"
git remote add origin https://github.com/silviak/HelloWorldAzure.git
git push -u origin master
```
It is very important that the secrets file containing the API key is not stored in source control. If it does get uploaded, remove the file and add to the .gitignore.

Your application is now stored in source control.

### Setting up Azure
Log into your Azure portal and begin setting up your resources.

On the left toolbar, choose App Services and then Add. Choose the type of application and click Create. Choose the appropriate responses for your application and then click Create.

Once the application is deployed, it is time to set up the deployment with GitHub.

Click on the App on your Azure Dashboard, this will take you to an overview. On the App Service toolbar, choose Deployment Options.

Then configure the settings by choosing GitHub as your source. You will be prompted to authorize Azure to access GitHub. Once that is complete, you can choose the project repository you created earlier in this tutorial.  Once your settings are completed, click ok and you will see a notification that Azure is deploying the App Service. 

### Deploying API key to Azure
For more information see: [MSDN article](https://docs.microsoft.com/en-us/aspnet/identity/overview/features-api/best-practices-for-deploying-passwords-and-other-sensitive-data-to-aspnet-and-azure).

The app settings and connection string values override the same settings in the web.config file. In our example, we did not deploy these settings to Azure, but if these keys were in the web.config file, the settings shown on the portal would take precedence.

To set them manually, log in to your Azure Portal
Click Browse > Web Apps, then click the name of your web app.

Click All settings > Application settings.

Scroll down until you see the App setting and update with the information from your application. 

Click save. 

*** You are now ready to test your first email!
