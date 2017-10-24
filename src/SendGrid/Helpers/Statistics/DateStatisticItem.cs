// <copyright file="DateStatisticItem.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid.Helpers.Statistics
{
    /// <summary>
    /// Statistic item related to a date
    /// </summary>
    public class DateStatisticItem
    {
        /// <summary>
        /// Gets or sets metrics
        /// </summary>
        public StatisticMetric Metrics { get; set; }

        /// <summary>
        /// Gets or sets name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets type
        /// </summary>
        public string Type { get; set; }
    }
}
