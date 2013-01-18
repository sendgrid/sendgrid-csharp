To use SendGrid in your C# project, you can either <a href="https://github.com/sendgrid/sendgrid-csharp.git">download the SendGrid C# .NET libraries directly from our Github repository</a> or, if you have the NuGet package manager installed, you can grab them automatically.

```
PM> Install-Package SendGrid 
```

The SendGrid library depends on [RestSharp](https://github.com/restsharp/RestSharp). NuGet will handle this dependency automatically, otherwise you will need to add it manually. 

Once you have the SendGrid libraries properly referenced in your project, you can include calls to them in your code. 
For a sample implementation, check the [Example](https://github.com/sendgrid/sendgrid-csharp/tree/master/SendGrid/Example) folder.

Add the following namespaces to use the library:
```csharp
using System.Net;
using System.Net.Mail;
using SendGridMail;
using SendGridMail.Transport;
```

#How to: Create an email

Use the static **SendGrid.GetInstance** method to create an email message that is of type **SendGrid**. Once the message is created, you can use **SendGrid** properties and methods to set values including the email sender, the email recipient, and the subject and body of the email.

The following example demonstrates how to create an email object and populate it:

```csharp
// Create the email object first, then add the properties.
var myMessage = SendGrid.GetInstance();

// Add the message properties.
myMessage.From = new MailAddress("john@example.com");

// Add multiple addresses to the To field.
List<String> recipients = new List<String>
{
    @"Jeff Smith <jeff@example.com>",
    @"Anna Lidman <anna@example.com>",
    @"Peter Saddow <peter@example.com>"
};

myMessage.AddTo(recipients);

myMessage.Subject = "Testing the SendGrid Library";

//Add the HTML and Text bodies
myMessage.Html = "<p>Hello World!</p>";
myMessage.Text = "Hello World plain text!";
```

#How to: Send an Email

After creating an email message, you can send it using either SMTP or the Web API provided by SendGrid. For details about the benefits and drawbacks of each API, see [SMTP vs. Web API](http://sendgrid.com/docs/Integrate/) in the SendGrid documentation.

Sending email with either protocol requires that you supply your SendGrid account credentials (username and password). The following code demonstrates how to wrap your credentials in a **NetworkCredential** object:

```csharp
// Create network credentials to access your SendGrid account.
var username = "your_sendgrid_username";
var pswd = "your_sendgrid_password";

var credentials = new NetworkCredential(username, pswd);
```
To send an email message, use the **Deliver** method on either the **SMTP** class, which uses the SMTP protocol, or the **Web** transport class, which calls the SendGrid Web API. The following examples show how to send a message using both SMTP and the Web API.


##SMTP
```csharp
// Create the email object first, then add the properties.
SendGrid myMessage = SendGrid.GetInstance();
myMessage.AddTo("anna@example.com");
myMessage.From = new MailAddress("john@example.com", "John Smith");
myMessage.Subject = "Testing the SendGrid Library";
myMessage.Text = "Hello World!";

// Create credentials, specifying your user name and password.
var credentials = new NetworkCredential("username", "password");

// Create an SMTP transport for sending email.
var transportSMTP = SMTP.GetInstance(credentials);

// Send the email.
transportSMTP.Deliver(myMessage);
```

##Web API
```csharp
// Create the email object first, then add the properties.
SendGrid myMessage = SendGrid.GenerateInstance();
myMessage.AddTo("anna@example.com");
myMessage.From = new MailAddress("john@example.com", "John Smith");
myMessage.Subject = "Testing the SendGrid Library";
myMessage.Text = "Hello World!";

// Create credentials, specifying your user name and password.
var credentials = new NetworkCredential("username", "password");

// Create a Web transport for sending email.
var transportWeb = Web.GetInstance(credentials);

// Send the email.
transportWeb.Deliver(myMessage);
```

#How to: Add an Attachment

Attachments can be added to a message by calling the **AddAttachment** method and specifying the name and path of the file you want to attach, or by passing a stream. You can include multiple attachments by calling this method once for each file you wish to attach. The following example demonstrates adding an attachment to a message:

```csharp
SendGrid myMessage = SendGrid.GenerateInstance();
myMessage.AddTo("anna@example.com");
myMessage.From = new MailAddress("john@example.com", "John Smith");
myMessage.Subject = "Testing the SendGrid Library";
myMessage.Text = "Hello World!";

myMessage.AddAttachment(@"C:\file1.txt");
```

#How to: Use filters to enable footers, tracking, and analytics

SendGrid provides additional email functionality through the use of filters. These are settings that can be added to an email message to enable specific functionality such as click tracking, Google analytics, subscription tracking, and so on. For a full list of filters, see [Filter Settings](http://docs.sendgrid.com/documentation/api/smtp-api/filter-settings/).

Filters can be applied to **SendGrid** email messages using methods implemented as part of the **SendGrid** class. Before you can enable filters on an email message, you must first initialize the list of available filters by calling the **InitializeFilters** method.

The following examples demonstrate the footer and click tracking filters:

##Footer
```csharp
// Create the email object first, then add the properties.
SendGrid myMessage = SendGrid.GetInstance();
myMessage.AddTo("anna@example.com");
myMessage.From = new MailAddress("john@example.com", "John Smith");
myMessage.Subject = "Testing the SendGrid Library";
myMessage.Text = "Hello World!";

myMessage.InitializeFilters();
// Add a footer to the message.
myMessage.EnableFooter("PLAIN TEXT FOOTER", "<p><em>HTML FOOTER</em></p>");
```

##Click tracking
```csharp
// Create the email object first, then add the properties.
SendGrid myMessage = SendGrid.GetInstance();
myMessage.AddTo("anna@example.com");
myMessage.From = new MailAddress("john@example.com", "John Smith");
myMessage.Subject = "Testing the SendGrid Library";
myMessage.Html = "<p><a href=\"http://www.example.com\">Hello World Link!</a></p>";
myMessage.Text = "Hello World!";

myMessage.InitializeFilters();
// true indicates that links in plain text portions of the email 
// should also be overwritten for link tracking purposes. 
myMessage.EnableClickTracking(true);
```
[SendGrid Documentation](http://www.sendgrid.com/docs)

This readme adapted from the [How to Send Email Using SendGrid with Windows Azure](http://www.windowsazure.com/en-us/develop/net/how-to-guides/sendgrid-email-service/)
