using Newtonsoft.Json;

namespace SteamC2FinderCore.Utils
{
    public class FileHelper
    {
        public static void CreateFolder(string folderName)
        {
            if (Constants.ProgramPath == null)
            {
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }

                return;
            }

            if (!Directory.Exists(Path.Combine(Constants.ProgramPath, folderName)))
            {
                Directory.CreateDirectory(Path.Combine(Constants.ProgramPath, folderName));
            }
        }

        public static void SaveResults(string fileName, string folderName, object? content)
        {
            if (Constants.ProgramPath == null)
            {
                File.WriteAllText(fileName, JsonConvert.SerializeObject(content, Formatting.Indented));
            }
            else
            {
                File.WriteAllText(Path.Combine(Constants.ProgramPath, folderName, fileName), JsonConvert.SerializeObject(content, Formatting.Indented));
            }
        }
    }
}
