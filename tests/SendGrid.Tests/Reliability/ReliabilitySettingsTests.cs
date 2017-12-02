namespace SendGrid.Tests.Reliability
{
    using System;
    using SendGrid.Helpers.Reliability;
    using Xunit;

    public class ReliabilitySettingsTests
    {
        [Fact]
        public void ShouldNotAllowNegativeRetryCount()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => 
                new ReliabilitySettings(-1, 
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(1)));

            Assert.Contains("maximumNumberOfRetries must be greater than 0", exception.Message);
        }

        [Fact]
        public void ShouldNotAllowNegativeMinimumBackoffTime()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
                new ReliabilitySettings(1,
                    TimeSpan.FromSeconds(-1),
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(1)));

            Assert.Contains("minimumBackoff must be greater than 0", exception.Message);
        }

        [Fact]
        public void ShouldNotAllowNegativeMaximumBackoffTime()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
                new ReliabilitySettings(1,
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(-11),
                    TimeSpan.FromSeconds(1)));

            Assert.Contains("maximumBackOff must be greater than 0", exception.Message);
        }

        [Fact]
        public void ShouldNotAllowNegativeDeltaBackoffTime()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
                new ReliabilitySettings(1,
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(-1)));

            Assert.Contains("deltaBackOff must be greater than 0", exception.Message);
        }

        [Fact]
        public void ShouldNotAllowRetryCountGreaterThan5()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
                new ReliabilitySettings(6,
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(1)));

            Assert.Contains("The maximum number of retries allowed is 5", exception.Message);
        }

        [Fact]
        public void ShouldNotAllowMinimumBackOffGreaterThanMaximumBackoff()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
                new ReliabilitySettings(1,
                    TimeSpan.FromSeconds(11),
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(1)));

            Assert.Contains("minimumBackoff must be less than maximumBackOff", exception.Message);
        }

        [Fact]
        public void ShouldNotAllowMaximumBackOffGreaterThan30Seconds()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
                new ReliabilitySettings(1,
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(31),
                    TimeSpan.FromSeconds(1)));

            Assert.Contains("maximumBackOff must be less than 30 seconds", exception.Message);
        }

        [Fact]
        public void ShouldPassValidValuesFromDefaultConstruct()
        {
            var defaultSettings = new ReliabilitySettings();

            Assert.Equal(TimeSpan.Zero, defaultSettings.MaximumBackOff);
            Assert.Equal(TimeSpan.Zero, defaultSettings.MinimumBackOff);
            Assert.Equal(TimeSpan.Zero, defaultSettings.DeltaBackOff);
            Assert.Equal(0, defaultSettings.MaximumNumberOfRetries);
        }

        [Fact]
        public void ShouldNotAllowNullInstanceOnSendGridClientOptions()
        {
            var options = new SendGridClientOptions();

            Assert.Throws<ArgumentNullException>(() => options.ReliabilitySettings = null);
        }
    }
}

