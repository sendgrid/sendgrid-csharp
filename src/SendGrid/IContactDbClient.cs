// <copyright file="IContactDbClient.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Threading.Tasks;

namespace SendGrid
{
    /// <summary>
    /// A HTTP client wrapper for interacting with SendGrid's Contact API
    /// </summary>
    public interface IContactDbClient
    {
        /// <summary>
        /// Creates a custom field
        /// </summary>
        /// <param name="name">The name of the custom field.</param>
        /// <param name="type">the type of the custom field.</param>
        /// <returns>A Response object.</returns>
        Task<Response> CreateCustomField(string name, string type);

        /// <summary>
        /// Retrieve all custom fields.
        /// </summary>
        /// <returns>A Response object.</returns>        
        Task<Response> GetAllCustomFields();

        /// <summary>
        /// Retrieve a custom field
        /// </summary>
        /// <param name="customFieldId">The identifier of the custom field.</param>
        /// <returns>A Response object.</returns>
        Task<Response> GetCustomField(string customFieldId);
    }
}