using HttpMultipartParser;

namespace Inbound.Util
{
    public static class Extensions
    {
        /// <summary>
        /// Returns the value of a parameter or the default value if it doesn't exist.
        /// </summary>
        /// <param name="parser">The parser.</param>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value of the parameter.</returns>
        public static string GetParameterValue(this MultipartFormDataParser parser, string name, string defaultValue)
        {
            return parser.HasParameter(name) ? parser.GetParameterValue(name) : defaultValue;
        }
    }
}
