// <copyright file="ContentVerifier.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SendGrid.Helpers.Mail
{

    /// <summary>
    /// Checks string for forbidden content with supplied rules
    /// </summary>
    public class ContentVerifier
    {

        /// <summary>
        /// List that contains all rules that should be checked
        /// </summary>
        private readonly List<KeyValuePair<VerifyMethod, string>> verificationRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentVerifier"/> class.
        /// </summary>
        public ContentVerifier()
        {
            verificationRules = new List<KeyValuePair<VerifyMethod, string>>();
        }

        /// <summary>
        /// Adds a new Verificationrule to the list
        /// </summary>
        /// <param name="verifyMethod">The <see cref="VerifyMethod"/> which should be used for verification.</param>
        /// <param name="content">The regex to check for or the word to search for.</param>
        public void AddVerificationRule(VerifyMethod verifyMethod, string content)
        {
            verificationRules.Add(new KeyValuePair<VerifyMethod, string>(verifyMethod,content));
        }

        /// <summary>
        /// Clears all Verificationrules.
        /// </summary>
        public void ClearVerificationRules()
        {
            verificationRules.Clear();
        }

        /// <summary>
        /// Check given string for forbidden content.
        /// </summary>
        /// <param name="contentToVerify">String which should be checked.</param>
        public void VerifyString(string contentToVerify)
        {
            foreach (var rule in verificationRules)
            {
                switch (rule.Key)
                {
                    case VerifyMethod.STRING:
                        if (new Regex(".*" + rule.Value + ".*", RegexOptions.IgnoreCase).IsMatch(contentToVerify))
                        {
                            throw new ArgumentException("Message content contains forbidden String.");
                        }

                        break;

                    case VerifyMethod.REGEX:
                        if (new Regex(rule.Value).IsMatch(contentToVerify))
                        {
                            throw new ArgumentException("Message content contains forbidden pattern.");
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// The supported verification methods.
        /// </summary>
        public enum VerifyMethod
        {
            /// <summary>
            /// Check if content contains given string
            /// </summary>
            STRING,

            /// <summary>
            /// Check if content matches given regex
            /// </summary>
            REGEX
        }
    }
}