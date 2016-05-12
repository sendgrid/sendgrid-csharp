using Newtonsoft.Json;

namespace SendGrid.Model
{
    public class ImportResult
    {
        [JsonProperty("error_count")]
        public int ErrorCount { get; set; }

        [JsonProperty("error_indices")]
        public int[] ErrorIndices { get; set; }

        [JsonProperty("new_count")]
        public int NewCount { get; set; }

        [JsonProperty("persisted_recipients")]
        public string[] PersistedRecipients { get; set; }

        [JsonProperty("updated_count")]
        public int UpdatedCount { get; set; }

        [JsonProperty("errors")]
        public Error[] Errors { get; set; }
    }
}
