// <copyright file="EmailAddress.cs" company="Twilio SendGrid">
// Copyright (c) Twilio SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// An email object containing the email address and name of the sender or recipient.
    /// </summary>
    [JsonObject(IsReference = false)]
    public class EmailAddress : IEquatable<EmailAddress>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddress"/> class.
        /// </summary>
        public EmailAddress()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddress"/> class.
        /// </summary>
        /// <param name="email">The email address of the sender or recipient.</param>
        /// <param name="name">The name of the sender or recipient.</param>
        public EmailAddress(string email, string name = null)
        {
            this.Email = email;
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the name of the sender or recipient.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address of the sender or recipient.
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="EmailAddress"/> is equal to the specified EmailAddress.
        /// </summary>
        /// <param name="other">The comparand email address.</param>
        /// <returns>true if the objects are equal, false if they're not.</returns>
        public bool Equals(EmailAddress other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && string.Equals(Email, other.Email);
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="EmailAddress"/> is equal to the specified object.
        /// </summary>
        /// <param name="obj">The comparand object.</param>
        /// <returns>true if the objects are equal, false if they're not.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EmailAddress)obj);
        }

        /// <summary>
        /// Gets a hash code representing this object.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Email != null ? Email.GetHashCode() : 0);
            }
        }
    }
}
