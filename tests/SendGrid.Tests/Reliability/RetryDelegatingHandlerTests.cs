namespace SendGrid.Tests.Reliability
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using SendGrid.Helpers.Reliability;
    using Xunit;

    public class RetryDelegatingHandlerTests
    {
        private readonly HttpClient client;

        private readonly RetryTestBehaviourDelegatingHandler innerHandler;

        public RetryDelegatingHandlerTests()
        {
            var reliabilitySettings = new ReliabilitySettings
            {
                RetryCount = 1
            };
            innerHandler = new RetryTestBehaviourDelegatingHandler();
            client = new HttpClient(new RetryDelegatingHandler(innerHandler, reliabilitySettings))
            {
                BaseAddress = new Uri("http://localhost")
            };
        }

        [Fact]
        public async Task Invoke_ShouldReturnHttpResponseAndNotRetryWhenSuccessful()
        {
            innerHandler.AddBehaviour(innerHandler.OK);

            var result = await client.SendAsync(new HttpRequestMessage());

            Assert.Equal(result.StatusCode, HttpStatusCode.OK);
            Assert.Equal(1, innerHandler.InvocationCount);
        }

        [Fact]
        public async Task Invoke_ShouldReturnHttpResponseAndNotRetryWhenUnauthorised()
        {
            innerHandler.AddBehaviour(innerHandler.AuthenticationError);

            var result = await client.SendAsync(new HttpRequestMessage());

            Assert.Equal(result.StatusCode, HttpStatusCode.Unauthorized);
            Assert.Equal(1, innerHandler.InvocationCount);
        }

        [Fact]
        public async Task Invoke_ShouldReturnErrorWithoutRetryWhenErrorIsNotTransient()
        {
            innerHandler.AddBehaviour(innerHandler.NonTransientException);

            await Assert.ThrowsAsync<InvalidOperationException>(() => client.SendAsync(new HttpRequestMessage()));

            Assert.Equal(1, innerHandler.InvocationCount);
        }

        [Fact]
        public async Task Invoke_ShoulddRetryOnceWhenFailedOnFirstAttemptThenSuccessful()
        {
            innerHandler.AddBehaviour(innerHandler.TaskCancelled);
            innerHandler.AddBehaviour(innerHandler.OK);

            var result = await client.SendAsync(new HttpRequestMessage());

            Assert.Equal(result.StatusCode, HttpStatusCode.OK);
            Assert.Equal(2, innerHandler.InvocationCount);
        }

        [Fact]
        public async Task Invoke_ShouldRetryTheExpectedAmountOfTimesAndReturnTimeoutExceptionWhenTasksCancelled()
        {
            innerHandler.AddBehaviour(innerHandler.TaskCancelled);
            innerHandler.AddBehaviour(innerHandler.TaskCancelled);

            await Assert.ThrowsAsync<TimeoutException>(() => client.SendAsync(new HttpRequestMessage()));

            Assert.Equal(2, innerHandler.InvocationCount);
        }

        [Fact]
        public async Task Invoke_ShouldRetryTheExpectedAmountOfTimesAndReturnExceptionWhenInternalServerErrorsEncountered()
        {
            innerHandler.AddBehaviour(innerHandler.InternalServerError);
            innerHandler.AddBehaviour(innerHandler.InternalServerError);

            await Assert.ThrowsAsync<HttpRequestException>(() => client.SendAsync(new HttpRequestMessage()));

            Assert.Equal(2, innerHandler.InvocationCount);
        }

        [Fact]
        public void ReliabilitySettingsShouldNotAllowNegativeRetryCount()
        {
            var settings = new ReliabilitySettings();

            Assert.Throws<ArgumentException>(() => settings.RetryCount = -1);
        }

        [Fact]
        public void ReliabilitySettingsShouldNotAllowRetryCountGreaterThan5()
        {
            var settings = new ReliabilitySettings();

            Assert.Throws<ArgumentException>(() => settings.RetryCount = 6);
        }

        [Fact]
        public void ReliabilitySettingsShouldNotAllowRetryIntervalGreaterThan30Seconds()
        {
            var settings = new ReliabilitySettings();

            Assert.Throws<ArgumentException>(() => settings.RetryInterval = TimeSpan.FromSeconds(31));
        }
    }
}