using System;

namespace SendGrid.Helpers.Reliability
{
    /// <summary>
    /// Defines the reliability settings to use on HTTP requests
    /// </summary>
    public class ReliabilitySettings
    {
        private int retryCount;
        private TimeSpan retryInterval;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliabilitySettings"/> class.
        /// </summary>
        public ReliabilitySettings()
        {
            retryCount = 0;
            retryInterval = TimeSpan.FromSeconds(1);
        }

        /// <summary>
        ///     Gets or sets the number of retries to execute against an HTTP service endpoint before throwing an exceptions. Defaults to 0 (no retries, you must explicitly enable)
        /// </summary>
        public int RetryCount
        {
            get
            {
                return retryCount;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Retry count must be greater than zero");
                }

                if (value > 5)
                {
                    throw new ArgumentException("Retry count must be less than 5");
                }

                retryCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the interval between HTTP retries. Defaults to 1 second
        /// </summary>
        public TimeSpan RetryInterval
        {
            get
            {
                return retryInterval;
            }

            set
            {
                if (value.TotalSeconds > 30)
                {
                    throw new ArgumentException("Maximum retry interval is 30 seconds");
                }

                retryInterval = value;
            }
        }
    }
}