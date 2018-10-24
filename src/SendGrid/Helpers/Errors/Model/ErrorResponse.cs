using System;
using System.Collections.Generic;
using System.Text;

namespace SendGrid.Helpers.Errors.Model
{
    /// <summary>
    /// Base class for the json error response
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Gets or sets the Default error Status Code and Reason Phrase
        /// </summary>
        public string DefaultErrorData { get; set; }

        /// <summary>
        /// Gets or sets the send grid error message
        /// </summary>
        public string SendGriErrorMessage { get; set; }

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
