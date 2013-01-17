To use SendGrid in your C# project, you can either <a href="https://github.com/sendgrid/sendgrid-csharp.git">download the SendGrid C# .NET libraries directly from our Github repository</a> or, if you have the NuGet package manager installed, you can grab them automatically.

```
PM> Install-Package SendGrid 
```

Once you have the SendGrid libraries properly referenced in your project, you can include calls to them in your code. This first example shows the basic structure of the code required to call the SendGrid libraries and send email. For a sample implementation, check the Examples folder of this repository.

In order to send a simple HTML email using the SendGrid SMTP API, use this code sample:

```csharp
public void SimpleHTMLEmail()
        {
            //create a new message object
            var message = SendGrid.GetInstance();

            //set the message recipients
            foreach(string recipient in _to)
            {
                message.AddTo(recipient);
            }

            //set the sender
            message.From = new MailAddress(_from);

            //set the message body
            message.Html = "<html>HelloWorld</html>";

            //set the message subject
            message.Subject = "Hello World HTML Test";

            //create an instance of the SMTP transport mechanism
            var transportInstance = SMTP.GetInstance(new NetworkCredential(_username, _password));

            //send the mail
            transportInstance.Deliver(message);
        }
```

There are two calls you can make to the AddTo function. One injects addresses into the standard MIME TO: field, and that's done (as in the above code sample) like so:

```csharp
message.AddTo(sampleListName(){"foo@bar.com"});
```

Using the X-SMTPAPI header makes this process a little cleaner and causes the message to appear as if it were sent directly to the recipient, as opposed to lots and lots of recipients. In order to inject addresses into into the X-SMTPAPI header, make the call like so:

```csharp
message.Header.AddTo(sampleListName(){"foo@bar.com"});
```

If you would prefer to send a simple HTML message using the SendGrid Web API, use this code:

```csharp
public void SimpleHTMLEmail()
        {
            //create a new message object
            var message = SendGrid.GetInstance();

            //set the message recipients
            foreach (string recipient in _to)
            {
                message.AddTo(recipient);
            }

            //set the sender
            message.From = new MailAddress(_from);

            //set the message body
            message.Html = "<html>HelloWorld</html>";

            //set the message subject
            message.Subject = "Hello World HTML Test";

            //create an instance of the Web transport mechanism
            var transportInstance = Web.GetInstance(new NetworkCredential(_username, _password));

            //send the mail
            transportInstance.Deliver(message);
        }
```

The following is a code sample that allows you to add substitution values to your messages. This is a powerful feature that makes it significantly easier to generate personalized email messages:

```csharp
public void AddSubstitutionValues()
        {
            //create a new message object
            var message = SendGrid.GetInstance();

            //set the message recipients
            foreach (string recipient in _to)
            {
                message.AddTo(recipient);
            }

            //set the sender
            message.From = new MailAddress(_from);

            //set the message body
            message.Text = "Hi %name%! Pleased to meet you!";

            //set the message subject
            message.Subject = "Testing Substitution Values";

            //This replacement key must exist in the message body
            var replacementKey = "%name%";

            //There should be one value for each recipient in the To list
            var substitutionValues = new List<String> {"Mr Foo", "Mrs Raz"};

            message.AddSubVal(replacementKey, substitutionValues);

            //create an instance of the SMTP transport mechanism
            var transportInstance = SMTP.GetInstance(new NetworkCredential(_username, _password));

            //enable bypass list management
            message.EnableBypassListManagement();

            //send the mail
            transportInstance.Deliver(message);
        }
```
