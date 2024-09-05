using SteamC2FinderCore.Extensions;
using SteamC2FinderCore.Model.MISP;
using SteamC2FinderCore.Services;
using SteamC2FinderCore.Utils;

namespace SteamC2FinderCore
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            SteamService steamService = new();

            await steamService.InitializeSession();
            await steamService.Start();

            if (steamService.C2Servers == null)
            {
                return;
            }

            Func<string, string> toIp = str => str.GetIpAddress();
            
            MISPService.CreateFolder();

            Root mispUrls = MISPService.ConvertToMisp(steamService.C2Servers, "url");
            Root mispIps = MISPService.ConvertToMisp(steamService.C2Servers, "ip-dst", transform: toIp);

            FileHelper.SaveResults(Path.Combine(Constants.MispFolder, $"{mispUrls.Event?.Uuid}.json"), mispUrls);
            FileHelper.SaveResults(Path.Combine(Constants.MispFolder, $"{mispIps.Event?.Uuid}.json"), mispIps);
        }
    }
}
