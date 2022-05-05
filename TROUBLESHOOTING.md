If you have an issue logging into your Twilio SendGrid account, please read this [document](https://sendgrid.com/docs/ui/account-and-settings/troubleshooting-login/). For any questions regarding login issues, please contact our [support team](https://support.sendgrid.com).

If you have a non-library Twilio SendGrid issue, please contact our [support team](https://support.sendgrid.com).


## Table of Contents

* [Continue Using the v2 API](#v2)
* [Environment Variables and Your Twilio SendGrid API Key](#environment)
* [Error Messages](#error)
* [Migrating from the v2 API to v3](#migrating)
* [Migrating from the v8 SDK to v9](#sdkmigration)
* [Missing Classes](#missing)
* [Testing v3 /mail/send Calls Directly](#testing)
* [Using .NET 4.5.1 and lower](#net45)
* [Using the Package Manager](#package-manager)
* [Versioning Convention](#versioning)
* [Viewing the Request Body](#request-body)
* [UI requests are failing](#ui-requests)
* [Verifying Event Webhooks](#signed-webhooks)

<a name="v2"></a>
## Continue Using the v2 API

[Here](https://github.com/sendgrid/sendgrid-csharp/tree/b27983a8f3d84a9d28972f2720cca0315ad9fe32) is the last working version with v2 support.

Using Nuget:

```bash
PM> Install-Package Sendgrid -Version 6.3.4
```

Download:

Click the "Clone or download" green button in [GitHub](https://github.com/sendgrid/sendgrid-csharp/tree/b27983a8f3d84a9d28972f2720cca0315ad9fe32) and choose download.

<a name="environment"></a>
## Environment Variables and Your Twilio SendGrid API Key

All of our examples assume you are using [environment variables](https://github.com/sendgrid/sendgrid-csharp#setup-environment-variables) to hold your Twilio SendGrid API key.

If you choose to add your Twilio SendGrid API key directly (not recommended):

`string apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");`

becomes

`string apiKey = "SENDGRID_API_KEY";`

In the first case, SENDGRID_API_KEY is in reference to the name of the environment variable, while the second case references the actual Twilio SendGrid API Key.

<a name="error"></a>
## Error Messages

By default if the API returns an error, it doesn't throw an exception, but you can read the error message returned by Twilio SendGrid's API:

```csharp
var response = await client.RequestAsync(method: SendGridClient.Method.POST,
                                         requestBody: msg.Serialize(),
                                         urlPath: "mail/send");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result); // The message will be here
Console.WriteLine(response.Headers.ToString());
```

If you want to throw the exception when the API returns an error, init the SendGridClient with options including 'HttpErrorAsException' as true:

```csharp
var client = new SendGridClient(new SendGridClientOptions{ ApiKey = apiKey, HttpErrorAsException = true });
```

Then if an error is thrown due to a failed request, a summary of that error along with a link to the appropriate place in the documentation will be sent back as a JSON object in the Exception Message. You can also deserialize the JSON object as a SendGridErrorResponse. Every error status code returned by the API has its own type of exception (BadRequestException, UnauthorizedException, PayloadTooLargeException, SendGridInternalException, etc.). All the possibles status codes and errors are documented [here](https://sendgrid.com/docs/API_Reference/Web_API_v3/Mail/errors.html).

### EXAMPLES

#### 401 - Unauthorized

```csharp
var client = new SendGridClient(new SendGridClientOptions{ ApiKey = "SG.12345678901234567890123456789012", HttpErrorAsException = true });

try
{
    var response = await client.RequestAsync(method: SendGridClient.Method.POST,
                                             requestBody: msg.Serialize(),
                                             urlPath: "mail/send");
}
catch(Exception ex)
{
    SendGridErrorResponse errorResponse = JsonConvert.DeserializeObject<SendGridErrorResponse>(ex.Message);
    Console.WriteLine(ex.Message);
    //{
    //  "ErrorHttpStatusCode":401,
    //  "ErrorReasonPhrase":"Unauthorized",
    //  "SendGridErrorMessage":"Permission denied, wrong credentials",
    //  "FieldWithError":null,
    //  "HelpLink":null
    //}
}
```

#### 400 - Bad Request - From Email Null

```csharp
var client = new SendGridClient(new SendGridClientOptions{ ApiKey = apiKey, HttpErrorAsException = true });

try
{
    var from = new EmailAddress("", "Example User"); // From email null
    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

    var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
}
catch (Exception ex)
{
    SendGridErrorResponse errorResponse = JsonConvert.DeserializeObject<SendGridErrorResponse>(ex.Message);
    Console.WriteLine(ex.Message);
    //{
    //  "ErrorHttpStatusCode":400,
    //  "ErrorReasonPhrase":"Bad Request",
    //  "SendGridErrorMessage":"The from email does not contain a valid address.",
    //  "FieldWithError":"from.email",
    //  "HelpLink":"http://sendgrid.com/docs/API_Reference/Web_API_v3/Mail/errors.html#message.from"
    //}
}
```

<a name="migrating"></a>
## Migrating from the v2 API to v3

Please review [our guide](https://sendgrid.com/docs/Classroom/Send/v3_Mail_Send/how_to_migrate_from_v2_to_v3_mail_send.html) on how to migrate from v2 to v3.

<a name="sdkmigration"></a>
## Migrating from the v8 SDK to v9

v9 of this SDK is a complete rewrite that includes the removal of dynamic dependencies, a new Mail Helper and support for .NET Standard 1.3.

Please begin at the [README](https://github.com/sendgrid/sendgrid-csharp).

<a name="missing"></a>
## Missing Classes

If you receive one of the following errors, or something similar:

* ‘Helpers’ does not exist in the namespace ‘SengrGrid’
* The type or namespace name ‘Mail’ could not be found

it means that you are probably not compiling your application to .NET 4.5.2. Currently, this is the only supported version of .NET by Twilio SendGrid.

The current solution is to download the code directly into your project and change the compile target to the desired version.

<a name="testing"></a>
## Testing v3 /mail/send Calls Directly

[Here](https://sendgrid.com/docs/for-developers/sending-email/curl-examples/) are some cURL examples for common use cases.

<a name="net45"></a>
## Using .NET 4.5.1 and lower

[Microsoft is no longer supporting these versions](https://blogs.msdn.microsoft.com/dotnet/2015/12/09/support-ending-for-the-net-framework-4-4-5-and-4-5-1/). We strongly advise upgrading your software to target .NET 4.5.2 or higher. If you are unable to do so, the current solution is to download the code directly into your project and change the compile target to the desired version.

<a name="package-manager"></a>
## Using the Package Manager

We upload this library to [Nuget](https://www.nuget.org/packages/SendGrid) whenever we make a release. This allows you to use [Nuget](https://www.nuget.org) for easy installation.

In most cases we recommend you download the latest version of the library, but if you need a different version, please use:

`PM> Install-Package Sendgrid -Version X.X.X`

<a name="versioning"></a>
## Versioning Convention

We follow the MAJOR.MINOR.PATCH versioning scheme as described by [SemVer.org](http://semver.org). Therefore, we recommend that you always pin (or vendor) the particular version you are working with your code and never auto-update to the latest version. Especially when there is a MAJOR point release since that is guaranteed to be a breaking change. Changes are documented in the [CHANGELOG](CHANGELOG.md) and [releases](https://github.com/sendgrid/sendgrid-csharp/releases) section.

<a name="request-body"></a>
## Viewing the Request Body

When debugging or testing, it may be useful to examine the raw request body to compare against the [documented format](https://sendgrid.com/docs/API_Reference/api_v3.html).

You can do this right before you call `var response = await client.SendEmailAsync(msg);` like so:

```csharp
Console.WriteLine(msg.Serialize());
```

<a name="ui-requests"></a>
## UI Requests are Failing / Deadlocks

If your UI based requests are failing, it may be due to a little known issue where the UI only has a single thread. The answer here is to use `ConfigureAwait(false)` on the end of your request call so that the thread does not reset back to request context and stays in capture context. Normally, async the request thread would "let go" of the capture context and reset to request context. With the UI, there is only a single thread, so you have to force the thread to switch to capture context, using `ConfigureAwait(false)`. For more information, please see a better summary that is linked to a longer article [in StackOverflow](https://stackoverflow.com/a/13494570).

In our example code, you would change:

```csharp
var response = await client.SendEmailAsync(msg);
```

to

```csharp
var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
```

If you are running a newer versions of .NET you can turn on a couple different Roslyn based analyzers that will trigger build errors if you're not calling `.ConfigureAwait(false)` on your async methods.

`Roslynator.Analyzers` can be installed through NuGet or as a VS plugin, but if you use the plugin then the analyzers won't run on build servers and trigger build errors as the NuGet package would. (thanks to [xt0rted](https://github.com/xt0rted) for this tip!)

<a name="signed-webhooks"></a>
## Signed Webhook Verification

Twilio SendGrid's Event Webhook will notify a URL via HTTP POST with information about events that occur as your mail is processed. [This](https://docs.sendgrid.com/for-developers/tracking-events/getting-started-event-webhook-security-features) article covers all you need to know to secure the Event Webhook, allowing you to verify that incoming requests originate from Twilio SendGrid. The sendgrid-csharp library can help you verify these Signed Event Webhooks.

You can find the end-to-end usage example and the tests [here](examples/eventwebhook). 
If you are still having trouble getting the validation to work, follow the following instructions:
- Be sure to use the *raw* payload for validation
- Be sure to include a trailing carriage return and newline in your payload
- In case of multi-event webhooks, make sure you include the trailing newline and carriage return after *each* event
