# Special Announcement

We have released a [v3 beta branch](https://github.com/sendgrid/sendgrid-csharp/tree/v3beta) for this library that supports our new v3 Mail Send endpoint which is in open beta. The v3/mail/send/beta endpoint is not a production endpoint, so you should not integrate with it for your production email sending. However, when we make this an officially released feature it will be available at v3/mail//send.

Please try it out and let us know what you think about the endpoint and the library in the [issues area of this repo](https://github.com/sendgrid/sendgrid-csharp/issues), all of your feedback will be taken into account to influence the endpoint and this library.

Beginning with v3/mail/send/beta, the new version of our library will only support v3 endpoints.. Once this endpoint is out of beta, we will update the endpoint, removing the “/beta” from the URI. At this point, the v3 beta branch will be merged to master and will be our official library going forward. This means that we will no longer formally support the v2 mail.send.json endpoint in any of our libraries.

So long as you are not automatically pulling new versions of the library into your production code base, your integration will not break regardless of which endpoint you’re using. By the way, don't pull new versions into your production code base, because breaking changes break things.

The /api/mail.send.json endpoint, known as v2 mail send, is NOT going away. It will continue to work as it always has, happily sending your emails along as if nothing happened.

[![BuildStatus](https://travis-ci.org/sendgrid/sendgrid-csharp.png?branch=master)](https://travis-ci.org/sendgrid/sendgrid-csharp)

See the [changelog](https://github.com/sendgrid/sendgrid-csharp/blob/master/CHANGELOG.md) for updates. 

#Requirements

As of 4.0.0, this library requires .NET 4.5 and above. [Fork with .NET 4.0 support](https://www.nuget.org/packages/SendGrid.Net40/)

#Installation

To use SendGrid in your C# project, you can either <a href="https://github.com/sendgrid/sendgrid-csharp.git">download the SendGrid C# .NET libraries directly from our Github repository</a> or, if you have the NuGet package manager installed, you can grab them automatically.

```
PM> Install-Package SendGrid 
```

Once you have the SendGrid libraries properly referenced in your project, you can include calls to them in your code. 
For a sample implementation, check the [Example](https://github.com/sendgrid/sendgrid-csharp/tree/master/SendGrid/Example) folder.

Add the following namespaces to use the library:
```csharp
using System;
using System.Net;
using System.Net.Mail;
using SendGrid;
```

#How to: Create an email

Use the static **new SendGridMessage** constructor to create an email message that is of type **SendGridMessage**. Once the message is created, you can use **SendGridMessage** properties and methods to set values including the email sender, the email recipient, and the subject and body of the email.

The following example demonstrates how to create an email object and populate it:

```csharp
// Create the email object first, then add the properties.
var myMessage = new SendGridMessage();

// Add the message properties.
myMessage.From = new MailAddress("john@example.com");

// Add multiple addresses to the To field.
List<String> recipients = new List<String>
{
    "jeff@example.com",
    "anna@example.com",
    "peter@example.com"
};

myMessage.AddTo(recipients);

myMessage.Subject = "Testing the SendGrid Library";

//Add the HTML and Text bodies
myMessage.Html = "<p>Hello World!</p>";
myMessage.Text = "Hello World plain text!";
```

#How to: Send an Email

After creating an email message, you can send it using the Web API provided by SendGrid.

Sending email requires that you supply your SendGrid account credentials (username and password) OR a SendGrid API Key. API Key is the preferred method. API Keys are in beta. To configure API keys, visit https://sendgrid.com/beta/settings/api_keys

To send an email message, use the **DeliverAsync** method on the **Web** transport class, which calls the SendGrid Web API. The following example shows how to send a message.

```csharp
// Create the email object first, then add the properties.
SendGridMessage myMessage = new SendGridMessage();
myMessage.AddTo("anna@example.com");
myMessage.From = new MailAddress("john@example.com", "John Smith");
myMessage.Subject = "Testing the SendGrid Library";
myMessage.Text = "Hello World!";

// Create a Web transport, using API Key
var transportWeb = new Web("This string is a SendGrid API key");

// Send the email.
transportWeb.DeliverAsync(myMessage);
// NOTE: If your developing a Console Application, use the following so that the API call has time to complete
// transportWeb.DeliverAsync(myMessage).Wait();
```

#How to: Add an Attachment

Attachments can be added to a message by calling the **AddAttachment** method and specifying the name and path of the file you want to attach, or by passing a stream. You can include multiple attachments by calling this method once for each file you wish to attach. The following example demonstrates adding an attachment to a message:

```csharp
SendGridMessage myMessage = new SendGridMessage();
myMessage.AddTo("anna@example.com");
myMessage.From = new MailAddress("john@example.com", "John Smith");
myMessage.Subject = "Testing the SendGrid Library";
myMessage.Text = "Hello World!";

myMessage.AddAttachment(@"C:\file1.txt");
```

You can also add attachments from the data's **Stream**. It can be done by calling the same method as above, **AddAttachment**, but by passing in the Stream of the data, and the filename you want it to show as in the message.

```csharp
SendGridMessage myMessage = new SendGridMessage();
myMessage.AddTo("anna@example.com");
myMessage.From = new MailAddress("john@example.com", "John Smith");
myMessage.Subject = "Testing the SendGrid Library";
myMessage.Text = "Hello World!";

using (var attachmentFileStream = new FileStream(@"C:\file.txt", FileMode.Open))
{
    message.AddAttachment(attachmentFileStream, "My Cool File.txt");
}
```

#How to: Use filters to enable footers, tracking, analytics and templates

SendGrid provides additional email functionality through the use of filters. These are settings that can be added to an email message to enable specific functionality such as click tracking, Google analytics, subscription tracking, and so on. For a full list of filters, see [Filter Settings](https://sendgrid.com/docs/API_Reference/SMTP_API/apps.html).

Filters can be applied to **SendGrid** email messages using methods implemented as part of the **SendGrid** class. Before you can enable filters on an email message, you must first initialize the list of available filters by calling the **InitializeFilters** method.

The following examples demonstrate the footer and click tracking filters:

##Footer
```csharp
// Create the email object first, then add the properties.
SendGridMessage myMessage = new SendGridMessage();
myMessage.AddTo("anna@example.com");
myMessage.From = new MailAddress("john@example.com", "John Smith");
myMessage.Subject = "Testing the SendGrid Library";
myMessage.Text = "Hello World!";

// Add a footer to the message.
myMessage.EnableFooter("PLAIN TEXT FOOTER", "<p><em>HTML FOOTER</em></p>");
```

##Click tracking
```csharp
// Create the email object first, then add the properties.
SendGridMessage myMessage = new SendGridMessage();
myMessage.AddTo("anna@example.com");
myMessage.From = new MailAddress("john@example.com", "John Smith");
myMessage.Subject = "Testing the SendGrid Library";
myMessage.Html = "<p><a href=\"http://www.example.com\">Hello World Link!</a></p>";
myMessage.Text = "Hello World!";

// true indicates that links in plain text portions of the email 
// should also be overwritten for link tracking purposes. 
myMessage.EnableClickTracking(true);
```

##Template
```csharp
// Create the email object first, then add the properties.
SendGridMessage myMessage = new SendGridMessage();
myMessage.AddTo("anna@example.com");
myMessage.From = new MailAddress("john@example.com", "John Smith");
myMessage.Subject = "Testing the SendGrid Library";
myMessage.Text = "Hello World!";

// Enable template engine, you must send the template id
 myMessage.EnableTemplateEngine("template id");
```

#How to: Use the [Web API v3](https://sendgrid.com/docs/API_Reference/Web_API_v3/index.html)

Note: We have just begun to implement support for these endpoints and therefore only the following endpoints are currently supported. This functionality is located in the "SendGrid" project.

## API Keys ##

Please refer to [our documentation](https://sendgrid.com/docs/API_Reference/Web_API_v3/API_Keys/index.html) for further details.

List all API Keys belonging to the authenticated user [GET]

```csharp
String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
var client = new SendGrid.Client(apiKey);
// Leave off .Result for an asyncronous call
HttpResponseMessage responseGet = client.ApiKeys.Get().Result;
```

Generate a new API Key for the authenticated user [POST]

```csharp
String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
var client = new SendGrid.Client(apiKey);
var apiKeyName = "CSharpTestKey"; 
// Leave off .Result for an asyncronous call
HttpResponseMessage responsePost = client.ApiKeys.Post(apiKeyName).Result; 
```

Update the name of an existing API Key [PATCH]

```csharp
String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
var client = new SendGrid.Client(apiKey);
var apiKeyName = "CSharpTestKey"; 
var apiKeyId = "<API Key ID>";
// Leave off .Result for an asyncronous call
HttpResponseMessage responsePatch = client.ApiKeys.Patch(apiKeyId, apiKeyName).Result; 
```

Revoke an existing API Key [DELETE]

```csharp
String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
var client = new SendGrid.Client(apiKey);
var apiKeyId = "<API Key ID>";
// Leave off .Result for an asyncronous call
HttpResponseMessage responseDelete = client.ApiKeys.Delete(apiKeyId).Result; 
```

## Unsubscribe Groups ##

Please refer to [our documentation](https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/groups.html) for further details.

Retrieve all suppression groups associated with the user. [GET]

```csharp
String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
var client = new SendGrid.Client(apiKey);
// Leave off .Result for an asyncronous call
HttpResponseMessage responseGet = client.ApiKeys.Get().Result;
```

Get information on a single suppression group. [GET]

```csharp
String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
var client = new SendGrid.Client(apiKey);
// Leave off .Result for an asyncronous call
HttpResponseMessage responseGet = client.UnsubscribeGroups.Get().Result;
```

Create a new suppression group. [POST]

There is a limit of 25 groups per user.

```csharp
String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
var client = new SendGrid.Client(apiKey);
var unsubscribeGroupName = "CSharpTestUnsubscribeGroup"; 
var unsubscribeGroupDescription = "CSharp test Unsubscribe Group description."; 
var unsubscribeGroupIsDefault = false; 
// Leave off .Result for an asyncronous call
HttpResponseMessage responsePost = client.UnsubscribeGroups.Post(unsubscribeGroupName, unsubscribeGroupDescription, unsubscribeGroupIsDefault ).Result; 
```

Delete a suppression group. [DELETE]

You can only delete groups that have not been attached to sent mail in the last 60 days. If a recipient uses the “one-click unsubscribe” option on an email associated with a deleted group, that recipient will be added to the global suppression list.

```csharp
String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
var client = new SendGrid.Client(apiKey);
var unsubscribeGroupId = "<UNSUBSCRIBE GROUP ID>";
// Leave off .Result for an asyncronous call
HttpResponseMessage responseDelete = client.UnsubscribeGroups.Delete(unsubscribeGroupId).Result; 
```

## Suppressions ##

Please refer to [our documentation](https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/suppressions.html) for further details.

Get suppressed addresses for a given group. [GET]

```csharp
String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
var client = new SendGrid.Client(apiKey);
// Leave off .Result for an asyncronous call
int groupId = <Group ID>;
HttpResponseMessage responseGet = client.Suppressions.Get(groupId).Result;
```

Add recipient addresses to the suppressions list for a given group. [POST]

If the group has been deleted, this request will add the address to the global suppression.

```csharp
String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
var client = new SendGrid.Client(apiKey);
string[] emails = { "example@example.com", "example2@example.com" };
// Leave off .Result for an asyncronous call
HttpResponseMessage responsePost = client.Suppressions.Post(groupID, emails).Result;
```

Delete a recipient email from the suppressions list for a group. [DELETE]

```csharp
String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
var client = new SendGrid.Client(apiKey);
var groupId = "<UNSUBSCRIBE GROUP ID>";
// Leave off .Result for an asyncronous call
HttpResponseMessage responseDelete1 = client.Suppressions.Delete(groupId, "example@example.com").Result;
```

## Global Suppressions ##

Please refer to [our documentation](https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/global_suppressions.html) for further details.

Check if a recipient address is in the global suppressions group. [GET]

```csharp
String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
var client = new SendGrid.Client(apiKey);
// Leave off .Result for an asyncronous call
string email = "example@example.com";
HttpResponseMessage responseGet = client.GlobalSuppressions.Get(email).Result;
```

Add recipient addresses to the global suppression group. [POST]

If the group has been deleted, this request will add the address to the global suppression.

```csharp
String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
var client = new SendGrid.Client(apiKey);
string[] emails = { "example@example.com", "example2@example.com" };
// Leave off .Result for an asyncronous call
HttpResponseMessage responsePost = client.GlobalSuppressions.Post(emails).Result;
```

Delete a recipient email from the global suppressions group. [DELETE]

```csharp
String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
var client = new SendGrid.Client(apiKey);
string email = "example@example.com";
// Leave off .Result for an asyncronous call
HttpResponseMessage responseDelete1 = client.GlobalSuppressions.Delete(email).Result;
```

## Global Stats ##

Please refer to [our documentation](https://sendgrid.com/docs/API_Reference/Web_API_v3/Stats/global.html) for further details.

Global Stats provide all of your user’s email statistics for a given date range. [GET]

```csharp
String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
var client = new SendGrid.Client(apiKey);
var startDate = "2015-11-01"; // required
var endDate = "2015-12-01";
var aggregatedBy = "day"; // "week" or "month" are also options
// Leave off .Result for an asyncronous call
HttpResponseMessage responseGet = client.GlobalStats.Get(startDate, endDate, aggregatedBy).Result;
```

#How to: Testing

* Load the solution (We have tested using the Visual Studio Community Edition)
* In the Test Explorer, click "Run All". Tests for the Mail Send v2 endpoint are in the "Tests" project, while the tests for the v3 endpoints are in the "UnitTests" project. Selecting "Run All" from the Test Explorer will run the tests in both projects.

You can also test the code by building and running our "Example" project. It will run through the examples using an interactive console. You will need your API key to run the examples against your account.

[SendGrid Documentation](http://www.sendgrid.com/docs)

This readme adapted from [How to Send Email Using SendGrid with Windows Azure](http://www.windowsazure.com/en-us/develop/net/how-to-guides/sendgrid-email-service/)
