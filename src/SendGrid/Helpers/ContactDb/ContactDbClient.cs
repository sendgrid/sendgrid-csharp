// <copyright file="SendGridClient.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SendGrid.Helpers.CustomDb;

namespace SendGrid.Helpers.ContactDb
{
    internal class ContactDbClient : IContactDbClient
    {
        private SendGridClient client;

        public ContactDbClient(SendGridClient client)
        {
            this.client = client;
        }

        public async Task<Response> AddRecipients(IEnumerable<Recipient> recipients)
        {
            var jsonData = JsonConvert.SerializeObject(recipients);
            return await client.RequestAsync(
                method: SendGridClient.Method.POST,
                urlPath: "contactdb/recipients",
                requestBody: jsonData);
        }

        public async Task<Response> AddRecipientsToList(int listId, IEnumerable<string> recipients)
        {
            var jsonData = JsonConvert.SerializeObject(recipients);
            return await client.RequestAsync(
                method: SendGridClient.Method.POST,
                urlPath: $"contactdb/lists/{listId}/recipients",
                requestBody: jsonData);
        }

        public async Task<Response> AddRecipientToList(int listId, string recipientId)
        {
            return await client.RequestAsync(
                method: SendGridClient.Method.POST,
                urlPath: $"contactdb/lists/{listId}/recipients{recipientId}");
        }

        public async Task<Response> CreateCustomField(string name, string type)
        {
            var data = new { name, type };
            var jsonData = JsonConvert.SerializeObject(data);

            return await client.RequestAsync(
                method: SendGridClient.Method.POST,
                urlPath: "contactdb/custom_fields",
                requestBody: jsonData);
        }

        public async Task<Response> CreateList(string name)
        {
            var data = new { name };
            var jsonData = JsonConvert.SerializeObject(data);

            return await client.RequestAsync(
                method: SendGridClient.Method.POST,
                urlPath: "contactdb/lists",
                requestBody: jsonData);
        }

        public async Task<Response> CreateSegment(string segment)
        {
            return await client.RequestAsync(
                method: SendGridClient.Method.POST,
                urlPath: "contactdb/segments",
                requestBody: segment);
        }

        public async Task<Response> DeleteCustomField(string customFieldId)
        {
            return await client.RequestAsync(
                method: SendGridClient.Method.DELETE,
                urlPath: $"contactdb/custom_fields/{customFieldId}");
        }

        public async Task<Response> DeleteList(int listId)
        {
            return await client.RequestAsync(
                method: SendGridClient.Method.DELETE,
                urlPath: $"contactdb/lists/{listId}");
        }

        public async Task<Response> DeleteLists(IEnumerable<int> listsIds)
        {
            var jsonData = JsonConvert.SerializeObject(listsIds);
            return await client.RequestAsync(
                method: SendGridClient.Method.DELETE,
                urlPath: "contactdb/lists",
                requestBody: jsonData);
        }

        public async Task<Response> DeleteRecipient(string recipientId)
        {
            return await client.RequestAsync(
                method: SendGridClient.Method.DELETE,
                urlPath: $"contactdb/recipients/{recipientId}");
        }

        public async Task<Response> DeleteRecipientFromList(int listId, string recipientId)
        {
            return await client.RequestAsync(
                method: SendGridClient.Method.DELETE,
                urlPath: $"contactdb/lists/{listId}/recipients/{recipientId}");
        }

        public async Task<Response> DeleteRecipients(IEnumerable<string> recipientsId)
        {
            var jsonData = JsonConvert.SerializeObject(recipientsId);
            return await client.RequestAsync(
                method: SendGridClient.Method.DELETE,
                urlPath: "contactdb/recipients",
                requestBody: jsonData);
        }

        public async Task<Response> DeleteSegment(string segmentId, bool deleteContacts)
        {
            string queryParams = $"{{'delete_contacts': '{deleteContacts}'}}";

            return await client.RequestAsync(
                method: SendGridClient.Method.DELETE,
                urlPath: $"contactdb/segments/{segmentId}",
                queryParams: queryParams);
        }

        public async Task<Response> GetAllCustomFields()
        {
            return await client.RequestAsync(
                method: SendGridClient.Method.GET,
                urlPath: "contactdb/custom_fields");
        }

