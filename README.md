[![BuildStatus](https://travis-ci.org/sendgrid/sendgrid-csharp.png?branch=v3beta)](https://travis-ci.org/sendgrid/sendgrid-csharp)

**This library allows you to quickly and easily use the SendGrid Web API via C Sharp with .NET.**

# Announcements

**NOTE: The `/mail/send/beta` endpoint is currently in beta!

Since this is not a general release, we do not recommend POSTing production level traffic through this endpoint or integrating your production servers with this endpoint.

When this endpoint is ready for general release, your code will require an update in order to use the official URI.

By using this endpoint, you accept that you may encounter bugs and that the endpoint may be taken down for maintenance at any time. We cannot guarantee the continued availability of this beta endpoint. We hope that you like this new endpoint and we appreciate any [feedback](dx+mail-beta@sendgrid.com) that you can send our way.**

**BREAKING CHANGE as of XXXX.XX.XX**

Version `7.0.0` is a breaking change for the entire library.

Version 7.0.0 brings you full support for all Web API v3 endpoints. We
have the following resources to get you started quickly:

-   [SendGrid
    Documentation](https://sendgrid.com/docs/API_Reference/Web_API_v3/index.html)
-   [Usage
    Documentation](https://github.com/sendgrid/sendgrid-csharp/tree/v3beta/USAGE.md)
-   [Example
    Code](https://github.com/sendgrid/sendgrid-csharp/tree/v3beta/Example)

Thank you for your continued support!

All updates to this library is documented in our [CHANGELOG](https://github.com/sendgrid/sendgrid-csharp/blob/v3beta/CHANGELOG.md).

# Installation

## Environment Variables

First, get your free SendGrid account [here](https://sendgrid.com/free?source=sendgrid-csharp).

Next, update your Environment (user space) with your [SENDGRID_API_KEY](https://app.sendgrid.com/settings/api_keys).

## TRYING OUT THE V3 BETA MAIL SEND

* Check out the v3beta branch from `https://github.com/sendgrid/sendgrid-csharp.git` using your favorite Git client.
* Open the [solution](https://github.com/sendgrid/sendgrid-csharp/blob/v3beta/SendGrid/SendGrid.sln) in Visual Studio (we have tested with the Community Edition).
* Update the to and from [emails](https://github.com/sendgrid/sendgrid-csharp/blob/v3beta/SendGrid/Example/Example.cs#L26).
* Build the Solution.
* Build the Example project and click `Start`.
* Check out the documentation for [Web API v3 /mail/send/beta endpoint](https://sendgrid.com/docs/API_Reference/Web_API_v3/Mail/index.html).

## TRYING OUT THE V3 BETA WEB API

* Check out the v3beta branch from `https://github.com/sendgrid/sendgrid-csharp.git` using your favorite Git client.
* Open the [solution](https://github.com/sendgrid/sendgrid-csharp/blob/v3beta/SendGrid/SendGrid.sln) in Visual Studio (we have tested with the Community Edition).
* Check out the documentation for [Web API v3 endpoints](https://sendgrid.com/docs/API_Reference/Web_API_v3/index.html).
* Review the corresponding [examples](https://github.com/sendgrid/sendgrid-csharp/blob/v3beta/examples).
* You can add your test code to our [Example project](https://github.com/sendgrid/sendgrid-csharp/blob/v3beta/SendGrid/Example/Example.cs).

## Once we are out of v3 BETA, the following will apply

To use SendGrid in your C# project, you can either <a href="https://github.com/sendgrid/sendgrid-csharp.git">download the SendGrid C# .NET libraries directly from our Github repository</a> or, if you have the NuGet package manager installed, you can grab them automatically.

```
PM> Install-Package SendGrid
```

Once you have the SendGrid libraries properly referenced in your project, you can include calls to them in your code.
For a sample implementation, check the [Example](https://github.com/sendgrid/sendgrid-csharp/tree/v3beta/SendGrid/Example) folder.

Add the following namespaces to use the library:
```csharp
using System;
using System.Web.Script.Serialization;
using SendGrid;
```

## Dependencies

- The SendGrid Service, starting at the [free level](https://sendgrid.com/free?source=sendgrid-csharp))
- [SendGrid.CSharp.HTTP.Client](https://github.com/sendgrid/csharp-http-client)

# Quick Start

## Hello Email

```csharp
using System;
using SendGrid;

namespace Example
{
    internal class Example
    {
        private static void Main()
        {
	    String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            dynamic sg = new SendGrid.SendGridAPIClient(apiKey);

            Email from = new Email("test@example.com");
            String subject = "Hello World from the SendGrid CSharp Library";
            Email to = new Email("test@example.com");
            Content content = new Content("text/plain", "Textual content");
            Mail mail = new Mail(from, subject, to, content);

            dynamic response = sg.client.mail.send.beta.post(requestBody: mail.Get());
        }
    }
}
```

## General v3 Web API Usage

```csharp
using System;
using SendGrid;

namespace Example
{
    internal class Example
    {
        private static void Main()
        {
            String apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            dynamic sg = new SendGrid.SendGridAPIClient(apiKey);
            dynamic response = sg.client.api_keys.get(queryParams: queryParams);
        }
    }
}
```

# Usage

- [SendGrid Docs](https://sendgrid.com/docs/API_Reference/Web_API_v3/index.html)
- [Usage Docs](https://github.com/sendgrid/sendgrid-csharp/tree/v3beta/USAGE.md)
- [Example Code](https://github.com/sendgrid/sendgrid-csharp/tree/v3beta/SendGrid/Example)
- [v3 Web API Mail Send Helper](https://github.com/sendgrid/sendgrid-csharp/tree/v3beta/SendGrid/SendGrid/Helpers/Mail)

## Roadmap

If you are intersted in the future direction of this project, please take a look at our [milestones](https://github.com/sendgrid/sendgrid-csharp/milestones). We would love to hear your feedback.

## How to Contribute

We encourage contribution to our library, please see our [CONTRIBUTING](https://github.com/sendgrid/sendgrid-csharp/tree/v3beta/CONTRIBUTING.md) guide for details.

Quick links:

- [Feature Request](https://github.com/sendgrid/sendgrid-csharp/tree/v3beta/CONTRIBUTING.md#feature_request)
- [Bug Reports](https://github.com/sendgrid/sendgrid-csharp/tree/v3beta/CONTRIBUTING.md#submit_a_bug_report)
- [Sign the CLA to Create a Pull Request](https://github.com/sendgrid/sendgrid-csharp/tree/v3beta/CONTRIBUTING.md#cla)
- [Improvements to the Codebase](https://github.com/sendgrid/sendgrid-csharp/tree/v3beta/CONTRIBUTING.md#improvements_to_the_codebase)

# About

sendgrid-csharp is guided and supported by the SendGrid [Developer Experience Team](mailto:dx@sendgrid.com).

sendgrid-csharp is maintained and funded by SendGrid, Inc. The names and logos for sendgrid-csharp are trademarks of SendGrid, Inc.

![SendGrid Logo]
(https://assets3.sendgrid.com/mkt/assets/logos_brands/small/sglogo_2015_blue-9c87423c2ff2ff393ebce1ab3bd018a4.png)
