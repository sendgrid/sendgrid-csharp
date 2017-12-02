![SendGrid Logo](https://uiux.s3.amazonaws.com/2016-logos/email-logo%402x.png)

[![BuildStatus](https://travis-ci.org/sendgrid/sendgrid-csharp.png?branch=master)](https://travis-ci.org/sendgrid/sendgrid-csharp)
[![NuGet](https://img.shields.io/nuget/v/SendGrid.svg)](https://www.nuget.org/packages/SendGrid)
[![Email Notifications Badge](https://dx.sendgrid.com/badge/csharp)](https://dx.sendgrid.com/newsletter/csharp)
[![MIT licensed](https://img.shields.io/badge/license-MIT-blue.svg)](./LICENSE.txt)
[![Twitter Follow](https://img.shields.io/twitter/follow/sendgrid.svg?style=social&label=Follow)](https://twitter.com/sendgrid)
[![GitHub contributors](https://img.shields.io/github/contributors/sendgrid/sendgrid-csharp.svg)](https://github.com/sendgrid/sendgrid-csharp/graphs/contributors)

<a name="announcements"></a>
# Announcements

## August 2017

Subscribe to email [notifications](https://dx.sendgrid.com/newsletter/csharp) for releases and breaking changes.


# Overview

**This library allows you to quickly and easily use the SendGrid Web API v3 via C# with .NET.**

Version 9.X.X+ of this library provides full support for all SendGrid [Web API v3](https://sendgrid.com/docs/API_Reference/api_v3.html) endpoints, including the new [v3 /mail/send](https://sendgrid.com/blog/introducing-v3mailsend-sendgrids-new-mail-endpoint).

We want this library to be community driven, and SendGrid led. We need your help to realize this goal. To help make sure we are building the right things in the right order, we ask that you create [issues](https://github.com/sendgrid/sendgrid-csharp/issues) and [pull requests](https://github.com/sendgrid/sendgrid-csharp/blob/master/CONTRIBUTING.md) or simply upvote or comment on existing issues or pull requests.

For updates to this library, see our [CHANGELOG](https://github.com/sendgrid/sendgrid-csharp/blob/master/CHANGELOG.md) and [releases](https://github.com/sendgrid/sendgrid-csharp/releases). 

Subscribe to email [release notifications](https://dx.sendgrid.com/newsletter/csharp) to receive emails about releases and breaking changes.

We appreciate your continued support, thank you!

# Table of Contents

* [Installation](#installation)
* [Quick Start](#quick-start)
* [Usage](#usage)
* [Use Cases](#use-cases)
* [Announcements](#announcements)
* [Roadmap](#roadmap)
* [How to Contribute](#contribute)
* [Troubleshooting](#troubleshooting)
* [About](#about)
* [License](#license)


<a name="installation"></a>
# Installation

## Prerequisites

- .NET version 4.5.2 and higher
- .NET Core 1.0 and higher
- .NET Standard 1.3 support
- A SendGrid account, [sign up for free](https://sendgrid.com/free?source=sendgrid-csharp) to send up to 40,000 emails for the first 30 days or check out [our pricing](https://sendgrid.com/pricing?source=sendgrid-csharp).

## Obtain an API Key

Grab your API Key from the [SendGrid UI](https://app.sendgrid.com/settings/api_keys).

## Setup Environment Variables to Manage Your API Key

Do not hard code your [SendGrid API Key](https://app.sendgrid.com/settings/api_keys) into your code. Instead, use something like an [environment variable](http://superuser.com/questions/949560/how-do-i-set-system-environment-variables-in-windows-10) or [Web.config](https://docs.microsoft.com/en-us/aspnet/identity/overview/features-api/best-practices-for-deploying-passwords-and-other-sensitive-data-to-aspnet-and-azure).

The examples found below in this quick start and in the example projects all make use of storing their API keys as an environment variable.

Environment variables are usually part of the OS and are available to programs within that same OS.
It gives the option to bypass hardcoding credentials, making them easy to manage.

The following example will go through adding an environment variable in Windows 10.

Looking at the code example from the [Quick Start](#quick_start), we see that the second line of the `Execute()` method tries to retrieve an environment variable like so:

```csharp
var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
```

`NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY` isn't a variable of your OS environment yet, so let's add it.

Press `win + R` (or search for "run"), fill in "SystemPropertiesAdvanced", press `enter` and then click "Environment variables".

You'll get two lists of variables, but we'll focus on the User Variables for this example.

Click the "New" button beneath the User Variables list. Give it the name `NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY` (or rename the variable in both the environment and code to a more generic name).

For value, insert your SendGrid API key. Click "Ok", and once again in the Variable Overview and lastly, close the system properties window.

For other Windows version, you can set an environment variable like this:

```csharp
var setKey = Environment.SetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY", value);
```

For example,

```csharp
var setKey = Environment.SetEnvironmentVariable("SENDGRID_API_KEY", YOUR_API_KEY);
```

You may need to restart your IDE to make use of the new variable.

Now, if all went well, you can access the newly added variable in your C# SendGrid projects! Ready for some examples?

## Install Package

To use SendGrid in your C# project, you can either <a href="https://github.com/sendgrid/sendgrid-csharp.git">download the SendGrid C# .NET libraries directly from our Github repository</a> or if you have the NuGet package manager installed, you can grab them automatically:

```
PM> Install-Package SendGrid
```

Once you have the SendGrid library installed, you can include calls to them in your code.
For sample implementations, see the [.NET Core Example](https://github.com/sendgrid/sendgrid-csharp/tree/master/ExampleCoreProject) and the [.NET 4.5.2 Example](https://github.com/sendgrid/sendgrid-csharp/tree/master/ExampleNet45Project) folders.

## Dependencies

- Please see the [.nuspec file](https://github.com/sendgrid/sendgrid-csharp/tree/master/nuspec).

<a name="quick-start"></a>
# Quick Start

## Hello Email

The following is the minimum needed code to send an simple email. Use this example, and modify the `apiKey`, `from` and `to` variables:

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

After executing the above code, `response.StatusCode` should be `202` and you should have an email in the inbox of the to recipient. You can check the status of your email [in the UI](https://app.sendgrid.com/email_activity?). Alternatively, we can post events to a URL of your choice using our [Event Webhook](https://sendgrid.com/docs/API_Reference/Webhooks/event.html). This gives you data about the events that occur as SendGrid processes your email.

For more advanced cases, you can build the SendGridMessage object yourself with these minimum required settings:

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
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("test@example.com", "DX Team"),
                Subject = "Sending with SendGrid is Fun",
                PlainTextContent = "and easy to do anywhere, even with C#",
                HtmlContent = "<strong>and easy to do anywhere, even with C#</strong>"
            };
            msg.AddTo(new EmailAddress("test@example.com", "Test User"));
            var response = await client.SendEmailAsync(msg);
        }
    }
}
```

You can find an example of all of the email features [here](https://github.com/sendgrid/sendgrid-csharp/blob/master/tests/SendGrid.Tests/Integration.cs#L79).

## General v3 Web API Usage

```csharp
using System;
using System.Threading.Tasks;
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
            var queryParams = @"{
                'limit': 100
            }";
            var response = await client.RequestAsync(method: SendGridClient.Method.GET,
                                                     urlPath: "suppression/bounces",
                                                     queryParams: queryParams);
	    }
    }
}
```
## Web Proxy

```csharp
var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
var proxy = new WebProxy("http://proxy:1337");
var client = new SendGridClient(proxy, apiKey);
```

<a name="usage"></a>
# Usage

- [SendGrid Docs](https://sendgrid.com/docs/API_Reference/api_v3.html)
- [Library Usage Docs](https://github.com/sendgrid/sendgrid-csharp/tree/master/USAGE.md)
- [Example Code - .NET Core](https://github.com/sendgrid/sendgrid-csharp/tree/master/ExampleCoreProject)
- [Example Code - .NET 4.5.2+](https://github.com/sendgrid/sendgrid-csharp/tree/master/ExampleNet45Project)
- [How-to: Migration from v2 to v3](https://sendgrid.com/docs/Classroom/Send/v3_Mail_Send/how_to_migrate_from_v2_to_v3_mail_send.html)
- [v3 Web API Mail Send Helper](https://github.com/sendgrid/sendgrid-csharp/tree/master/src/SendGrid/Helpers/Mail)

<a name="use-cases"></a>
# Use Cases

Here are some [examples of common API use cases](https://github.com/sendgrid/sendgrid-csharp/blob/master/USE_CASES.md), such as how to send an email with a transactional template.

<a name="roadmap"></a>
# Roadmap

If you are interested in the future direction of this project, please take a look at our open [issues](https://github.com/sendgrid/sendgrid-csharp/issues) and [pull requests](https://github.com/sendgrid/sendgrid-csharp/pulls). We would love to hear your feedback!

<a name="contribute"></a>
# How to Contribute

We encourage contribution to our library (you might even score some nifty swag), please see our [CONTRIBUTING](https://github.com/sendgrid/sendgrid-csharp/tree/master/CONTRIBUTING.md) guide for details.

Quick links:

- [Feature Request](https://github.com/sendgrid/sendgrid-csharp/tree/master/CONTRIBUTING.md#feature-request)
- [Bug Reports](https://github.com/sendgrid/sendgrid-csharp/tree/master/CONTRIBUTING.md#submit-a-bug-report)
- [Sign the CLA to Create a Pull Request](https://github.com/sendgrid/sendgrid-csharp/tree/master/CONTRIBUTING.md#cla)
- [Improvements to the Codebase](https://github.com/sendgrid/sendgrid-csharp/tree/master/CONTRIBUTING.md#improvements-to-the-codebase)

<a name="troubleshooting"></a>
# Troubleshooting

Please see our [troubleshooting guide](https://github.com/sendgrid/sendgrid-csharp/blob/master/TROUBLESHOOTING.md) for common library issues.

<a name="about"></a>
# About

sendgrid-csharp is guided and supported by the SendGrid [Developer Experience Team](mailto:dx@sendgrid.com).

sendgrid-csharp is maintained and funded by SendGrid, Inc. The names and logos for sendgrid-csharp are trademarks of SendGrid, Inc.

# License
[The MIT License (MIT)](LICENSE.txt)
