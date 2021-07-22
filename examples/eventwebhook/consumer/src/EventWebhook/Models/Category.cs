using System.Text.Json.Serialization;
using Microsoft.Extensions.Primitives;

namespace EventWebhook.Models
{
    public class Category
    {
        public StringValues Value { get; }

        public Category(StringValues value, bool isArray)
        {
            Value = value;
            IsArray = isArray;
        }

        [JsonIgnore]
        public bool IsArray { get; }
    }
}
