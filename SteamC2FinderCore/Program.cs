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
            FileHelper.CreateFolder(Constants.MispFolder);
            FileHelper.CreateFolder(Constants.C2Folder);
            FileHelper.CreateFolder(Constants.SearchFolder);

            SteamService steamService = new();

            await steamService.InitializeSession();
            await steamService.Start();

            if (steamService.C2Servers == null)
            {
                return;
            }

            Func<string, string> toIp = str => str.GetIpAddress();

            Root mispUrls = MISPService.ConvertToMisp(steamService.C2Servers, "url");
            Root mispIps = MISPService.ConvertToMisp(steamService.C2Servers, "ip-dst", transform: toIp);

            FileHelper.SaveResults($"{mispUrls.Event?.Uuid}.json", Constants.MispFolder, mispUrls);
            FileHelper.SaveResults($"{mispIps.Event?.Uuid}.json", Constants.MispFolder, mispIps);
        }
    }
}
