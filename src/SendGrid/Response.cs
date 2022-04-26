﻿// <copyright file="Response.cs" company="Twilio SendGrid">
// Copyright (c) Twilio SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SendGrid
{
    /// <summary>
    /// The response received from an API call to Twilio SendGrid.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class.
        /// </summary>
        /// <param name="statusCode">https://docs.microsoft.com/dotnet/api/system.net.httpstatuscode.</param>
        /// <param name="responseBody">https://docs.microsoft.com/dotnet/api/system.net.http.httpcontent.</param>
        /// <param name="responseHeaders">https://docs.microsoft.com/dotnet/api/system.net.http.headers.httpresponseheaders.</param>
        public Response(HttpStatusCode statusCode, HttpContent? responseBody, HttpResponseHeaders responseHeaders)
        {
            this.StatusCode = statusCode;
            this.Body = responseBody;
            this.Headers = responseHeaders;
        }

        /// <summary>
        /// Gets or sets the status code returned from Twilio SendGrid.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets a value indicating whether Status Code of this response indicates success.
        /// </summary>
        public bool IsSuccessStatusCode => ((int)StatusCode >= 200) && ((int)StatusCode <= 299);

        /// <summary>
        /// Gets or sets the response body returned from Twilio SendGrid.
        /// <see href="https://docs.microsoft.com/dotnet/api/system.net.http.httpcontent"></see>
        /// </summary>
        public HttpContent? Body { get; set; }

        /// <summary>
        /// Gets or sets the response headers returned from Twilio SendGrid.
        /// <see href="https://docs.microsoft.com/dotnet/api/system.net.http.headers.httpresponseheaders"></see>
        /// </summary>
        public HttpResponseHeaders Headers { get; set; }

        /// <summary>
        /// Converts string formatted response body to a Dictionary.
        /// </summary>
        /// <param name="content">https://docs.microsoft.com/dotnet/api/system.net.http.httpcontent.</param>
        /// <returns>Dictionary object representation of HttpContent.</returns>
        public virtual async Task<Dictionary<string, dynamic>> DeserializeResponseBodyAsync(HttpContent? content = null)
        {
            content ??= Body;
            if (content is null)
            {
                return new Dictionary<string, dynamic>();
            }

            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(false);
            var dsContent = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(stringContent);
            return dsContent;
        }

        /// <summary>
        /// Converts string formatted response headers to a Dictionary.
        /// </summary>
        /// <param name="headers">https://docs.microsoft.com/dotnet/api/system.net.http.headers.httpresponseheaders.</param>
        /// <returns>Dictionary object representation of HttpResponseHeaders.</returns>
        public virtual Dictionary<string, string> DeserializeResponseHeaders(HttpResponseHeaders? headers = null)
        {
            var dsContent = new Dictionary<string, string>();

            headers ??= Headers;
            if (headers == null)
            {
                return dsContent;
            }

            foreach (var pair in headers)
            {
                dsContent.Add(pair.Key, pair.Value.First());
            }

            return dsContent;
        }
    }
}
