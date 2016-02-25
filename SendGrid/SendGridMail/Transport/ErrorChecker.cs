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
            CheckForErrorsAsync(response).RunSynchronously();
        }

        public static async Task CheckForErrorsAsync(HttpResponseMessage response)
        {
            // Should this be disposed?
            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            CheckForErrors(response, stream);
        }

        private static void CheckForErrors(HttpResponseMessage response, Stream stream)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                using (var reader = XmlReader.Create(stream))
                {
                    while (reader.Read())
                    {
                        if (!reader.IsStartElement())
                        {
                            continue;
                        }

                        switch (reader.Name)
                        {
                            case "result":
                                continue;
                            case "message":
                                continue;
                            case "errors":
                                reader.ReadToFollowing("error");
                                var message = reader.ReadElementContentAsString("error", reader.NamespaceURI);
                                throw new InvalidApiRequestException(response.StatusCode, new[] { message }, response.ReasonPhrase);
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
}