using System.Net;
using Newtonsoft.Json;

namespace SteamC2FinderCore.Services
{
    public class WebService
    {
        private readonly HttpClient _httpClient;
        private readonly CookieContainer _cookieContainer;
        private readonly HttpClientHandler _handler;

        public WebService()
        {
            _cookieContainer = new CookieContainer();

            _handler = new HttpClientHandler()
            {
                CookieContainer = _cookieContainer,
                UseCookies = true
            };

            _httpClient = new HttpClient(_handler);

            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/128.0.0.0 Safari/537.36");
        }

        public void AddHeader(string key, string value) => _httpClient.DefaultRequestHeaders.Add(key, value);

        public string? GetCookieValue(string cookieName)
        {
            var cookie = _cookieContainer?.GetCookies(new Uri("https://steamcommunity.com"))[cookieName];
            return cookie?.Value;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(responseContent);

                if (result is not null)
                {
                    return result;
                }
            }

            return Activator.CreateInstance<T>();
        }

        public async Task GetAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync();
            }
        }
    }
}
