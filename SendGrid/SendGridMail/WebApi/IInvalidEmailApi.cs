using System;
using System.Collections.Generic;
using System.Linq;

namespace SendGridMail.WebApi
{
    /// <summary>
    /// Allows you to retrieve and delete entries in the Invalid Emails list.
    /// </summary>
    interface IInvalidEmailApi
    {
        /// <summary>
        /// Retrieve a list of invalid emails with addresses and response codes, optionally with dates.
        /// </summary>
        /// <param name="includeDate">Determines if the date of the  invalid email should be included.</param>
        /// <param name="days">Number of days in the past for which to retrieve  invalid emails (includes today).</param>
        /// <param name="startDate">The start of the date range for which to retrieve  invalid emails.</param>
        /// <param name="endDate">The end of the date range for which to retrieve  invalid emails.</param>
        /// <param name="limit">Limit the number of results returned.</param>
        /// <param name="offset">Beginning point in the list to retrieve from.</param>
        /// <param name="email">Optional email address to filter by.</param>
        /// <returns></returns>
        List<InvalidEmail> GetInvalidEmails(Boolean includeDate, Int32? days, DateTime? startDate, DateTime? endDate, Int32? limit, Int32? offset, String email);

        /// <summary>
        /// Delete an address from the Invalid Email list.
        /// </summary>
        /// <param name="email">Email Invalid Email address to remove.  Must be a valid user account email.</param>
        void DeleteInvalidEmails(String email);
    }
}