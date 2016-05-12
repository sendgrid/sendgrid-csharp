using Newtonsoft.Json;
using System;

namespace SendGrid.Model
{
    public class GlobalStat
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("stats")]
        public Stat[] Stats { get; set; }
    }
}
