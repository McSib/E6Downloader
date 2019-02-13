using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace E6Downloader
{
    /// <summary>
    /// Main entry point of program.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            SetupLogger(args);
            RunSetupAndConfiguration();
        }

        private static void SetupLogger(IEnumerable<string> args)
        {
            ConsoleLogger.InitializeLogger(args);
            ConsoleLogger.Print("Logger Initialized...", Level.Debug);
        }

        private static void RunSetupAndConfiguration()
        {
            GeneralSetup setup = new GeneralSetup();
            RunSetupIfNoConfig(setup);
            SetConfiguration();
            setup.CreateDownloadDirectory();
        }

        private static void SetConfiguration()
        {
            Config.Configuration = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Config.ConfigName));
        }

        private static void RunSetupIfNoConfig(GeneralSetup setup)
        {
            if (!File.Exists(Config.ConfigName))
            {
                setup.RunNewSetup();
            }
        }
    }
}