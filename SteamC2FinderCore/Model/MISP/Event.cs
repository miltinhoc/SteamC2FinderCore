using Newtonsoft.Json;

namespace SteamC2FinderCore.Model.MISP
{
    public class Event
    {
        [JsonProperty("date")]
        public string? Date { get; set; }

        [JsonProperty("threat_level_id")]
        public string ThreatLevelId { get; set; } = "3";

        [JsonProperty("info")]
        public string? Info { get; set; }

        [JsonProperty("published")]
        public bool Published { get; set; } = false;

        [JsonProperty("uuid")]
        public string? Uuid { get; set; }

        [JsonProperty("analysis")]
        public string Analysis { get; set; } = "0";

        [JsonProperty("timestamp")]
        public string? Timestamp { get; set; }

        [JsonProperty("Orgc")]
        public Orgc? Orgc { get; set; }

        [JsonProperty("Attribute")]
        public List<Attribute> Attribute { get; set; } = new();
    }

}
