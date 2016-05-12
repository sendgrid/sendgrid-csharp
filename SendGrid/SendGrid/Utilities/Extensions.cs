using System;
using System.ComponentModel;
using System.Net.Http;

namespace SendGrid.Utilities
{
    public static class Extensions
    {
        private static readonly DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime FromUnixTime(this long unixTime)
        {
            return EPOCH.AddSeconds(unixTime);
        }

        public static long ToUnixTime(this DateTime date)
        {
            return Convert.ToInt64((date.ToUniversalTime() - EPOCH).TotalSeconds);
        }

        public static string GetDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return value.ToString();

            var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes == null || attributes.Length == 0) return value.ToString();

            var descriptionAttribute = attributes[0] as DescriptionAttribute;
            return (descriptionAttribute == null ? value.ToString() : descriptionAttribute.Description);
        }

        public static void EnsureSuccess(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) return;

            var content = response.Content.ReadAsStringAsync().Result;
            if (response.Content != null) response.Content.Dispose();

            throw new Exception(content);
        }
    }
}
