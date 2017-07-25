using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SendGrid.Tests.Helpers.Reliability
{
    public class RetryTestBehaviourDelegatingHandler : DelegatingHandler
    {
        private Func<Task<HttpResponseMessage>> behaviour;

        public RetryTestBehaviourDelegatingHandler()
        {
            ConfigureBehaviour(OK);
        }

        public int InvocationCount { get; private set; }

        public void ConfigureBehaviour(Func<Task<HttpResponseMessage>> configuredBehavior)
        {
            behaviour = () =>
            {
                InvocationCount++;
                return configuredBehavior();
            };
        }

        public Task<HttpResponseMessage> OK()
        {
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("string content") };
            return Task.Factory.StartNew(() => httpResponseMessage);
        }

        public Task<HttpResponseMessage> InternalServerError()
        {
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent("string content") };
            return Task.Factory.StartNew(() => httpResponseMessage);
        }


        public Task<HttpResponseMessage> AuthenticationError()
        {
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent("not authorizaed") };
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
            return behaviour();
        }
    }
}