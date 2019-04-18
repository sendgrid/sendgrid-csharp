![SendGrid Logo](https://github.com/sendgrid/sendgrid-python/raw/master/twilio_sendgrid_logo.png)

[![BuildStatus](https://travis-ci.org/sendgrid/sendgrid-csharp.png?branch=master)](https://travis-ci.org/sendgrid/sendgrid-csharp)
[![NuGet](https://img.shields.io/nuget/v/SendGrid.svg)](https://www.nuget.org/packages/SendGrid)
[![Email Notifications Badge](https://dx.sendgrid.com/badge/csharp)](https://dx.sendgrid.com/newsletter/csharp)
[![MIT licensed](https://img.shields.io/badge/license-MIT-blue.svg)](./LICENSE.txt)
[![Twitter Follow](https://img.shields.io/twitter/follow/sendgrid.svg?style=social&label=Follow)](https://twitter.com/sendgrid)
[![GitHub contributors](https://img.shields.io/github/contributors/sendgrid/sendgrid-csharp.svg)](https://github.com/sendgrid/sendgrid-csharp/graphs/contributors)
[![Open Source Helpers](https://www.codetriage.com/sendgrid/sendgrid-csharp/badges/users.svg)](https://www.codetriage.com/sendgrid/sendgrid-csharp)

<a name="announcements"></a>
# Announcements

* Subscribe to email [notifications](https://dx.sendgrid.com/newsletter/csharp) for releases and breaking changes.
* Send SMS messages with [Twilio](https://github.com/sendgrid/sendgrid-csharp/blob/master/USE_CASES.md#sms). 
* If you're a software engineer who is passionate about #DeveloperExperience and/or #OpenSource, this is an incredible opportunity to join our #DX team as a Developer Experience Engineer and work with @thinkingserious and @aroach! Tell your friends :)

# Overview

**This library allows you to quickly and easily use the Twilio SendGrid Web API v3 via C# with .NET.**

Version 9.X.X+ of this library provides full support for all Twilio SendGrid [Web API v3](https://sendgrid.com/docs/API_Reference/api_v3.html) endpoints, including the new [v3 /mail/send](https://sendgrid.com/blog/introducing-v3mailsend-sendgrids-new-mail-endpoint).

We want this library to be community driven, and Twilio SendGrid led. We need your help to realize this goal. To help make sure we are building the right things in the right order, we ask that you create [issues](https://github.com/sendgrid/sendgrid-csharp/issues) and [pull requests](https://github.com/sendgrid/sendgrid-csharp/blob/master/CONTRIBUTING.md) or simply upvote or comment on existing issues or pull requests.

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
- A Twilio SendGrid account, [sign up for free](https://sendgrid.com/free?source=sendgrid-csharp) to send up to 40,000 emails for the first 30 days, then send 100 emails/day free forever or check out [our pricing](https://sendgrid.com/pricing?source=sendgrid-csharp).

## Obtain an API Key

Grab your API Key from the [Twilio SendGrid UI](https://app.sendgrid.com/settings/api_keys).

## Setup Environment Variables to Manage Your API Key

Manage your [Twilio SendGrid API Keys](https://app.sendgrid.com/settings/api_keys) by storing them in Environment Variables or in [Web.config](https://docs.microsoft.com/en-us/aspnet/identity/overview/features-api/best-practices-for-deploying-passwords-and-other-sensitive-data-to-aspnet-and-azure). It is a good practice to keep your data and configuration settings separate. This way to you can change your Twilio SendGrid API key without changing your code. Also, we strongly advise against storing sensitive data directly in your code.

Setup Environment Variables using the UI:
1. Press Win+R and run SystemPropertiesAdvanced
2. Click on Environment Variables
3. Click New in user variables section
4. Type SENDGRID_API_KEY in the name. (Make sure this name matches the name of key in your code)
5. Type actual API Key in the value
6. Restart the IDE and you're done!

Setup Environment Variables using CMD:
1. Run CMD as administrator
2. setx SENDGRID_API_KEY "YOUR_API_KEY"

Here are few example to get and set API Keys programatically:

Get Environment Variable
```csharp
var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
```

Set Environment Variable
```csharp
var setKey = Environment.SetEnvironmentVariable("SENDGRID_API_KEY", "YOUR_API_KEY");
```

## Install Package

To use Twilio SendGrid in your C# project, you can either <a href="https://github.com/sendgrid/sendgrid-csharp.git">download the Twilio SendGrid C# .NET libraries directly from our Github repository</a> or if you have the NuGet package manager installed, you can grab them automatically:

```
PM> Install-Package SendGrid
```

Once you have the Twilio SendGrid library installed, you can include calls to them in your code.
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

After executing the above code, `response.StatusCode` should be `202` and you should have an email in the inbox of the to recipient. You can check the status of your email [in the UI](https://app.sendgrid.com/email_activity?). Alternatively, we can post events to a URL of your choice using our [Event Webhook](https://sendgrid.com/docs/API_Reference/Webhooks/event.html). This gives you data about the events that occur as Twilio SendGrid processes your email.

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
                Subject = "Sending with Twilio SendGrid is Fun",
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

- [Twilio SendGrid Docs](https://sendgrid.com/docs/API_Reference/api_v3.html)
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
- [Improvements to the Codebase](https://github.com/sendgrid/sendgrid-csharp/tree/master/CONTRIBUTING.md#improvements-to-the-codebase)
- [Review Pull Requests](https://github.com/sendgrid/sendgrid-csharp/tree/master/CONTRIBUTING.md#code-reviews)

<a name="troubleshooting"></a>
# Troubleshooting

Please see our [troubleshooting guide](https://github.com/sendgrid/sendgrid-csharp/blob/master/TROUBLESHOOTING.md) for common library issues.

<a name="about"></a>
# About

sendgrid-csharp is guided and supported by the Twilio Developer Experience Team.

Email the Developer Experience Team [here](mailto:dx@sendgrid.com) in case of any assistance or queries.


sendgrid-csharp is maintained and funded by Twilio SendGrid, Inc. The names and logos for sendgrid-csharp are trademarks of Twilio SendGrid, Inc.

# License
[The MIT License (MIT)](LICENSE.txt)
