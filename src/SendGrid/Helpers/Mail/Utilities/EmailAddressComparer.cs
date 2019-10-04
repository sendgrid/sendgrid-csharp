using System;
using System.Collections.Generic;
using System.Text;

namespace SendGrid.Helpers.Mail.Utilites
{
    /// <summary>
    /// EmailAddress comparer to if check two objects are equal and generate Hashcode for an object.
    /// </summary>
    internal class EmailAddressComparer : IEqualityComparer<EmailAddress>
    {
        /// <inheritdoc/>
        public bool Equals(EmailAddress x, EmailAddress y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            {
                return false;
            }

            return !string.IsNullOrWhiteSpace(x.Email) && StringComparer.OrdinalIgnoreCase.Equals(x.Email, y.Email);
        }

        /// <inheritdoc/>
        public int GetHashCode(EmailAddress data)
        {
            if (ReferenceEquals(data, null))
            {
                return 0;
            }

            return string.IsNullOrWhiteSpace(data.Email) ? 0 : StringComparer.OrdinalIgnoreCase.GetHashCode(data.Email);
        }
    }
}
