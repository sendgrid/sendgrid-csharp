// <copyright file="InjectableSendGridClient.cs" company="Twilio SendGrid">
// Copyright (c) Twilio SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.Extensions.Options;
using System.Net.Http;

namespace SendGrid.Extensions.DependencyInjection
{
    /// <summary>
    /// A wrapped SendGridClient with single constructor to inject an <see cref="HttpClient"/> whose lifetime is managed externally, e.g. by an DI container.
    /// </summary>
    internal class InjectableSendGridClient : BaseClient
    {
        public InjectableSendGridClient(HttpClient? httpClient, IOptions<SendGridClientOptions> options)
            : base(httpClient, options.Value)
        {
        }
    }
}
