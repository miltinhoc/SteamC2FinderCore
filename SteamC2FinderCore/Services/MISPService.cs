using SteamC2FinderCore.Model.MISP;
using SteamC2FinderCore.Utils;

namespace SteamC2FinderCore.Services
{
    public class MISPService
    {
        public static void CreateFolder()
        {
            if (!Directory.Exists(Constants.MispFolder))
            {
                Directory.CreateDirectory(Constants.MispFolder);
            }
        }

        public static Root ConvertToMisp(HashSet<string>? c2Servers, string type, Func<string, string>? transform = null)
        {
            string eventUuid = Guid.NewGuid().ToString();

            Root root = new()
            {
                Event = new Event()
                {
                    Orgc = new Orgc
                    {
                        Name = "miltinhoc",
                        Uuid = Constants.Organization
                    },
                    Uuid = eventUuid,
                    Info = "C2 Servers extracted from Steam profiles",
                    Date = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                    ThreatLevelId = "3",
                    Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                    Attribute = []
                }
            };

            foreach (var c2Server in c2Servers!)
            {
                root.Event.Attribute.Add(new Model.MISP.Attribute
                {
                    Type = type,
                    Value = transform != null ? transform(c2Server) : c2Server,
                    Uuid = Guid.NewGuid().ToString(),
                    Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                    Comment = string.Empty
                });
            }

            return root;
        }
    }
}
