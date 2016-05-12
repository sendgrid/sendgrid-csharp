using Newtonsoft.Json;

namespace SendGrid.Model
{
    public class CustomField<T> : CustomFieldMetadata
    {
        [JsonProperty("value")]
        public T Value { get; set; }
    }
}
