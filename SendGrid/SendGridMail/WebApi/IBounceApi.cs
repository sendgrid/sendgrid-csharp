using System;
using System.Collections.Generic;
using System.Linq;

namespace SendGridMail.WebApi
{
    /// <summary>
    /// Allows you to retrieve and delete entries in the Bounces list.
    /// </summary>
    public interface IBounceApi
    {
        /// <summary>
        /// Retrieve a list of bounces with addresses and response codes, optionally with dates.
        /// </summary>
        /// <param name="includeDate">Determines if the date of the bounce should be included.</param>
        /// <param name="days">Number of days in the past for which to retrieve bounces (includes today).</param>
        /// <param name="startDate">The start of the date range for which to retrieve bounces.</param>
        /// <param name="endDate">The end of the date range for which to retrieve bounces.</param>
        /// <param name="limit">Limit the number of results returned.</param>
        /// <param name="offset">Beginning point in the list to retrieve from.</param>
        /// <param name="type">The type(s) of bounces to include.</param>
        /// <param name="email">Optional email address to filter by.</param>
        /// <returns></returns>
        List<Bounce> GetBounces(Boolean includeDate, Int32? days, DateTime? startDate, DateTime? endDate, Int32? limit, Int32? offset, BounceType type, String email);

        /// <summary>
        /// Delete an address from the Bounce list. Please note that if no parameters are specified the ENTIRE list will be deleted.
        /// </summary>
        /// <param name="startDate">The start of the date range for which to retrieve bounces.</param>
        /// <param name="endDate">The end of the date range for which to retrieve bounces.</param>
        /// <param name="type">The type(s) of bounces to include.</param>
        /// <param name="email">Optional email address to filter by.</param>
        /// <returns></returns>
        void DeleteBounces(DateTime? startDate, DateTime? endDate, BounceType type, String email);
    }
}