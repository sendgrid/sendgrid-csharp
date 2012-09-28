using System.Collections.Generic;
using System.Net.Mail;

namespace SendGrid
{
    /// <summary>
    /// Represents the additional functionality to add SendGrid specific mail headers
    /// </summary>
    public interface IHeader
    {
		/// <summary>
		/// Gets the array of recipient addresses from the X-SMTPAPI header
		/// </summary>
		IEnumerable<string> To { get; }

        /// <summary>
        /// This adds a substitution value to be used during the mail merge.  Substitutions
        /// will happen in order added, so calls to this should match calls to addTo in the mail message.
        /// </summary>
        /// <param name="tag">string to be replaced in the message</param>
        /// <param name="substitutions">substitutions to be made, one per recipient</param>
        void AddSubVal(string tag, IEnumerable<string> substitutions);

        /// <summary>
        /// This adds the "to" array to the X-SMTPAPI header so that multiple recipients
        /// may be addressed in a single email. (but they each get their own email, instead of a single email with multiple TO: addressees)
        /// </summary>
        /// <param name="addresses">List of email addresses</param>
        void AddTo(IEnumerable<string> addresses);

        /// <summary>
        /// This adds parameters and values that will be passed back through SendGrid's
        /// Event API if an event notification is triggered by this email.
        /// </summary>
        /// <param name="identifiers">parameter value pairs to be passed back on event notification</param>
        void AddUniqueIdentifier(IDictionary<string, string> identifiers);

        /// <summary>
        /// This sets the category for this email.  Statistics are stored on a per category
        /// basis, so this can be useful for tracking on a per group basis.
        /// </summary>
        /// <param name="category">categories applied to the message</param>
        void SetCategory(string category);

        /// <summary>
        /// Shortcut method for enabling a filter.
        /// </summary>
        /// <param name="filter">The name of the filter to enable</param>
        void Enable(string filter);

        /// <summary>
        /// Shortcut method for disabling a filter.
        /// </summary>
        /// <param name="filter">The name of the filter to disable</param>
        void Disable(string filter);

        /// <summary>
        /// Allows you to specify a filter setting.  You can find a list of filters and settings here:
        /// http://docs.sendgrid.com/documentation/api/web-api/filtersettings/
        /// </summary>
        /// <param name="filter">The name of the filter to set</param>
        /// <param name="settings">The multipart name of the parameter being set</param>
        /// <param name="value">The value that the settings name will be assigning</param>
        void AddFilterSetting(string filter, IEnumerable<string> settings, string value);

        /// <summary>
        /// Attaches the SendGrid headers to the MIME.
        /// </summary>
        /// <param name="mime">the MIME to which we are attaching</param>
        void AddHeader(MailMessage mime);

        /// <summary>
        /// Converts the filter settings into a JSON string.
        /// </summary>
        /// <returns>string representation of the SendGrid headers</returns>
        string AsJson();
    }
}
