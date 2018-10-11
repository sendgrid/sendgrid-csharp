**This helper allows you to get IP addresses from your account through the SendGrid v3 API.**

## How to get a list of unassigned IPs

This is how you can use the helper to get a list of unassigned IPs from your account.

```csharp
using SendGrid;
using SendGrid.Helpers.IpAddresses;
using System;
using System.Threading.Tasks;
using System.IO;

namespace Example
{
    internal class Example
    {
        private static void Main()
        {
            ExecuteManualAttachmentAdd().Wait();
        }

        static async Task ExecuteGetUnassignedIps()
        {
            var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var helper = new IpAddressHelper(client);
            var ips = await helper.GetUnassignedIpsAsync();
        }
    }
}
```
