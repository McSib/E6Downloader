namespace E6Downloader
{
    public class Config
    {
        public const string ConfigName = "config.txt";
        public static Config Configuration;

        public bool CreateDirectories;
        public string DownloadDirectory;
        public string LastRun;
        public string PartUsedAsName;
    }
}