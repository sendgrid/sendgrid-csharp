using System;
using System.Collections.Generic;
using System.Linq;

namespace SendGridMail.WebApi
{
    /// <summary>
    /// Retrieve, delete and add entries in the Unsubscribes list.
    /// </summary>
    public interface IUnsubscribesApi
    {
        /// <summary>
        /// Retrieve a list of Unsubscribes with addresses and optionally with dates.
        /// </summary>
        /// <param name="includeDate">Determines if the date of the unsubscribe should be included.</param>
        /// <param name="days">Number of days in the past for which to retrieve unsubscribes (includes today).</param>
        /// <param name="startDate">The start of the date range for which to retrieve unsubscribes.</param>
        /// <param name="endDate">The end of the date range for which to retrieve unsubscribes.</param>
        /// <param name="limit">Limit the number of results returned.</param>
        /// <param name="offset">Beginning point in the list to retrieve from.</param>
        /// <param name="email">Optional email address to filter by.</param>
        /// <returns></returns>
        List<Unsubscribe> GetUnsubscribes(Boolean includeDate, Int32? days, DateTime? startDate, DateTime? endDate, Int32? limit, Int32? offset, String email);

        /// <summary>
        /// Delete an address from the Unsubscribe list. Please note that if no parameters are provided the ENTIRE list will be removed.
        /// </summary>
        /// <param name="startDate">Optional date to start retrieving for.</param>
        /// <param name="endDate">Optional date to end retrieving for.</param>
        /// <param name="email">Unsubscribed email address to remove</param>
        /// <returns></returns>
        void DeleteUnsubscribes(DateTime? startDate, DateTime? endDate, String email);

        /// <summary>
        /// Add email addresses to the Unsubscribe list.
        /// </summary>
        /// <param name="email">Email address to add to unsubscribe list.  Must be a valid email address.</param>
        void AddUnsubscribes(String email);
    }
}