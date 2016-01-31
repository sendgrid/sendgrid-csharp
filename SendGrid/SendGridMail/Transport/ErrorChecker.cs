namespace SendGrid
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Xml;
    using Exceptions;

    public static class ErrorChecker
    {
        public static void CheckForErrors(HttpResponseMessage response)
        {
            CheckForErrors(response, response.Content.ReadAsStreamAsync().Result);
        }

        public static async Task CheckForErrorsAsync(HttpResponseMessage response)
        {
            CheckForErrors(response, await response.Content.ReadAsStreamAsync());
        }

        private static void CheckForErrors(HttpResponseMessage response, Stream stream)
        {
            if (response.StatusCode == HttpStatusCode.OK) return;

            using (var reader = XmlReader.Create(stream))
            {
                while (reader.Read())
                {
                    if (!reader.IsStartElement())
                        continue;

                    switch (reader.Name)
                    {
                        case "result":
                        case "message":
                            continue;

                        case "errors":
                            reader.ReadToFollowing("error");
                            var message = reader.ReadElementContentAsString("error", reader.NamespaceURI);
                            throw new InvalidApiRequestException(response.StatusCode, new[] {message},
                                response.ReasonPhrase);

                        case "error":
                            throw new ProtocolViolationException();

                        default:
                            throw new ArgumentException("Unknown element: " + reader.Name);
                    }
                }
            }
        }
    }
}