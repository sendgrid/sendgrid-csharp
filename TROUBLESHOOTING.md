﻿If you have a non-library SendGrid issue, please contact our [support team](https://support.sendgrid.com).

If you can't find a solution below, please open an [issue](https://github.com/sendgrid/sendgrid-csharp/issues).


## Table of Contents

* [Migrating from the v2 API to v3](#migrating)
* [Continue Using the v2 API](#v2)
* [Migrating from the v8 SDK to v9](#sdkmigration)
* [Testing v3 /mail/send Calls Directly](#testing)
* [Using .NET 4.5.1 and lower](#net45)
* [Missing Classes](#missing)
* [Error Messages](#error)
* [Versions](#versions)
* [Environment Variables and Your SendGrid API Key](#environment)
* [Using the Package Manager](#package-manager)

<a name="migrating"></a>
## Migrating from the v2 API to v3

Please review [our guide](https://sendgrid.com/docs/Classroom/Send/v3_Mail_Send/how_to_migrate_from_v2_to_v3_mail_send.html) on how to migrate from v2 to v3.

<a name="v2"></a>
## Continue Using the v2 API

[Here](https://github.com/sendgrid/sendgrid-csharp/tree/b27983a8f3d84a9d28972f2720cca0315ad9fe32) is the last working version with v2 support.

Using Nuget:

```bash
PM> Install-Package Sendgrid -Version 6.3.4
```

Download:

Click the "Clone or download" green button in [GitHub](https://github.com/sendgrid/sendgrid-csharp/tree/b27983a8f3d84a9d28972f2720cca0315ad9fe32) and choose download.

<a name="sdkmigration"></a>
## Migrating from the v8 SDK to v9

v9 of this SDK is a complete rewrite that includes the removal of dynamic dependencies, a new Mail Helper and support for .NET Standard 1.3.

Please begin at the [README](https://github.com/sendgrid/sendgrid-csharp) and if you need further assistance, please [create an issue on GitHub](https://github.com/sendgrid/sendgrid-csharp/issues).

<a name="net45"></a>
## Using .NET 4.5.1 and lower

[Microsoft is no longer supporting these versions](https://blogs.msdn.microsoft.com/dotnet/2015/12/09/support-ending-for-the-net-framework-4-4-5-and-4-5-1/). We strongly advise upgrading your software to target .NET 4.5.2 or higher. If you are unable to do so, the current solution is to download the code directly into your project and change the compile target to the desired version.

<a name="missing"></a>
## Missing Classes

If you receive one of the following errors, or something similar:

* ‘Helpers’ does not exist in the namespace ‘SengrGrid’
* The type or namespace name ‘Mail’ could not be found

it means that you are probably not compiling your application to .NET 4.5.2. Currently, this is the only supported version of .NET by SendGrid.

The current solution is to download the code directly into your project and change the compile target to the desired version.

<a name="testing"></a>
## Testing v3 /mail/send Calls Directly

[Here](https://sendgrid.com/docs/Classroom/Send/v3_Mail_Send/curl_examples.html) are some cURL examples for common use cases.

<a name="error"></a>
## Error Messages

To read the error message returned by SendGrid's API:

```csharp
var response = await client.RequestAsync(method: SendGridClient.Method.POST,
                                                 requestBody: msg.Serialize(),
                                                 urlPath: "mail/send");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result); // The message will be here
Console.WriteLine(response.Headers.ToString());
```

<a name="versions"></a>
## Versions

We follow the MAJOR.MINOR.PATCH versioning scheme as described by [SemVer.org](http://semver.org). Therefore, we recommend that you always pin (or vendor) the particular version you are working with to your code and never auto-update to the latest version. Especially when there is a MAJOR point release, since that is guarenteed to be a breaking change. Changes are documented in the [CHANGELOG](https://github.com/sendgrid/sendgrid-csharp/blob/master/CHANGELOG.md) and [releases](https://github.com/sendgrid/sendgrid-csharp/releases) section.

<a name="environment"></a>
## Environment Variables and Your SendGrid API Key

All of our examples assume you are using [environment variables](https://github.com/sendgrid/sendgrid-csharp#setup-environment-variables) to hold your SendGrid API key.

If you choose to add your SendGrid API key directly (not recommended):

`string apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");`

becomes

`string apiKey = "SENDGRID_API_KEY";`

In the first case SENDGRID_API_KEY is in reference to the name of the environment variable, while the second case references the actual SendGrid API Key.

<a name="package-manager"></a>
## Using the Package Manager

We upload this library to [Nuget](https://www.nuget.org/packages/SendGrid) whenever we make a release. This allows you to use [Nuget](https://www.nuget.org) for easy installation.

In most cases we recommend you download the latest version of the library, but if you need a different version, please use:

`PM> Install-Package Sendgrid -Version X.X.X`
