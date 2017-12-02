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
        /// <param name="innerHandler">A HttpMessageHandler instance to set as the inner handler</param>
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
            var random = new Random();

            var delta = (int)((Math.Pow(2.0, numberOfAttempts) - 1.0) *
                               random.Next(
                                   (int)(this.settings.DeltaBackOff.TotalMilliseconds * 0.8),
                                   (int)(this.settings.DeltaBackOff.TotalMilliseconds * 1.2)));

            var interval = (int)Math.Min(this.settings.MinimumBackOff.TotalMilliseconds + delta, this.settings.MaximumBackOff.TotalMilliseconds);

            return TimeSpan.FromMilliseconds(interval);
        }
    }
}
