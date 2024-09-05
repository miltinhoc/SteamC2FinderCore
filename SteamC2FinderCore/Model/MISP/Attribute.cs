using Newtonsoft.Json;

namespace SteamC2FinderCore.Model.MISP
{
    public class Attribute
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; } = "Network activity";

        [JsonProperty("to_ids")]
        public bool ToIds { get; set; } = true;

        [JsonProperty("uuid")]
        public string? Uuid { get; set; }

        [JsonProperty("timestamp")]
        public string? Timestamp { get; set; }

        [JsonProperty("comment")]
        public string? Comment { get; set; }

        [JsonProperty("value")]
        public string? Value { get; set; }
    }
}
