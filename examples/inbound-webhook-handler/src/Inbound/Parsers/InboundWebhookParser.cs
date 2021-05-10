using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HttpMultipartParser;
using Inbound.Models;
using Inbound.Util;

namespace Inbound.Parsers
{
    public class InboundWebhookParser
    {
        public static async Task<InboundEmail> ParseAsync(Stream stream)
        {
            var payload = new MemoryStream();
            
            // https://docs.microsoft.com/dotnet/core/compatibility/aspnetcore#http-synchronous-io-disabled-in-all-servers
            await stream.CopyToAsync(payload);
            
            // It's important to rewind the stream
            payload.Position = 0;

            // Parse the multipart content received from SendGrid
            var parser = await MultipartFormDataParser.ParseAsync(payload);

            // Convert the 'headers' from a string into array of KeyValuePair
            var rawHeaders = parser
                .GetParameterValue("headers", string.Empty)
                .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var headers = rawHeaders
                .Select(header =>
                {
                    var splitHeader = header.Split(new[] {": "}, StringSplitOptions.RemoveEmptyEntries);
                    var key = splitHeader[0];
                    var value = splitHeader.Length > 1 ? splitHeader[1] : null;
                    return new KeyValuePair<string, string>(key, value);
                })
                .ToArray();

            // Raw email
            var rawEmail = parser.GetParameterValue("email", string.Empty);
            
            // Combine the 'attachment-info' and Files into an array of Attachments
            var attachmentInfoAsJsonElement = JsonDocument.Parse(parser.GetParameterValue("attachment-info", "{}")).RootElement;
            var attachments = new List<InboundEmailAttachment>();
            if (attachmentInfoAsJsonElement.ValueKind == JsonValueKind.Object)
            {
                foreach (var prop in attachmentInfoAsJsonElement.EnumerateObject())
                {
                    var attachment = ToObject<InboundEmailAttachment>(prop.Value);
                    attachment.Id = prop.Name;
                    var file = parser.Files.FirstOrDefault(f => f.Name == prop.Name);
                    if (file != null)
                    {
                        attachment.Data = file.Data;
                        if (string.IsNullOrEmpty(attachment.ContentType)) attachment.ContentType = file.ContentType;
                        if (string.IsNullOrEmpty(attachment.FileName)) attachment.FileName = file.FileName;
                    }
                    attachments.Add(attachment);
                }
            }

            // Convert the 'envelope' from a JSON string into a strongly typed object
            var envelope = JsonSerializer.Deserialize<InboundEmailEnvelope>(parser.GetParameterValue("envelope", "{}"));

            // Convert the 'charset' from a string into array of KeyValuePair
            var charsetsAsJsonElement = JsonDocument.Parse(parser.GetParameterValue("charsets", "{}")).RootElement;
            var charsets = new List<KeyValuePair<string, Encoding>>();
            if (charsetsAsJsonElement.ValueKind == JsonValueKind.Object)
            {
                foreach (var prop in charsetsAsJsonElement.EnumerateObject())
                {
                    var value = prop.Value.GetString();
                    if (string.IsNullOrWhiteSpace(value)) continue;
                    charsets.Add(new KeyValuePair<string, Encoding>(prop.Name,Encoding.GetEncoding(value)));
                }
            }

            // Create a dictionary of parsers, one parser for each desired encoding.
            // This is necessary because MultipartFormDataParser can only handle one
            // encoding and SendGrid can use different encodings for parameters such
            // as "from", "to", "text" and "html".
            var encodedParsers = charsets
                .Where(c => !Equals(c.Value, Encoding.UTF8))
                .Select(c => c.Value)
                .Distinct()
                .Select(encoding =>
                {
                    payload.Position = 0; // It's important to rewind the stream
                    return new
                    {
                        Encoding = encoding,
                        Parser = MultipartFormDataParser.Parse(payload)
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
            return new InboundEmail
            {
                Attachments = attachments.ToArray(),
                Charsets = charsets.ToArray(),
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
        }
        
        private static T ToObject<T>(JsonElement element, JsonSerializerOptions options = null)
        {
            using var buffer = new MemoryStream();
            using (var writer = new Utf8JsonWriter(buffer))
            {
                element.WriteTo(writer);
            }
            buffer.Seek(0, SeekOrigin.Begin);
            return JsonSerializer.Deserialize<T>(buffer.ToArray(), options);
        }
    }
}