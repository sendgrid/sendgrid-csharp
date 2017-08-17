namespace SendGrid.Tests.Reliability
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class RetryTestBehaviourDelegatingHandler : DelegatingHandler
    {
        private readonly List<Func<Task<HttpResponseMessage>>> behaviours;

        public RetryTestBehaviourDelegatingHandler()
        {
            behaviours = new List<Func<Task<HttpResponseMessage>>>();
        }

        public int InvocationCount { get; private set; }

        public void AddBehaviour(Func<Task<HttpResponseMessage>> configuredBehavior)
        {
            Task<HttpResponseMessage> behaviour()
            {
                InvocationCount++;
                return configuredBehavior();
            }

            behaviours.Add(behaviour);
        }

        public Task<HttpResponseMessage> OK()
        {
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(string.Empty) };
            return Task.Factory.StartNew(() => httpResponseMessage);
        }

        public Task<HttpResponseMessage> InternalServerError()
        {
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(string.Empty) };
            return Task.Factory.StartNew(() => httpResponseMessage);
        }

        public Task<HttpResponseMessage> AuthenticationError()
        {
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent(string.Empty) };
            return Task.Factory.StartNew(() => httpResponseMessage);
        }

        public Task<HttpResponseMessage> TaskCancelled()
        {
            throw new TaskCanceledException();
        }

        public Task<HttpResponseMessage> NonTransientException()
        {
            throw new InvalidOperationException();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return behaviours[InvocationCount]();
        }
    }
}