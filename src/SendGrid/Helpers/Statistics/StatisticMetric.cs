// <copyright file="StatisticMetric.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid.Helpers.Statistics
{
    using Newtonsoft.Json;

    /// <summary>
    /// Metric related to a statistic item of a date
    /// </summary>
    public class StatisticMetric
    {
        /// <summary>
        /// Gets or sets metric blocks
        /// </summary>
        public int? Blocks { get; set; }

        /// <summary>
        /// Gets or sets bounce drops
        /// </summary>
        [JsonProperty("bounce_drops")]
        public int? BounceDrops { get; set; }

        /// <summary>
        /// Gets or sets bounces
        /// </summary>
        public int? Bounces { get; set; }

        /// <summary>
        /// Gets or sets clicks
        /// </summary>
        public int? Clicks { get; set; }

        /// <summary>
        /// Gets or sets deferred
        /// </summary>
        public int? Deferred { get; set; }

        /// <summary>
        /// Gets or sets delivered
        /// </summary>
        public int? Delivered { get; set; }

        /// <summary>
        /// Gets or sets invalid emails
        /// </summary>
        [JsonProperty("invalid_emails")]
        public int? InvalidEmails { get; set; }

        /// <summary>
        /// Gets or sets opens
        /// </summary>
        public int? Opens { get; set; }

        /// <summary>
        /// Gets or sets processed
        /// </summary>
        public int? Processed { get; set; }

        /// <summary>
        /// Gets or sets requests
        /// </summary>
        public int? Requests { get; set; }

        /// <summary>
        /// Gets or sets spam report drops
        /// </summary>
        [JsonProperty("spam_report_drops")]
        public int? SpamReportDrops { get; set; }

        /// <summary>
        /// Gets or sets spam reports
        /// </summary>
        [JsonProperty("spam_reports")]
        public int? SpamReports { get; set; }

        /// <summary>
        /// Gets or sets unique clicks
        /// </summary>
        [JsonProperty("unique_clicks")]
        public int? UniqueClicks { get; set; }

        /// <summary>
        /// Gets or sets unique opens
        /// </summary>
        [JsonProperty("unique_opens")]
        public int? UniqueOpens { get; set; }

        /// <summary>
        /// Gets or sets unsubscribe drops
        /// </summary>
        [JsonProperty("unsubscribe_drops")]
        public int? UnsubscribeDrops { get; set; }

        /// <summary>
        /// Gets or sets unsubscribes
        /// </summary>
        public int? Unsubscribes { get; set; }
    }
}
