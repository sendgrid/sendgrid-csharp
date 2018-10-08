// <copyright file="IContactDbClient.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using SendGrid.Helpers.CustomDb;

namespace SendGrid.Helpers.ContactDb
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
        /// <param name="listId">The identifier of the list.</param>
        /// <param name="page">Page index of first recipient to return (must be a positive integer)</param>
        /// <param name="pageSize">Number of recipients to return at a time (must be a positive integer between 1 and 1000)</param>
        /// <returns>A Response object.</returns>
        Task<Response> GetAllListRecipients(int listId, int page = 1, int pageSize = 100);

        /// <summary>
        /// Add a recipient to a list
        /// </summary>
        /// <param name="listId">The identifier of the list.</param>
        /// <param name="recipientId">The identifier of the recipient.</param>
        /// <returns>A Response object.</returns>
        Task<Response> DeleteRecipientFromList(int listId, string recipientId);

        /// <summary>
        /// Update recipient
        /// </summary>
        /// <param name="recipient">Recipient to update in the database.</param>
        /// <returns>A Response object.</returns>
        Task<Response> UpdateRecipient(Recipient recipient);

        /// <summary>
        /// Add recipients
        /// </summary>
        /// <param name="recipients">The list of recipients to add to the database.</param>
        /// <returns>A Response object.</returns>
        Task<Response> AddRecipients(IEnumerable<Recipient> recipients);

        /// <summary>
        /// Retrieves the list of recipients.
        /// </summary>
        /// <param name="pageNumber">The number of the page to retrieve.</param>
        /// <param name="pageSize">The number of items in the page.</param>
        /// <returns>A Response object.</returns>
        Task<Response> GetRecipients(int pageNumber, int pageSize);

        /// <summary>
        /// Delete recipients.
        /// </summary>
        /// <param name="recipientsId">The list of identifiers of the recipients to delete.</param>
        /// <returns>A Response object.</returns>
        Task<Response> DeleteRecipients(IEnumerable<string> recipientsId);

        /// <summary>
        /// Retrieve a count of billable recipients.
        /// </summary>
        /// <returns>A Response object.</returns>
        Task<Response> GetBillableRecipientsCount();

        /// <summary>
        /// Retrieve a count of recipients.
        /// </summary>
        /// <returns>A Response object.</returns>
        Task<Response> GetRecipientsCount();

        /// <summary>
        /// Retrieve recipients matching search criteria.
        /// </summary>
        /// <param name="queryParams">Retrieve recipients matching search criteria.</param>        
        /// <returns>A Response object.</returns>
        Task<Response> SearchRecipients(string queryParams);

        /// <summary>
        /// Retrieve a single recipient.
        /// </summary>
        /// <param name="recipientId">The identifier of the recipient to retrieve.</param>        
        /// <returns>A Response object.</returns>
        Task<Response> GetRecipient(string recipientId);

        /// <summary>
        /// Delete a recipient.
        /// </summary>
        /// <param name="recipientId">The identifier of the recipient to delete.</param>        
        /// <returns>A Response object.</returns>
        Task<Response> DeleteRecipient(string recipientId);

        /// <summary>
        /// Retrieve the lists that a recipient is on.
        /// </summary>
        /// <param name="recipientId">The identifier of the recipient.</param>        
        /// <returns>A Response object.</returns>
        Task<Response> GetRecipientsList(string recipientId);

        /// <summary>
        /// Retrieve reserved fields.
        /// </summary>
        /// <returns>A Response object.</returns>
        Task<Response> GetReservedFields();

        /// <summary>
        /// Create a segment.
        /// </summary>
        /// <param name="segment">The JSON representation of a segemnt.</param>        
        /// <returns>A Response object.</returns>
        Task<Response> CreateSegment(string segment);

        /// <summary>
        /// Retrieve all segments.
        /// </summary>
        /// <returns>A Response object.</returns>
        Task<Response> GetAllSegments();

        /// <summary>
        /// Update a segment.
        /// </summary>
        /// <param name="segmentId">The segment identifier.</param>        
        /// <param name="segment">The JSON representation of a segment.</param>        
        /// <returns>A Response object.</returns>
        Task<Response> UpdateSegment(string segmentId, string segment);

        /// <summary>
        /// Retrieve a segment.
        /// </summary>
        /// <param name="segmentId">The segment identifier.</param>        
        /// <returns>A Response object.</returns>
        Task<Response> GetSegment(string segmentId);

        /// <summary>
        /// Delete a segment.
        /// </summary>
        /// <param name="segmentId">The segment identifier.</param>
        /// <param name="deleteContacts">True to delete all contacts matching the segment in addition to deleting the segment.</param>
        /// <returns>A Response object.</returns>
        Task<Response> DeleteSegment(string segmentId, bool deleteContacts);

        /// <summary>
        /// Retrieve recipients on a segment.
        /// </summary>
        /// <param name="segmentId">The segment identifier.</param>
        /// <param name="page">Page index of first recipient to return (must be a positive integer)</param>
        /// <param name="pageSize">Number of recipients to return at a time (must be a positive integer between 1 and 1000)</param>
        /// <returns>A Response object.</returns>
        Task<Response> GetSegmentRecipients(string segmentId, int page, int pageSize);
    }
}