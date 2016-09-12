[![BuildStatus](https://travis-ci.org/sendgrid/sendgrid-csharp.png?branch=master)](https://travis-ci.org/sendgrid/sendgrid-csharp)

Please see our announcement regarding [breaking changes](https://github.com/sendgrid/sendgrid-csharp/issues/317). Your support is appreciated!

**This library allows you to quickly and easily use the SendGrid Web API v3 via C# with .NET.**

Version 7.X.X+ of this library provides full support for all SendGrid [Web API v3](https://sendgrid.com/docs/API_Reference/Web_API_v3/index.html) endpoints, including the new [v3 /mail/send](https://sendgrid.com/blog/introducing-v3mailsend-sendgrids-new-mail-endpoint).

This library represents the beginning of a new path for SendGrid. We want this library to be community driven and SendGrid led. We need your help to realize this goal. To help make sure we are building the right things in the right order, we ask that you create [issues](https://github.com/sendgrid/sendgrid-csharp/issues) and [pull requests](https://github.com/sendgrid/sendgrid-csharp/blob/master/CONTRIBUTING.md) or simply upvote or comment on existing issues or pull requests.

Please browse the rest of this README for further detail.

We appreciate your continued support, thank you!

# Table of Contents

* [Installation](#installation)
* [Quick Start](#quick_start)
* [Usage](#usage)
* [Use Cases](#use_cases)
* [Announcements](#announcements)
* [Roadmap](#roadmap)
* [How to Contribute](#contribute)
* [Troubleshooting](#troubleshooting)
* [About](#about)


<a name="installation"></a>
# Installation

## Prerequisites

- .NET version 4.5.2
- The SendGrid service, starting at the [free level](https://sendgrid.com/free?source=sendgrid-csharp)

## Setup Environment Variables

Update the development Environment (user space) with your [SENDGRID_API_KEY](https://app.sendgrid.com/settings/api_keys). For example, in Windows 10, please review [this thread](http://superuser.com/questions/949560/how-do-i-set-system-environment-variables-in-windows-10).

## Install Package

To use SendGrid in your C# project, you can either <a href="https://github.com/sendgrid/sendgrid-csharp.git">download the SendGrid C# .NET libraries directly from our Github repository</a> or, if you have the NuGet package manager installed, you can grab them automatically.

```
PM> Install-Package SendGrid
```

Once you have the SendGrid libraries properly referenced in your project, you can include calls to them in your code.
For a sample implementation, check the [Example](https://github.com/sendgrid/sendgrid-csharp/tree/master/SendGrid/Example) folder.

Add the following namespaces to use the library:
```csharp
using System;
using System.Web.Script.Serialization;
using SendGrid;
using SendGrid.Helpers.Mail; // Include if you want to use the Mail Helper
```

## Dependencies

- [SendGrid.CSharp.HTTP.Client](https://github.com/sendgrid/csharp-http-client)
- [Newtonsoft.Json](http://www.newtonsoft.com/json)

<a name="quick_start"></a>
# Quick Start

## Hello Email

The following is the minimum needed code to send an email with the [/mail/send Helper](https://github.com/sendgrid/sendgrid-csharp/tree/master/SendGrid/SendGrid/Helpers/Mail) ([here](https://github.com/sendgrid/sendgrid-csharp/blob/master/SendGrid/Example/Example.cs#L45) is a full example):

### With Mail Helper Class

```csharp
using System;
using SendGrid;
using SendGrid.Helpers.Mail;
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
            string apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY", EnvironmentVariableTarget.User);
            dynamic sg = new SendGridAPIClient(apiKey);

            Email from = new Email("test@example.com");
            string subject = "Hello World from the SendGrid CSharp Library!";
            Email to = new Email("test@example.com");
            Content content = new Content("text/plain", "Hello, Email!");
            Mail mail = new Mail(from, subject, to, content);

            dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());
        }
    }
}
```

The `Mail` constructor creates a [personalization object](https://sendgrid.com/docs/Classroom/Send/v3_Mail_Send/personalizations.html) for you. [Here](https://github.com/sendgrid/sendgrid-csharp/blob/master/SendGrid/Example/Example.cs#L33) is an example of how to add to it.

### Without Mail Helper Class

The following is the minimum needed code to send an email without the /mail/send Helper ([here](https://github.com/sendgrid/sendgrid-csharp/blob/master/examples/mail/mail.cs#L29) is a full example):

```csharp
using System;
using SendGrid;
using Newtonsoft.Json; // You can generate your JSON string yourelf or with another library if you prefer
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
            String apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY", EnvironmentVariableTarget.User);
            dynamic sg = new SendGridAPIClient(apiKey);

            string data = @"{
              'personalizations': [
                {
                  'to': [
                    {
                      'email': 'test@example.com'
                    }
                  ],
                  'subject': 'Hello World from the SendGrid C# Library!'
                }
              ],
              'from': {
                'email': 'test@example.com'
              },
              'content': [
                {
                  'type': 'text/plain',
                  'value': 'Hello, Email!'
                }
              ]
            }";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            dynamic response = await sg.client.mail.send.post(requestBody: json.ToString());
        }
    }
}
```

## General v3 Web API Usage (With Fluent Interface)

```csharp
using System;
using SendGrid;
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
            string apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY", EnvironmentVariableTarget.User);
            dynamic sg = new SendGrid.SendGridAPIClient(apiKey);
            dynamic response = await sg.client.suppression.bounces.get();
	    }
    }
}
```

## General v3 Web API Usage (Without Fluent Interface)

```csharp
using System;
using SendGrid;
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
                string apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY", EnvironmentVariableTarget.User);
                dynamic sg = new SendGrid.SendGridAPIClient(apiKey);
                dynamic response = await sg.client._("suppression/bounces").get();
        }
    }
}
```

<a name="usage"></a>
# Usage

- [SendGrid Docs](https://sendgrid.com/docs/API_Reference/Web_API_v3/index.html)
- [Library Usage Docs](https://github.com/sendgrid/sendgrid-csharp/tree/master/USAGE.md)
- [Example Code](https://github.com/sendgrid/sendgrid-csharp/tree/master/SendGrid/Example)
- [How-to: Migration from v2 to v3](https://sendgrid.com/docs/Classroom/Send/v3_Mail_Send/how_to_migrate_from_v2_to_v3_mail_send.html)
- [v3 Web API Mail Send Helper](https://github.com/sendgrid/sendgrid-csharp/tree/master/SendGrid/SendGrid/Helpers/Mail)

<a name="use_cases">
# Use Cases

[Examples of common API use cases](https://github.com/sendgrid/sendgrid-csharp/blob/master/USE_CASES.md), such as how to send an email with a transactional template.

<a name="announcements"></a>
# Announcements

Please see our announcement regarding [breaking changes](https://github.com/sendgrid/sendgrid-csharp/issues/317). Your support is appreciated!

All updates to this library is documented in our [CHANGELOG](https://github.com/sendgrid/sendgrid-csharp/blob/master/CHANGELOG.md) and [releases](https://github.com/sendgrid/sendgrid-csharp/releases).

<a name="roadmap"></a>
# Roadmap

If you are interested in the future direction of this project, please take a look at our open [issues](https://github.com/sendgrid/sendgrid-csharp/issues) and [pull requests](https://github.com/sendgrid/sendgrid-csharp/pulls). We would love to hear your feedback.

<a name="contribute"></a>
# How to Contribute

We encourage contribution to our library (you might even score some nifty swag), please see our [CONTRIBUTING](https://github.com/sendgrid/sendgrid-csharp/tree/master/CONTRIBUTING.md) guide for details.

Quick links:

- [Feature Request](https://github.com/sendgrid/sendgrid-csharp/tree/master/CONTRIBUTING.md#feature_request)
- [Bug Reports](https://github.com/sendgrid/sendgrid-csharp/tree/master/CONTRIBUTING.md#submit_a_bug_report)
- [Sign the CLA to Create a Pull Request](https://github.com/sendgrid/sendgrid-csharp/tree/master/CONTRIBUTING.md#cla)
- [Improvements to the Codebase](https://github.com/sendgrid/sendgrid-csharp/tree/master/CONTRIBUTING.md#improvements_to_the_codebase)

<a name="troubleshooting"></a>
# Troubleshooting

Please see our [troubleshooting guide](https://github.com/sendgrid/sendgrid-csharp/blob/master/TROUBLESHOOTING.md) for common library issues.

<a name="about"></a>
# About

sendgrid-csharp is guided and supported by the SendGrid [Developer Experience Team](mailto:dx@sendgrid.com).

sendgrid-csharp is maintained and funded by SendGrid, Inc. The names and logos for sendgrid-csharp are trademarks of SendGrid, Inc.

![SendGrid Logo]
(https://uiux.s3.amazonaws.com/2016-logos/email-logo%402x.png)
