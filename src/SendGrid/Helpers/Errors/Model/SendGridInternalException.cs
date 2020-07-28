﻿using System;

namespace SendGrid.Helpers.Errors.Model
{
    /// <summary>
    /// Represents errors with status code 5xx. Being x any number
    /// </summary>
    public class SendGridInternalException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridInternalException"/> class.
        /// </summary>
        public SendGridInternalException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridInternalException"/> class with a specified error.
        /// </summary>
        /// <param name="message"> The error message that explains the reason for the exception.</param>
        public SendGridInternalException(string message)
                : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridInternalException"/> class with a specified error and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference  if no inner exception is specified.</param>
        public SendGridInternalException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
