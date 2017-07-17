namespace SendGrid.Reliability
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using Polly;
    using Polly.Retry;
    
    public class RetryDelegatingHandler : DelegatingHandler
    {
        private readonly ReliabilitySettings settings;

        private RetryPolicy retryPolicy;

        public RetryDelegatingHandler(ReliabilitySettings settings)
            : this(new HttpClientHandler(), settings)
        {
        }

        public RetryDelegatingHandler(HttpMessageHandler innerHandler, ReliabilitySettings settings)
            : base(innerHandler)
        {
            this.settings = settings;
            ConfigurePolicy();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!settings.UseRetryPolicy)
            {
                return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }

            HttpResponseMessage responseMessage;

            var result = await retryPolicy.ExecuteAndCaptureAsync(
                             async () =>
                                 {
                                     try
                                     {
                                         responseMessage = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
                                         EnsureResponseIsValid(responseMessage);
                                     }
                                     catch (TaskCanceledException)
                                     {
                                         throw new TimeoutException();
                                     }

                                     return responseMessage;
                                 });

            if (result.Outcome == OutcomeType.Successful)
            {
                return result.Result;
            }

            throw result.FinalException;
        }

        private static void EnsureResponseIsValid(HttpResponseMessage responseMessage)
        {
            if ((int)responseMessage.StatusCode >= 500 && (int)responseMessage.StatusCode < 600)
            {
                throw new HttpRequestException(string.Format("Response Http Status code {0} indicates server error", responseMessage.StatusCode));
            }
        }

        private void ConfigurePolicy()
        {
            retryPolicy = Policy.Handle<HttpRequestException>().Or<TimeoutException>().WaitAndRetryAsync(settings.RetryCount, i => settings.RetryInterval);
        }
    }
}