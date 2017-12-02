// <copyright file="Response.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>
    /// The response received from an API call to SendGrid
    /// </summary>
    public class Response
    {
        /// <summary>
        /// The status code returned from SendGrid.
        /// </summary>
        private HttpStatusCode statusCode;

        /// <summary>
        /// The response body returned from SendGrid.
        /// </summary>
        private HttpContent body;

        /// <summary>
        /// The response headers returned from SendGrid.
        /// </summary>
        private HttpResponseHeaders headers;

        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class.
        /// </summary>
        /// <param name="statusCode">https://msdn.microsoft.com/en-us/library/system.net.httpstatuscode(v=vs.110).aspx</param>
        /// <param name="responseBody">https://msdn.microsoft.com/en-us/library/system.net.http.httpcontent(v=vs.118).aspx</param>
        /// <param name="responseHeaders">https://msdn.microsoft.com/en-us/library/system.net.http.headers.httpresponseheaders(v=vs.118).aspx</param>
        public Response(HttpStatusCode statusCode, HttpContent responseBody, HttpResponseHeaders responseHeaders)
        {
            this.StatusCode = statusCode;
            this.Body = responseBody;
            this.Headers = responseHeaders;
        }

        /// <summary>
        /// Gets or sets the status code returned from SendGrid.
        /// </summary>
        public HttpStatusCode StatusCode
        {
            get
            {
                return this.statusCode;
            }

            set
            {
                this.statusCode = value;
            }
        }

        /// <summary>
        /// Gets or sets the response headers returned from SendGrid.
        /// </summary>
        public HttpContent Body
        {
            get
            {
                return this.body;
            }

            set
            {
                this.body = value;
            }
        }

        /// <summary>
        /// Gets or sets the response body returned from SendGrid.
        /// </summary>
        public HttpResponseHeaders Headers
        {
            get
            {
                return this.headers;
            }

            set
            {
                this.headers = value;
            }
        }

        /// <summary>
        /// Converts string formatted response body to a Dictionary.
        /// </summary>
        /// <param name="content">https://msdn.microsoft.com/en-us/library/system.net.http.httpcontent(v=vs.118).aspx</param>
        /// <returns>Dictionary object representation of HttpContent</returns>
        public virtual async Task<Dictionary<string, dynamic>> DeserializeResponseBodyAsync(HttpContent content)
        {
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(false);
            var dsContent = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(stringContent);
            return dsContent;
        }

        /// <summary>
        ///     Converts string formatted response headers to a Dictionary.
        /// </summary>
        /// <param name="content">https://msdn.microsoft.com/en-us/library/system.net.http.headers.httpresponseheaders(v=vs.118).aspx</param>
        /// <returns>Dictionary object representation of  HttpResponseHeaders</returns>
        public virtual Dictionary<string, string> DeserializeResponseHeaders(HttpResponseHeaders content)
        {
            var dsContent = new Dictionary<string, string>();
            foreach (var pair in content)
            {
                dsContent.Add(pair.Key, pair.Value.First());
            }

            return dsContent;
        }
    }
}
