using System.Reflection;

namespace SteamC2FinderCore.Utils
{
    public class Constants
    {
        public static readonly string SteamUserProfilePattern = "class=\"searchPersonaName\" href=\"(.*?)\">(.*?)</a>";
        public static readonly string C2NamesPattern = "\\w+ (http:\\/\\/.+?)\\|";
        public static readonly string Organization = "af3dc5a1-5e31-40fc-9ac7-1aae6768cc32";
        public static readonly string MispFolder = "misp";
        public static readonly string C2Folder = "c2-lists";
        public static readonly string SearchFolder = "search-lists";

        public static readonly string? ProgramPath = Path.GetDirectoryName(AppContext.BaseDirectory);
    }
}
