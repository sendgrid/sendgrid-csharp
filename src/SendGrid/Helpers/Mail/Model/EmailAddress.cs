// <copyright file="EmailAddress.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid.Helpers.Mail
{
    using Newtonsoft.Json;

    /// <summary>
    /// An email object containing the email address and name of the sender or recipient.
    /// </summary>
    [JsonObject(IsReference = false)]
    public class EmailAddress
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
    }
}
