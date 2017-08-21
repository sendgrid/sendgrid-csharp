namespace SendGrid.Helpers.Reliability
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A delegating handler that provides retry functionality while executing a request
    /// </summary>
    public class RetryDelegatingHandler : DelegatingHandler
    {
        private static readonly List<int> RetriableStatusCodes = new List<int>() { 500, 502, 503, 504 };

        private readonly ReliabilitySettings settings;

        private readonly Random random = new Random();

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
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (this.settings.MaximumNumberOfRetries == 0)
            {
                return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }

            HttpResponseMessage responseMessage = null;

            var waitFor = settings.RetryInterval;
            var numberOfAttempts = 0;
            var sent = false;

            while (!sent)
            {
                try
                {
                    responseMessage = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

                    ThrowHttpRequestExceptionIfResponseCodeCanBeRetried(responseMessage);

                    sent = true;
                }
                catch (TaskCanceledException)
                {
                    numberOfAttempts++;

                    if (numberOfAttempts > this.settings.MaximumNumberOfRetries)
                    {
                        throw new TimeoutException();
                    }

                    // ReSharper disable once MethodSupportsCancellation, cancel will be indicated on the token
                    await Task.Delay(waitFor).ConfigureAwait(false);
                }
                catch (HttpRequestException)
                {
                    numberOfAttempts++;

                    if (numberOfAttempts > this.settings.MaximumNumberOfRetries)
                    {
                        throw;
                    }

                    await Task.Delay(waitFor).ConfigureAwait(false);
                }

                waitFor = GetNextWaitInterval(numberOfAttempts);
            }

            return responseMessage;
        }

        private static void ThrowHttpRequestExceptionIfResponseCodeCanBeRetried(HttpResponseMessage responseMessage)
        {
            int statusCode = (int)responseMessage.StatusCode;

            if (RetriableStatusCodes.Contains(statusCode))
            {
                throw new HttpRequestException(string.Format("Http status code '{0}' indicates server error", statusCode));
            }
        }

        private TimeSpan GetNextWaitInterval(int numberOfAttempts)
        {
            var interval = this.settings.RetryInterval.TotalMilliseconds + (Math.Pow(2, numberOfAttempts) * 1000);

            var randomDelay = this.random.Next(0, 1000);

            return TimeSpan.FromMilliseconds(interval + randomDelay);
        }
    }
}
