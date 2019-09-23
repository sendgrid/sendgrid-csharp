﻿// <copyright file="SendGridClientOptions.cs" company="Twilio SendGrid">
// Copyright (c) Twilio SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SendGrid.Helpers.Reliability;
using System;
using System.Collections.Generic;

namespace SendGrid
{
    /// <summary>
    /// Defines the options to use with the Twilio SendGrid client.
    /// </summary>
    public class SendGridClientOptions
    {
        private ReliabilitySettings reliabilitySettings = new ReliabilitySettings();

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridClientOptions"/> class.
        /// </summary>
        public SendGridClientOptions()
        {
            this.RequestHeaders = new Dictionary<string, string>();
            this.Host = "https://api.sendgrid.com";
            this.Version = "v3";
        }

        /// <summary>
        /// Gets or sets the reliability settings to use on HTTP Requests.
        /// </summary>
        public ReliabilitySettings ReliabilitySettings
        {
            get => this.reliabilitySettings;

            set => this.reliabilitySettings = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the Twilio SendGrid API key.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the request headers to use on HttpRequests sent to Twilio SendGrid.
        /// </summary>
        public Dictionary<string, string> RequestHeaders { get; set; }

        /// <summary>
        /// Gets or sets base url (e.g. https://api.sendgrid.com, this is the default).
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets API version, override AddVersion to customize.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the path to the API endpoint.
        /// </summary>
        public string UrlPath { get; set; }
    }
}
