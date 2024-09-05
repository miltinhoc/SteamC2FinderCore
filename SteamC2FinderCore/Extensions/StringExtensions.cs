using System.Net;

namespace SteamC2FinderCore.Extensions
{
    public static class StringExtensions
    {
        public static bool IsPublicIp(this string ipAddress)
        {
            if (IPAddress.TryParse(ipAddress, out IPAddress? ip))
            {
                if (IPAddress.IsLoopback(ip))
                    return false;

                byte[] bytes = ip.GetAddressBytes();
                return !(
                    (bytes[0] == 10) ||
                    (bytes[0] == 172 && (bytes[1] >= 16 && bytes[1] <= 31)) ||
                    (bytes[0] == 192 && bytes[1] == 168)
                );
            }
            return false;
        }

        public static string GetIpAddress(this string url)
        {
            try
            {
                Uri uri = new(url);
                string host = uri.Host;

                if (IPAddress.TryParse(host, out IPAddress? ip))
                    return host;
            }
            catch { }

            return string.Empty;
        }
    }
}
