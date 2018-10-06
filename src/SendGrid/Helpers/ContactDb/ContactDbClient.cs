// <copyright file="SendGridClient.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SendGrid
{
    internal class ContactDbClient : IContactDbClient
    {
        private HttpClient client;

        public ContactDbClient(HttpClient client)
        {
            this.client = client;
        }

        public Task<Response> AddRecipientsToList(int listId, IEnumerable<string> recipients)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> AddRecipientToList(int listId, string recipient)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> CreateCustomField(string name, string type)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> CreateList(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> DeleteCustomField(string customFieldId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> DeleteList(int listId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> DeleteLists(IEnumerable<int> listsIds)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> DeleteRecipientFromList(int listId, int recipientId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> GetAllCustomFields()
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> GetAllListRecipients()
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> GetAllLists()
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> GetCustomField(string customFieldId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> GetList(int listId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> UpdateList(int listId, string newName)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> UpdateRecipient()
        {
            throw new System.NotImplementedException();
        }
    }
}