using SteamC2FinderCore.Extensions;
using SteamC2FinderCore.Model.Steam;
using SteamC2FinderCore.Utils;
using System.Text.RegularExpressions;

namespace SteamC2FinderCore.Services
{
    public class SteamService
    {
        private readonly WebService _webService;
        private string? _sessionId;

        private readonly Dictionary<string, string>? _searchResults;
        private readonly HashSet<string>? _c2Servers;
        private readonly string _initTimestamp;

        public HashSet<string>? C2Servers 
        { 
            get
            {
                return _c2Servers;
            } 
        }

        public SteamService()
        {
            _webService = new WebService();

            _searchResults = [];
            _c2Servers = [];

            _initTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        }

        public async Task Start()
        {
            for (int i = 1; i <= 255; i++)
            {
                ProgressBarHelper.DisplayProgressBar(i, 255);

                await SearchForUsers($"http://{i}.", _sessionId);
                FileHelper.SaveResults($"search_history_{_initTimestamp}.json", Constants.SearchFolder, _searchResults);
            }

            Console.WriteLine("\n[*] Extracting c2 servers...");
            ExtractC2ServersFromNames();
            FileHelper.SaveResults($"c2_servers_{_initTimestamp}.json", Constants.C2Folder, _c2Servers);
        }

        private void ExtractC2ServersFromNames()
        {
            if (_searchResults == null)
            {
                return;
            }

            Regex regex = new(Constants.C2NamesPattern, RegexOptions.Compiled);

            foreach (string profile in _searchResults.Values)
            {
                Match match = regex.Match(profile);
                if (match.Success)
                {
                    string matchValue = match.Groups[1].Value.Trim();
                    string ipAddress = matchValue.GetIpAddress();
                    
                    if (ipAddress == null)
                    {
                        continue;
                    }

                    if (ipAddress.IsPublicIp())
                    {
                        _c2Servers?.Add(matchValue);
                    }
                }
            }
        }

        public async Task InitializeSession()
        {
            await _webService.GetAsync("https://steamcommunity.com/search/users/");

            _webService.AddHeader("X-Requested-With", "XMLHttpRequest");
            _sessionId = _webService.GetCookieValue("sessionid");
        }

        private static int CalculateNumberOfPages(int results) => (int)Math.Ceiling((double)results / 20);

        private async Task SearchForUsers(string searchTerm, string? sessionId)
        {
            SearchResult firstResponse = await GetSearchResult(1, sessionId, searchTerm);
            int numberOfPages = CalculateNumberOfPages(firstResponse.SearchResultCount);

            for (int i = 1; i <= numberOfPages; i++)
            {
                try
                {
                    SearchResult results = await GetSearchResult(i, sessionId, searchTerm);
                    ParseAndStoreUserNames(results.Html);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async Task<SearchResult> GetSearchResult(int pageNumber, string? sessionId, string searchTerm)
        {
            string url = $"https://steamcommunity.com/search/SearchCommunityAjax?text={Uri.EscapeDataString(searchTerm)}&filter=users&sessionid={sessionId}&steamid_user=false";

            if (pageNumber != 1) 
            {
                url += $"&page={pageNumber}";
            }

            return await _webService.GetAsync<SearchResult>(url);
        }

        private void ParseAndStoreUserNames(string? htmlContent)
        {
            if (string.IsNullOrEmpty(htmlContent) || _searchResults == null)
            {
                return;
            }

            MatchCollection matches = Regex.Matches(htmlContent, Constants.SteamUserProfilePattern, RegexOptions.Multiline | RegexOptions.Compiled);

            foreach (Match match in matches)
            {
                if (!match.Success)
                    continue;

                var steamId = match.Groups[1].Value.Split('/').LastOrDefault();

                if (steamId == null)
                {
                    continue;
                }

                if (_searchResults!.ContainsKey(steamId))
                {
                    continue;
                }

                _searchResults?.Add(steamId, match.Groups[2].Value);
            }
        }

    }
}
