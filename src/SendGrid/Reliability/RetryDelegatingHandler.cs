namespace SendGrid.Helpers.Reliability
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A delegating handler that provides retry functionality while executing a request
    /// </summary>
    public class RetryDelegatingHandler : DelegatingHandler
    {
        private static readonly List<HttpStatusCode> RetriableServerErrorStatusCodes =
            new List<HttpStatusCode>()
            {
                HttpStatusCode.InternalServerError,
                HttpStatusCode.BadGateway,
                HttpStatusCode.ServiceUnavailable,
                HttpStatusCode.GatewayTimeout
            };

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

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryDelegatingHandler"/> class.
        /// </summary>
        /// <param name="innerHandler">A HttpMessageHandler instance to set as the innner handler</param>
        /// <param name="settings">A ReliabilitySettings instance</param>
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

            var numberOfAttempts = 0;
            var sent = false;

            while (!sent)
            {
                var waitFor = this.GetNextWaitInterval(numberOfAttempts);

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
            }

            return responseMessage;
        }

        private static void ThrowHttpRequestExceptionIfResponseCodeCanBeRetried(HttpResponseMessage responseMessage)
        {
            if (RetriableServerErrorStatusCodes.Contains(responseMessage.StatusCode))
            {
                throw new HttpRequestException(string.Format("Http status code '{0}' indicates server error", responseMessage.StatusCode));
            }
        }

        private TimeSpan GetNextWaitInterval(int numberOfAttempts)
        {
            var randomDelay = this.random.Next(0, 500);

            if (numberOfAttempts == 0)
            {
                return TimeSpan.FromMilliseconds(this.settings.RetryInterval.TotalMilliseconds + randomDelay);
            }

            var exponentialIncrease = Math.Pow(2, numberOfAttempts) * 1000;

            var actualIncrease = TimeSpan.FromMilliseconds(this.settings.RetryInterval.TotalMilliseconds + exponentialIncrease + randomDelay);

            return actualIncrease;
        }
    }
}
