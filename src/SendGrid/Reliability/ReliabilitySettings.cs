namespace SendGrid.Helpers.Reliability
{
    using System;

    /// <summary>
    /// Defines the reliability settings to use on HTTP requests
    /// </summary>
    public class ReliabilitySettings
    {
        private int maximumNumberOfRetries;

        private TimeSpan minimumBackOff;
        private TimeSpan maximumBackOff;
        private TimeSpan deltaBackOff;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliabilitySettings"/> class.
        /// </summary>
        public ReliabilitySettings()
        {
            this.maximumNumberOfRetries = 0;
            this.minimumBackOff = TimeSpan.FromSeconds(1);
            this.deltaBackOff = TimeSpan.FromSeconds(1);
            this.maximumBackOff = TimeSpan.FromSeconds(10);
        }

        /// <summary>
        ///     Gets or sets the maximum number of retries to execute against when sending an HTTP Request before throwing an exception. Defaults to 0 (no retries, you must explicitly enable)
        /// </summary>
        public int MaximumNumberOfRetries
        {
            get
            {
                return this.maximumNumberOfRetries;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Retry count must be greater than zero");
                }

                if (value > 5)
                {
                    throw new ArgumentException("The maximum number of retries that can be attempted is 5");
                }

                this.maximumNumberOfRetries = value;
            }
        }

        /// <summary>
        /// Gets or sets the interval between HTTP retries. Defaults to 1 second
        /// </summary>
        public TimeSpan MinimumBackOff
        {
            get
            {
                return this.minimumBackOff;
            }

            set
            {
                if (value.TotalSeconds > 30)
                {
                    throw new ArgumentException("The maximum setting for minimum back off is 30 seconds");
                }

                this.minimumBackOff = value;
            }
        }

        public TimeSpan MaximumBackOff
        {
            get
            {
                return this.maximumBackOff;
            }

            set
            {
                if (value.TotalSeconds > 30)
                {
                    throw new ArgumentException("The maximum setting to back off for is 30 seconds");
                }

                this.maximumBackOff = value;
            }
        }

        public TimeSpan DeltaBackOff
        {
            get
            {
                return this.deltaBackOff;
            }

            set
            {
                if (value.TotalSeconds > 30)
                {
                    throw new ArgumentException("The maximum delta interval is 5 seconds");
                }

                this.deltaBackOff = value;
            }
        }
    }
}
