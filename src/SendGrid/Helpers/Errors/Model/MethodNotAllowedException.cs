﻿using System;

namespace SendGrid.Helpers.Errors.Model
{
    /// <summary>
    /// Represents errors with status code 405
    /// </summary>
    public class MethodNotAllowedException : RequestErrorException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodNotAllowedException"/> class.
        /// </summary>
        public MethodNotAllowedException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodNotAllowedException"/> class with a specified error.
        /// </summary>
        /// <param name="message"> The error message that explains the reason for the exception.</param>
        public MethodNotAllowedException(string message)
                : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodNotAllowedException"/> class with a specified error and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference  if no inner exception is specified.</param>
        public MethodNotAllowedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
