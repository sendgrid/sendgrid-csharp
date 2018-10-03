// <copyright file="IContactDbClient.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using SendGrid.Helpers.CustomDb;

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
        /// <returns>The created custom field.</returns>
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

        /// <summary>
        /// Delete a custom field
        /// </summary>
        /// <param name="customFieldId">The identifier of the custom field.</param>
        /// <returns>A Response object.</returns>
        Task<Response> DeleteCustomField(string customFieldId);

        /// <summary>
        /// Creates a list
        /// </summary>
        /// <param name="name">The name of the list.</param>
        /// <returns>A Response object.</returns>
        Task<Response> CreateList(string name);

        /// <summary>
        /// Retrieves all the lists
        /// </summary>
        /// <returns>A Response object.</returns>
        Task<Response> GetAllLists();

        /// <summary>
        /// Retrieve a single list
        /// </summary>
        /// <param name="listId">The identifier of the list.</param>
        /// <returns>A Response object.</returns>
        Task<Response> GetList(int listId);

        /// <summary>
        /// Deletes multiple lists
        /// </summary>
        /// <param name="listsIds">The identifiers of the lists to delete.</param>
        /// <returns>A Response object.</returns>
        Task<Response> DeleteLists(IEnumerable<int> listsIds);

        /// <summary>
        /// Deletes a list
        /// </summary>
        /// <param name="listId">The identifier of the list to delete.</param>
        /// <returns>A Response object.</returns>
        Task<Response> DeleteList(int listId);

        /// <summary>
        /// Update a list
        /// </summary>
        /// <param name="listId">The identifier of the list.</param>
        /// <param name="newName">the new list name.</param>
        /// <returns>A Response object.</returns>
        Task<Response> UpdateList(int listId, string newName);

        /// <summary>
        /// Adds multiple recipients to a list
        /// </summary>
        /// <param name="listId">The identifier of the list.</param>
        /// <param name="recipients">The list of recipients.</param>
        /// <returns>A Response object.</returns>
        Task<Response> AddRecipientsToList(int listId, IEnumerable<string> recipients);

        /// <summary>
        /// Add a recipient to a list
        /// </summary>
        /// <param name="listId">The identifier of the list.</param>
        /// <param name="recipient">The recipient.</param>
        /// <returns>A Response object.</returns>
        Task<Response> AddRecipientToList(int listId, string recipient);

        /// <summary>
        /// Retrieve all recipients on a List.
        /// </summary>
        /// <returns>A Response object.</returns>
        Task<Response> GetAllListRecipients();

        /// <summary>
        /// Add a recipient to a list
        /// </summary>
        /// <param name="listId">The identifier of the list.</param>
        /// <param name="recipientId">The identifier of the recipient.</param>
        /// <returns>A Response object.</returns>
        Task<Response> DeleteRecipientFromList(int listId, int recipientId);

        Task<Response> UpdateRecipient();
    }
}