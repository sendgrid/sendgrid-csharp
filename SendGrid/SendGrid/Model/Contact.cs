using Newtonsoft.Json;
using SendGrid.Utilities;
using System;

namespace SendGrid.Model
{
    public class Contact
    {
        [JsonProperty("created_at")]
        [JsonConverter(typeof(EpochConverter))]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("last_clicked")]
        [JsonConverter(typeof(EpochConverter))]
        public DateTime? LastClickedOn { get; set; }

        [JsonProperty("last_emailed")]
        [JsonConverter(typeof(EpochConverter))]
        public DateTime? LastEmailedOn { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("last_opened")]
        [JsonConverter(typeof(EpochConverter))]
        public DateTime? LastOpenedOn { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(EpochConverter))]
        public DateTime ModifiedOn { get; set; }

        [JsonProperty("custom_fields")]
        public CustomFieldMetadata[] CustomFields { get; set; }
    }
}
