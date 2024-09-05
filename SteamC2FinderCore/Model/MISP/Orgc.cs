using Newtonsoft.Json;

namespace SteamC2FinderCore.Model.MISP
{
    public class Orgc
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("uuid")]
        public string? Uuid { get; set; }
    }
}