        public async Task<Response> GetAllListRecipients(int listId, int page = 1, int pageSize = 100)
        {
            return await client.RequestAsync(
                method: SendGridClient.Method.GET,
                urlPath: $"contactdb/lists/{listId}/recipients/?page_size={pageSize}&page={page}");
        }

        public async Task<Response> GetAllLists()
        {
            return await client.RequestAsync(
                method: SendGridClient.Method.GET,
                urlPath: "contactdb/lists");
        }

        public async Task<Response> GetAllSegments()
        {
            return await client.RequestAsync(
                method: SendGridClient.Method.GET,
                urlPath: "contactdb/segments");
        }

        public async Task<Response> GetBillableRecipientsCount()
        {
            return await client.RequestAsync(
                method: SendGridClient.Method.GET,
                urlPath: "contactdb/recipients/billable_count");
        }

        public async Task<Response> GetCustomField(string customFieldId)
        {
            return await client.RequestAsync(
                method: SendGridClient.Method.GET,
                urlPath: $"contactdb/custom_fields/{customFieldId}");
        }

        public async Task<Response> GetList(int listId)
        {
            return await client.RequestAsync(
                method: SendGridClient.Method.GET,
                urlPath: $"contactdb/lists/{listId}");
        }

        public async Task<Response> GetRecipient(string recipientId)
        {
            return await client.RequestAsync(
                method: SendGridClient.Method.GET,
                urlPath: $"contactdb/recipients/{recipientId}");
        }

        public async Task<Response> GetRecipients(int pageNumber, int pageSize)
        {
            string queryParams = $"{{'page': {pageNumber},'page_size': {pageSize}}}";
            return await client.RequestAsync(
                method: SendGridClient.Method.GET,
                urlPath: "contactdb/recipients",
                queryParams: queryParams);
        }

        public async Task<Response> GetRecipientsCount()
        {
            return await client.RequestAsync(
                method: SendGridClient.Method.GET,
                urlPath: "contactdb/recipients/count");
        }

        public async Task<Response> GetRecipientsList(string recipientId)
        {
            return await client.RequestAsync(
                method: SendGridClient.Method.GET,
                urlPath: $"contactdb/recipients/{recipientId}/lists");
        }

        public async Task<Response> GetReservedFields()
        {
            return await client.RequestAsync(
                method: SendGridClient.Method.GET,
                urlPath: "contactdb/reserved_fields");
        }

        public async Task<Response> GetSegment(string segmentId)
        {
            return await client.RequestAsync(
                method: SendGridClient.Method.GET,
                urlPath: $"contactdb/segments/{segmentId}");
        }

        public async Task<Response> GetSegmentRecipients(string segmentId, int page, int pageSize)
        {
            string queryParams = $"{{'page': {page},'page_size': {pageSize}}}";
            return await client.RequestAsync(
                method: SendGridClient.Method.GET,
                urlPath: $"contactdb/segments/{segmentId}/recipients",
                queryParams: queryParams);
        }

        public async Task<Response> SearchRecipients(string queryParams)
        {
            return await client.RequestAsync(
                method: SendGridClient.Method.GET,
                urlPath: "contactdb/recipients/search",
                queryParams: queryParams);
        }

        public async Task<Response> UpdateList(int listId, string newName)
        {
            var data = new { name = newName };
            var jsonData = JsonConvert.SerializeObject(data);

            return await client.RequestAsync(
                method: SendGridClient.Method.PATCH,
                urlPath: $"contactdb/lists/{listId}",
                requestBody: jsonData);
        }

        public async Task<Response> UpdateRecipient(Recipient recipient)
        {
            var jsonData = JsonConvert.SerializeObject(recipient);
            return await client.RequestAsync(
                method: SendGridClient.Method.PATCH,
                urlPath: "contactdb/recipients",
                requestBody: jsonData);
        }

        public async Task<Response> UpdateSegment(string segmentId, string segment)
        {
            return await client.RequestAsync(
                method: SendGridClient.Method.PATCH,
                urlPath: $"contactdb/segments/{segmentId}",
                requestBody: segment);
        }
    }
}