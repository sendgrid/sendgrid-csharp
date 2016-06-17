[![BuildStatus](https://travis-ci.org/sendgrid/sendgrid-csharp.png?branch=master)](https://travis-ci.org/sendgrid/sendgrid-csharp)

**This library allows you to quickly and easily use the SendGrid Web API via C Sharp with .NET.**

# Announcements

**BREAKING CHANGE as of 2016.06.14**

Version `7.0.0` is a breaking change for the entire library.

Version 7.0.0 brings you full support for all Web API v3 endpoints. We
have the following resources to get you started quickly:

-   [SendGrid
    Documentation](https://sendgrid.com/docs/API_Reference/Web_API_v3/index.html)
-   [Usage
    Documentation](https://github.com/sendgrid/sendgrid-csharp/tree/master/USAGE.md)
-   [Example
    Code](https://github.com/sendgrid/sendgrid-csharp/tree/master/Example)

Thank you for your continued support!

All updates to this library is documented in our [CHANGELOG](https://github.com/sendgrid/sendgrid-csharp/blob/master/CHANGELOG.md).

# Installation

## Setup Environment Variables

First, get your free SendGrid account [here](https://sendgrid.com/free?source=sendgrid-csharp).

Next, update your Environment (user space) with your [SENDGRID_API_KEY](https://app.sendgrid.com/settings/api_keys).

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

- The SendGrid Service, starting at the [free level](https://sendgrid.com/free?source=sendgrid-csharp))
- [SendGrid.CSharp.HTTP.Client](https://github.com/sendgrid/csharp-http-client)

# Quick Start

## Hello Email

```csharp
using System;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Example
{
    internal class Example
    {
        private static void Main()
        {
            String apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY", EnvironmentVariableTarget.User);
            dynamic sg = new SendGridAPIClient(apiKey);

            Email from = new Email("test@example.com");
            String subject = "Hello World from the SendGrid CSharp Library";
            Email to = new Email("test@example.com");
            Content content = new Content("text/plain", "Textual content");
            Mail mail = new Mail(from, subject, to, content);

            dynamic response = sg.client.mail.send.post(requestBody: mail.Get());
        }
    }
}
```

## General v3 Web API Usage

```csharp
using System;

namespace Example
{
    internal class Example
    {
        private static void Main()
        {
            String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            dynamic sg = new SendGrid.SendGridAPIClient(apiKey);
            dynamic response = sg.client.api_keys.get();
        }
    }
}
```

# Usage

- [SendGrid Docs](https://sendgrid.com/docs/API_Reference/Web_API_v3/index.html)
- [Usage Docs](https://github.com/sendgrid/sendgrid-csharp/tree/master/USAGE.md)
- [Example Code](https://github.com/sendgrid/sendgrid-csharp/tree/master/SendGrid/Example)
- [v3 Web API Mail Send Helper](https://github.com/sendgrid/sendgrid-csharp/tree/master/SendGrid/SendGrid/Helpers/Mail)

## Roadmap

If you are intersted in the future direction of this project, please take a look at our [milestones](https://github.com/sendgrid/sendgrid-csharp/milestones). We would love to hear your feedback.

## How to Contribute

We encourage contribution to our library, please see our [CONTRIBUTING](https://github.com/sendgrid/sendgrid-csharp/tree/master/CONTRIBUTING.md) guide for details.

Quick links:

- [Feature Request](https://github.com/sendgrid/sendgrid-csharp/tree/master/CONTRIBUTING.md#feature_request)
- [Bug Reports](https://github.com/sendgrid/sendgrid-csharp/tree/master/CONTRIBUTING.md#submit_a_bug_report)
- [Sign the CLA to Create a Pull Request](https://github.com/sendgrid/sendgrid-csharp/tree/master/CONTRIBUTING.md#cla)
- [Improvements to the Codebase](https://github.com/sendgrid/sendgrid-csharp/tree/master/CONTRIBUTING.md#improvements_to_the_codebase)

# About

sendgrid-csharp is guided and supported by the SendGrid [Developer Experience Team](mailto:dx@sendgrid.com).

sendgrid-csharp is maintained and funded by SendGrid, Inc. The names and logos for sendgrid-csharp are trademarks of SendGrid, Inc.

![SendGrid Logo]
(https://uiux.s3.amazonaws.com/2016-logos/email-logo%402x.png)
