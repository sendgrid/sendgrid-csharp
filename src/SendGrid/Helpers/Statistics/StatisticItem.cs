// <copyright file="StatisticItem.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid.Helpers.Statistics
{
    using System.Collections.Generic;

    /// <summary>
    /// Represent one statistic item
    /// </summary>
    public class StatisticItem
    {
        /// <summary>
        /// Gets or sets date
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets stats related to date
        /// </summary>
        public IList<DateStatisticItem> Stats { get; set; }
    }
}
