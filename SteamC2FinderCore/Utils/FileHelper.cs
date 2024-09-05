using Newtonsoft.Json;

namespace SteamC2FinderCore.Utils
{
    public class FileHelper
    {
        public static void SaveResults(string fileName, object? content) => File.WriteAllText(fileName, JsonConvert.SerializeObject(content, Formatting.Indented));
    }
}
