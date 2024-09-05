using Newtonsoft.Json;

namespace SteamC2FinderCore.Model.Steam
{
    public class SearchResult
    {
        [JsonProperty("success")]
        public int Success { get; set; }

        [JsonProperty("search_text")]
        public string? SearchText { get; set; }

        [JsonProperty("search_result_count")]
        public int SearchResultCount { get; set; }

        [JsonProperty("search_filter")]
        public string? SearchFilter { get; set; }

        [JsonProperty("search_page")]
        public string? SearchPage { get; set; }

        [JsonProperty("html")]
        public string? Html { get; set; }
    }
}
