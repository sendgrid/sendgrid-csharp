using HttpMultipartParser;
using Inbound.Models;
using Inbound.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Inbound.Parsers
{
    public class InboundWebhookParser
    {
        private readonly Stream _payload;

        public InboundWebhookParser(Stream stream)
        {
            _payload = new MemoryStream();
            stream.CopyTo(_payload);
        }

        public InboundEmail Parse()
        {
            // It's important to rewind the stream
            _payload.Position = 0;

            // Parse the multipart content received from SendGrid
            var parser = new MultipartFormDataParser(_payload, Encoding.UTF8);

            // Convert the 'headers' from a string into array of KeyValuePair
            var rawHeaders = parser
                .GetParameterValue("headers", string.Empty)
                .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var headers = rawHeaders
                .Select(header =>
                {
                    var splitHeader = header.Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
                    var key = splitHeader[0];
                    var value = splitHeader.Length > 1 ? splitHeader[1] : null;
                    return new KeyValuePair<string, string>(key, value);
                }).ToArray();

            // Raw email
            var rawEmail = parser.GetParameterValue("email", string.Empty);
            
            // Combine the 'attachment-info' and Files into an array of Attachments
            var attachmentInfoAsJObject = JObject.Parse(parser.GetParameterValue("attachment-info", "{}"));
            var attachments = attachmentInfoAsJObject
                .Properties()
                .Select(prop =>
                {
                    var attachment = prop.Value.ToObject<InboundEmailAttachment>();
                    attachment.Id = prop.Name;

                    var file = parser.Files.FirstOrDefault(f => f.Name == prop.Name);
                    if (file != null)
                    {
                        attachment.Data = file.Data;
                        if (string.IsNullOrEmpty(attachment.ContentType)) attachment.ContentType = file.ContentType;
                        if (string.IsNullOrEmpty(attachment.FileName)) attachment.FileName = file.FileName;
                    }

                    return attachment;
                }).ToArray();

            // Convert the 'envelope' from a JSON string into a strongly typed object
            var envelope = JsonConvert.DeserializeObject<InboundEmailEnvelope>(parser.GetParameterValue("envelope", "{}"));

            // Convert the 'charset' from a string into array of KeyValuePair
            var charsetsAsJObject = JObject.Parse(parser.GetParameterValue("charsets", "{}"));
            var charsets = charsetsAsJObject
                .Properties()
                .Select(prop =>
                {
                    var key = prop.Name;
                    var value = Encoding.GetEncoding(prop.Value.ToString());
                    return new KeyValuePair<string, Encoding>(key, value);
                }).ToArray();

            // Create a dictionary of parsers, one parser for each desired encoding.
            // This is necessary because MultipartFormDataParser can only handle one
            // encoding and SendGrid can use different encodings for parameters such
            // as "from", "to", "text" and "html".
            var encodedParsers = charsets
                .Where(c => c.Value != Encoding.UTF8)
                .Select(c => c.Value)
                .Distinct()
                .Select(encoding =>
                {
                    _payload.Position = 0; // It's important to rewind the stream
                    return new
                    {
                        Encoding = encoding,
                        Parser = new MultipartFormDataParser(_payload, encoding)
                    };
                })
                .Union(new[]
                {
                    new { Encoding = Encoding.UTF8, Parser = parser }
                })
                .ToDictionary(ep => ep.Encoding, ep => ep.Parser);

            // Convert the 'from' from a string into an email address
            var rawFrom = InboundWebhookParserHelper.GetEncodedValue("from", charsets, encodedParsers, string.Empty);
            var from = InboundWebhookParserHelper.ParseEmailAddress(rawFrom);

            // Convert the 'to' from a string into an array of email addresses
            var rawTo = InboundWebhookParserHelper.GetEncodedValue("to", charsets, encodedParsers, string.Empty);
            var to = InboundWebhookParserHelper.ParseEmailAddresses(rawTo);

            // Convert the 'cc' from a string into an array of email addresses
            var rawCc = InboundWebhookParserHelper.GetEncodedValue("cc", charsets, encodedParsers, string.Empty);
            var cc = InboundWebhookParserHelper.ParseEmailAddresses(rawCc);

            // Arrange the InboundEmail
            var inboundEmail = new InboundEmail
            {
                Attachments = attachments,
                Charsets = charsets,
                Dkim = InboundWebhookParserHelper.GetEncodedValue("dkim", charsets, encodedParsers, null),
                Envelope = envelope,
                From = from,
                Headers = headers,
                Html = InboundWebhookParserHelper.GetEncodedValue("html", charsets, encodedParsers, null),
                SenderIp = InboundWebhookParserHelper.GetEncodedValue("sender_ip", charsets, encodedParsers, null),
                SpamReport = InboundWebhookParserHelper.GetEncodedValue("spam_report", charsets, encodedParsers, null),
                SpamScore = InboundWebhookParserHelper.GetEncodedValue("spam_score", charsets, encodedParsers, null),
                Spf = InboundWebhookParserHelper.GetEncodedValue("SPF", charsets, encodedParsers, null),
                Subject = InboundWebhookParserHelper.GetEncodedValue("subject", charsets, encodedParsers, null),
                Text = InboundWebhookParserHelper.GetEncodedValue("text", charsets, encodedParsers, null),
                To = to,
                Cc = cc,
                RawEmail = rawEmail
            };

            return inboundEmail;
        }
    }
}