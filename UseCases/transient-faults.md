
<a name="transient-faults"></a>
# Transient Fault Handling

The SendGridClient provides functionality for handling transient errors that might occur when sending an HttpRequest. This includes client side timeouts while sending the mail, or certain errors returned within the 500 range. Errors within the 500 range are limited to 500 Internal Server Error, 502 Bad Gateway, 503 Service unavailable and 504 Gateway timeout.

By default, retry behaviour is off, you must explicitly enable it by setting the retry count to a value greater than zero. To set the retry count, you must use the SendGridClient construct that takes a **SendGridClientOptions** object, allowing you to configure the **ReliabilitySettings**

### RetryCount

The amount of times to retry the operation before reporting an exception to the caller. This is in addition to the initial attempt so setting a value of 1 would result in 2 attempts, the initial attempt and the retry. Defaults to zero, retry behaviour is not enabled. The maximum amount of retries permitted is 5. 

### MinimumBackOff

The minimum amount of time to wait between retries. 

### MaximumBackOff

The maximum possible amount of time to wait between retries. The maximum value allowed is 30 seconds

### DeltaBackOff

The value that will be used to calculate a random delta in the exponential delay between retries.  A random element of time is factored into the delta calculation as this helps avoid many clients retrying at regular intervals.


## Examples

In this example we are setting RetryCount to 2, with a minimum wait time of 1 seconds, a maximum of 10 seconds and a delta of 3 seconds

```csharp

var options = new SendGridClientOptions
{
    ApiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY"),
    ReliabilitySettings = new ReliabilitySettings(2, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(3))
};

var client = new SendGridClient(options);

```

The SendGridClientOptions object defines all the settings that can be set for the client, e.g.

```csharp

var options = new SendGridClientOptions
{
    ApiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY"),
    ReliabilitySettings = new ReliabilitySettings(2, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(3)),
    Host = "Your-Host",
    UrlPath = "Url-Path",
    Version = "3",
    RequestHeaders = new Dictionary<string, string>() {{"header-key", "header-value"}}
};

var client = new SendGridClient(options);

```
