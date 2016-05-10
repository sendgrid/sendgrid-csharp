[![BuildStatus](https://travis-ci.org/sendgrid/sendgrid-csharp.png?branch=master)](https://travis-ci.org/sendgrid/sendgrid-csharp)

**This library allows you to quickly and easily use the SendGrid Web API via C Sharp with .NET.**

**NOTE: The `/mail/send/beta` endpoint is currently in beta!

Since this is not a general release, we do not recommend POSTing production level traffic through this endpoint or integrating your production servers with this endpoint. 

When this endpoint is ready for general release, your code will require an update in order to use the official URI.

By using this endpoint, you accept that you may encounter bugs and that the endpoint may be taken down for maintenance at any time. We cannot guarantee the continued availability of this beta endpoint. We hope that you like this new endpoint and we appreciate any [feedback](dx+mail-beta@sendgrid.com) that you can send our way.**

# Installation

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
```

## Dependencies

- [SendGrid.CSharp.HTTP.Client](https://github.com/sendgrid/csharp-http-client)

## Environment Variables 

First, get your free SendGrid account [here](https://sendgrid.com/free?source=sendgrid-csharp).

Next, update your environment with your [SENDGRID_API_KEY](https://app.sendgrid.com/settings/api_keys).

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

            Email from = new Email("dx@sendgrid.com");
            String subject = "Hello World from the SendGrid CSharp Library";
            Email to = new Email("elmer.thomas@sendgrid.com");
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

# Announcements

**BREAKING CHANGE as of XXXX.XX.XX**

Version `7.0.0` is a breaking change for the entire library.

Version 7.0.0 brings you full support for all Web API v3 endpoints. We
have the following resources to get you started quickly:

-   [SendGrid
    Documentation](https://sendgrid.com/docs/API_Reference/Web_API_v3/index.html)
-   [Usage
    Documentation](https://github.com/sendgrid/sendgrid-csharp/blob/master/USAGE.md)
-   [Example
    Code](https://github.com/sendgrid/sendgrid-csharp/blob/master/Example)

Thank you for your continued support!

## Roadmap

[Milestones](https://github.com/sendgrid/sendgrid-csharp/milestones)

## How to Contribute

We encourage contribution to our libraries, please see our [CONTRIBUTING](https://github.com/sendgrid/sendgrid-csharp/blob/master/CONTRIBUTING.md) guide for details.

* [Feature Request](https://github.com/sendgrid/sendgrid-csharp/blob/master/CONTRIBUTING.md#feature_request)
* [Bug Reports](https://github.com/sendgrid/sendgrid-csharp/blob/master/CONTRIBUTING.md#submit_a_bug_report)
* [Improvements to the Codebase](https://github.com/sendgrid/sendgrid-csharp/blob/master/CONTRIBUTING.md#improvements_to_the_codebase)

## Usage

- [SendGrid Docs](https://sendgrid.com/docs/API_Reference/index.html)
- [v3 Web API](https://github.com/sendgrid/sendgrid-csharp/blob/master/USAGE.md)
- [Example Code](https://github.com/sendgrid/sendgrid-csharp/blob/master/examples)
- [v3 Web API Mail Send Helper]()

## Unsupported Libraries

- [Official and Unsupported SendGrid Libraries](https://sendgrid.com/docs/Integrate/libraries.html)

# About

![SendGrid Logo]
(https://assets3.sendgrid.com/mkt/assets/logos_brands/small/sglogo_2015_blue-9c87423c2ff2ff393ebce1ab3bd018a4.png)

sendgrid-csharp is guided and supported by the SendGrid [Developer Experience Team](mailto:dx@sendgrid.com).

sendgrid-csharp is maintained and funded by SendGrid, Inc. The names and logos for sendgrid-csharp are trademarks of SendGrid, Inc.

