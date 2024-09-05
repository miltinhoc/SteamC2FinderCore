using Newtonsoft.Json;

namespace SteamC2FinderCore.Model.MISP
{
    public class Root
    {
        [JsonProperty("Event")]
        public Event? Event { get; set; }
    }
}
