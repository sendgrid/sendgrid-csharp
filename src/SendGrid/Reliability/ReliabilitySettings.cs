namespace SendGrid.Reliability
{
    using System;

    /// <summary>
    /// Defines the reliability settings to use on HTTP requests
    /// </summary>
    public class ReliabilitySettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationReliabilitySettings"/> class.
        /// Default ctor, sets default property values
        /// </summary>
        public ReliabilitySettings()
        {
            RetryCount = 0;
            RetryInterval = TimeSpan.FromSeconds(1);
        }

        /// <summary>
        ///     Gets or sets the number of retries to execute against an HTTP service endpoint before throwing an exceptions. Defaults to 2
        /// </summary>
        public int RetryCount { get; set; }

        /// <summary>
        /// Gets or sets the interval between HTTP retries. Defaults to 1 second
        /// </summary>
        public TimeSpan RetryInterval { get; set; }
    }
}