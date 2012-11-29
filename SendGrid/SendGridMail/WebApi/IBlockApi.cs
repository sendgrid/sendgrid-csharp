using System;
using System.Collections.Generic;

namespace SendGridMail.WebApi
{
    /// <summary>
    /// Allows you to retrieve and delete entries in the Blocks list.
    /// </summary>
    public interface IBlockApi
    {
        /// <summary>
        /// Retrieve a list of Blocks with addresses and response codes, optionally with dates.
        /// </summary>
        /// <param name="includeDate">Determines if the date of the bounce should be included.</param>
        /// <param name="days">Number of days in the past for which to retrieve bounces (includes today).</param>
        /// <param name="startDate">The start of the date range for which to retrieve bounces.</param>
        /// <param name="endDate">The end of the date range for which to retrieve bounces.</param>
        /// <returns></returns>
        List<Block> GetBlocks(Boolean includeDate, Int32? days, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// Delete an address from the Block list.
        /// </summary>
        /// <param name="email">Email block address to remove.  Must be a valid user account email</param>
        void DeleteBlocks(String email);
    }
}