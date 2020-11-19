namespace SendGrid.Helpers.Errors.Model
{
    /// <summary>
    /// Base class for the json error response
    /// </summary>
    public class SendGridErrorResponse
    {
        /// <summary>
        /// Gets or sets the error Status Code
        /// </summary>
        public int ErrorHttpStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the error Reason Phrase
        /// </summary>
        public string ErrorReasonPhrase { get; set; }

        /// <summary>
        /// Gets or sets the SendGrid error message
        /// </summary>
        public string SendGridErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the field that has the error
        /// </summary>
        public string FieldWithError { get; set; }

        /// <summary>
        /// Gets or sets the error default help
        /// </summary>
        public string HelpLink { get; set; }
    }
}
