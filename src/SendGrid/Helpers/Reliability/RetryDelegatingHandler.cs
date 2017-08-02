namespace SendGrid.Helpers.Reliability
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Polly;
    using Polly.Retry;

    /// <summary>
    /// A delegating handler that provides retry functionality while executing a request
    /// </summary>
    public class RetryDelegatingHandler : DelegatingHandler
    {
        private readonly ReliabilitySettings settings;

        private RetryPolicy retryPolicy;

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryDelegatingHandler"/> class.
        /// </summary>
        /// <param name="settings">A ReliabilitySettings instance</param>
        public RetryDelegatingHandler(ReliabilitySettings settings)
            : this(new HttpClientHandler(), settings)
        {
        }

        public RetryDelegatingHandler(HttpMessageHandler innerHandler, ReliabilitySettings settings)
            : base(innerHandler)
        {
            this.settings = settings;
            this.ConfigurePolicy();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (this.settings.RetryCount == 0)
            {
                return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }

            HttpResponseMessage responseMessage;

            var result = await this.retryPolicy.ExecuteAndCaptureAsync(
                             async () =>
                                 {
                                     try
                                     {
                                         responseMessage = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
                                         ThrowHttpRequestExceptionIfResponseIsWithinTheServerErrorRange(responseMessage);
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

        private static void ThrowHttpRequestExceptionIfResponseIsWithinTheServerErrorRange(HttpResponseMessage responseMessage)
        {
            if ((int)responseMessage.StatusCode >= 500 && (int)responseMessage.StatusCode < 600)
            {
                throw new HttpRequestException(string.Format("Response Http Status code {0} indicates server error", responseMessage.StatusCode));
            }
        }

        private void ConfigurePolicy()
        {
            this.retryPolicy = Policy.Handle<HttpRequestException>().Or<TimeoutException>().WaitAndRetryAsync(settings.RetryCount, i => settings.RetryInterval);
        }
    }
}