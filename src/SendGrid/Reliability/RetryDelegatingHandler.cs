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
        private static List<int> retriableStatusCodes = new List<int>() { 500, 502, 503, 504 };

        private readonly ReliabilitySettings settings;

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
            if (this.settings.RetryCount == 0)
            {
                return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }

            HttpResponseMessage responseMessage = null;
            var numberOfAttempts = 0;
            var sent = false;

            while (!sent)
            {
                try
                {
                    responseMessage = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

                    ThrowHttpRequestExceptionIfResponseCodeIsRetriable(responseMessage);

                    sent = true;
                }
                catch (TaskCanceledException)
                {
                    numberOfAttempts++;

                    if (numberOfAttempts > this.settings.RetryCount)
                    {
                        throw new TimeoutException();
                    }

                    // ReSharper disable once MethodSupportsCancellation, cancel will be indicated on the token
                    await Task.Delay(this.settings.RetryInterval).ConfigureAwait(false);
                }
                catch (HttpRequestException)
                {
                    numberOfAttempts++;

                    if (numberOfAttempts > this.settings.RetryCount)
                    {
                        throw;
                    }

                    await Task.Delay(this.settings.RetryInterval).ConfigureAwait(false);
                }
            }

            return responseMessage;
        }

        private static void ThrowHttpRequestExceptionIfResponseCodeIsRetriable(HttpResponseMessage responseMessage)
        {
            int statusCode = (int)responseMessage.StatusCode;

            if (retriableStatusCodes.Contains(statusCode))
            {
                throw new HttpRequestException(string.Format("Http status code '{0}' indicates server error", statusCode));
            }
        }
    }
}
