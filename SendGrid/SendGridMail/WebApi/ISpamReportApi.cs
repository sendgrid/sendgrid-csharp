using System;
using System.Collections.Generic;
using System.Linq;

namespace SendGridMail.WebApi
{
    /// <summary>
    /// Retrieve and delete entries in the Spam Reports list.
    /// </summary>
    public interface ISpamReportApi
    {
        /// <summary>
        /// Retrieve a list of spam reports with addresses and ip address, optionally with dates.
        /// </summary>
        /// <param name="includeDate">Determines if the date of the spam report should be included.</param>
        /// <param name="days">Number of days in the past for which to retrieve spam reports (includes today).</param>
        /// <param name="startDate">The start of the date range for which to retrieve spam reports.</param>
        /// <param name="endDate">The end of the date range for which to retrieve spam reports.</param>
        /// <param name="limit">Limit the number of results returned.</param>
        /// <param name="offset">Beginning point in the list to retrieve from.</param>
        /// <param name="email">Optional email address to filter by.</param>
        /// <returns></returns>
        List<SpamReport> GetSpamReports(Boolean includeDate, Int32? days, DateTime? startDate, DateTime? endDate, Int32? limit, Int32? offset, String email);

        /// <summary>
        /// Delete an address from the Spam Reports list. Please note that if no parameters are specified the ENTIRE list will be deleted.
        /// </summary>
        /// <param name="email">Optional email address to filter by.</param>
        /// <returns></returns>
        void DeleteSpamReports(String email);
    }
}