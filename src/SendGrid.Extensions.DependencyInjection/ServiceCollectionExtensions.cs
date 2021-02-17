// <copyright file="ServiceCollectionExtensions.cs" company="Twilio SendGrid">
// Copyright (c) Twilio SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace SendGrid.Extensions.DependencyInjection
{
    /// <summary>
    /// extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="System.Net.Http.IHttpClientFactory"/> with <see cref="ISendGridClient"/> and related services to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="configureOptions">A delegate that is used to configure a <see cref="SendGridClientOptions"/>.</param>
        /// <returns>An <see cref="T:Microsoft.Extensions.DependencyInjection.IHttpClientBuilder" /> that can be used to configure the client.</returns>
        public static IHttpClientBuilder AddSendGrid(this IServiceCollection services, Action<IServiceProvider, SendGridClientOptions> configureOptions)
        {
            services.AddOptions<SendGridClientOptions>().Configure<IServiceProvider>((options, resolver) => configureOptions(resolver, options))
                .PostConfigure(options =>
                {
                    // validation
                    if (string.IsNullOrWhiteSpace(options.ApiKey))
                    {
                        throw new ArgumentNullException(nameof(options.ApiKey));
                    }
                });

            services.TryAddTransient<ISendGridClient>(resolver => resolver.GetRequiredService<InjectableSendGridClient>());

            return services.AddHttpClient<InjectableSendGridClient>();
        }

        /// <summary>
        /// Adds the <see cref="System.Net.Http.IHttpClientFactory"/> with <see cref="ISendGridClient"/> and related services to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="configureOptions">A delegate that is used to configure a <see cref="SendGridClientOptions"/>.</param>
        /// <returns>An <see cref="T:Microsoft.Extensions.DependencyInjection.IHttpClientBuilder" /> that can be used to configure the client.</returns>
        public static IHttpClientBuilder AddSendGrid(this IServiceCollection services, Action<SendGridClientOptions> configureOptions)
        {
            return services.AddSendGrid((_, options) => configureOptions(options));
        }
    }
}
