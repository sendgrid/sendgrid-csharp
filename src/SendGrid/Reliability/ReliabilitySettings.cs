namespace SendGrid.Helpers.Reliability
{
    using System;

    /// <summary>
    /// Defines the reliability settings to use on HTTP requests
    /// </summary>
    public class ReliabilitySettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReliabilitySettings"/> class with default settings.
        /// </summary>
        public ReliabilitySettings()
            : this(0, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliabilitySettings"/> class.
        /// </summary>
        /// <param name="maximumNumberOfRetries">The maximum number of retries to execute against when sending an HTTP Request before throwing an exception</param>
        /// <param name="minimumBackoff">The minimum amount of time to wait between between HTTP retries</param>
        /// <param name="maximumBackOff">the maximum amount of time to wait between between HTTP retries</param>
        /// <param name="deltaBackOff">the value that will be used to calculate a random delta in the exponential delay between retries</param>
        public ReliabilitySettings(int maximumNumberOfRetries, TimeSpan minimumBackoff, TimeSpan maximumBackOff, TimeSpan deltaBackOff)
        {
            if (maximumNumberOfRetries < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maximumNumberOfRetries), "maximumNumberOfRetries must be greater than 0");
            }

            if (maximumNumberOfRetries > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(maximumNumberOfRetries), "The maximum number of retries allowed is 5");
            }

            if (minimumBackoff.Ticks < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minimumBackoff), "minimumBackoff must be greater than 0");
            }

            if (maximumBackOff.Ticks < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maximumBackOff), "maximumBackOff must be greater than 0");
            }

            if (maximumBackOff.TotalSeconds > 30)
            {
                throw new ArgumentOutOfRangeException(nameof(maximumBackOff), "maximumBackOff must be less than 30 seconds");
            }

            if (deltaBackOff.Ticks < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(deltaBackOff), "deltaBackOff must be greater than 0");
            }

            if (minimumBackoff.TotalMilliseconds > maximumBackOff.TotalMilliseconds)
            {
                throw new ArgumentOutOfRangeException(nameof(minimumBackoff), "minimumBackoff must be less than maximumBackOff");
            }

            this.MaximumNumberOfRetries = maximumNumberOfRetries;
            this.MinimumBackOff = minimumBackoff;
            this.DeltaBackOff = deltaBackOff;
            this.MaximumBackOff = maximumBackOff;
        }

        /// <summary>
        ///     Gets the maximum number of retries to execute against when sending an HTTP Request before throwing an exception. Defaults to 0 (no retries, you must explicitly enable)
        /// </summary>
        public int MaximumNumberOfRetries { get; }

        /// <summary>
        /// Gets the minimum amount of time to wait between between HTTP retries. Defaults to 1 second
        /// </summary>
        public TimeSpan MinimumBackOff { get; }

        /// <summary>
        /// Gets the maximum amount of time to wait between between HTTP retries. Defaults to 10 seconds
        /// </summary>
        public TimeSpan MaximumBackOff { get; }

        /// <summary>
        /// Gets the value that will be used to calculate a random delta in the exponential delay between retries. Defaults to 1 second
        /// </summary>
        public TimeSpan DeltaBackOff { get; }
    }
}
