// <copyright file="IAttachmentSource.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid.Helpers.Mail
{
    using System.Threading.Tasks;

    /// <summary>
    /// Provides attachment
    /// </summary>
    public interface IAttachmentSource
    {
        /// <summary>
        /// Create <see cref="AttachmentSource"/> object from source
        /// </summary>
        /// <returns>An <see cref="AttachmentSource"/> object</returns>
        Task<AttachmentSource> GetAttachmentAsync();
    }
}
