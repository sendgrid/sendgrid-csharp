using HttpMultipartParser;
using Inbound.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Inbound.Util
{
    public static class InboundWebhookParserHelper
    {
        public static InboundEmailAddress[] ParseEmailAddresses(string rawEmailAddresses)
        {
            // Split on commas that have an even number of double-quotes following them
            const string SPLIT_EMAIL_ADDRESSES = ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)";

            /*
                When we stop supporting .NET 4.5.2 we will be able to use the following:
                if (string.IsNullOrEmpty(rawEmailAddresses)) return Array.Empty<InboundEmailAddress>();
            */
            if (string.IsNullOrEmpty(rawEmailAddresses)) return Enumerable.Empty<InboundEmailAddress>().ToArray();

            var rawEmails = Regex.Split(rawEmailAddresses, SPLIT_EMAIL_ADDRESSES);
            var addresses = rawEmails
                .Select(rawEmail => ParseEmailAddress(rawEmail))
                .Where(address => address != null)
                .ToArray();
            return addresses;
        }

        public static InboundEmailAddress ParseEmailAddress(string rawEmailAddress)
        {
            if (string.IsNullOrEmpty(rawEmailAddress))
            {
                return null;
            }

            var pieces = rawEmailAddress.Split(new[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries);

            if (pieces.Length == 0)
            {
                return null;
            }

            var email = pieces.Length == 2 ? pieces[1].Trim() : pieces[0].Trim();
            var name = pieces.Length == 2 ? pieces[0].Replace("\"", string.Empty).Trim() : string.Empty;
            return new InboundEmailAddress(email, name);
        }

        public static string GetEncodedValue(string parameterName, IEnumerable<KeyValuePair<string, Encoding>> charsets,
            IDictionary<Encoding, MultipartFormDataParser> encodedParsers, string defaultValue = null)
        {
            var parser = GetEncodedParser(parameterName, charsets, encodedParsers);
            var value = parser.GetParameterValue(parameterName, defaultValue);
            return value;
        }

        private static MultipartFormDataParser GetEncodedParser(string parameterName, IEnumerable<KeyValuePair<string, Encoding>> charsets,
            IDictionary<Encoding, MultipartFormDataParser> encodedParsers)
        {
            var encoding = GetEncoding(parameterName, charsets);
            var parser = encodedParsers[encoding];
            return parser;
        }

        private static Encoding GetEncoding(string parameterName, IEnumerable<KeyValuePair<string, Encoding>> charsets)
        {
            var encoding = charsets.Where(c => c.Key == parameterName);
            return encoding.Any() ? encoding.First().Value : Encoding.UTF8;
        }
    }
}
