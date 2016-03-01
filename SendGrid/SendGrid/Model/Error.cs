using Newtonsoft.Json;

namespace SendGrid.Model
{
    public class Error
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("error_indices")]
        public int[] ErrorIndices { get; set; }
    }
}
