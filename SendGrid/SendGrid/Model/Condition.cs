using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SendGrid.Model
{
    public class Condition
    {
        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("operator")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ConditionOperator Operator { get; set; }

        [JsonProperty("and_or")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ConditionLogicalConjunction AndOr { get; set; }
    }
}
