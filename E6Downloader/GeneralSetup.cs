using System;
using System.IO;
using Newtonsoft.Json;

namespace E6Downloader
{
    public class GeneralSetup
    {
        private const string TagSource =
            "# Any line that starts with a pound sign is a comment and will be ignored by the program.\r\n" +
            "# Much like e621dl, this uses that same syntax, so feel free to list as many artist/pools/groups as you like.\r\n";

        public void RunNewSetup()
        {
            CreateTagFile();
            CreateConfigFile();
        }

        public void CreateDownloadDirectory()
        {
            Directory.CreateDirectory(Config.Configuration.DownloadDirectory);
        }

        private void CreateTagFile()
        {
            CreateFile("tag.txt", TagSource);
        }

        private void CreateConfigFile()
        {
            Config newConfig = new Config
            {
                CreateDirectories = true,
                DownloadDirectory = "download/",
                LastRun = $"{DateTime.Today:yyyy/MM/dd}",
                PartUsedAsName = "md5"
            };

            CreateFile(Config.ConfigName, JsonConvert.SerializeObject(newConfig, Formatting.Indented));
        }

        private static void CreateFile(string fileName, string data)
        {
            StreamWriter writer = new StreamWriter(fileName);
            writer.Write(data);
            writer.Close();
        }
    }
}