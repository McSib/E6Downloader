using System;
using System.Collections.Generic;
using NDesk.Options;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace E6Downloader
{
    public enum Level
    {
        Debug,
        Info,
        Warning,
        Error,
        Fatal
    }

    public static class ConsoleLogger
    {
        private static readonly LoggingConfiguration Config = new LoggingConfiguration();
        private static readonly FileTarget LogFile = new FileTarget("E6DL LogFile") {FileName = "log.txt"};
        private static readonly ConsoleTarget LogConsole = new ConsoleTarget("E6DL Console");

        private static Logger _logger;
        private static IEnumerable<string> _args;
        private static int _verbosity;

        public static void InitializeLogger(IEnumerable<string> args)
        {
            _args = args;
            DetermineLogLevel();
            AddRules();
            SetLogManagerConfig();
            _logger = LogManager.GetCurrentClassLogger();
        }

        public static void Print(string message, Level level)
        {
            switch (level)
            {
                case Level.Debug:
                    _logger.Debug(message);
                    break;
                case Level.Info:
                    _logger.Info(message);
                    break;
                case Level.Warning:
                    _logger.Warn(message);
                    break;
                case Level.Error:
                    _logger.Error(message);
                    break;
                case Level.Fatal:
                    _logger.Fatal(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }

        private static void ParseCommands(OptionSet options)
        {
            try
            {
                options.Parse(_args);
            }
            catch (OptionException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private static int CreateAndParseCommands()
        {
            OptionSet optionSet = new OptionSet
            {
                {"v", "Prints debug statements.", v => _verbosity++}
            };

            ParseCommands(optionSet);
            return _verbosity;
        }

        private static int DetermineLogLevel()
        {
            return CreateAndParseCommands();
        }

        private static void AddRules()
        {
            Config.AddRule(_verbosity == 1 ? LogLevel.Debug : LogLevel.Info,
                _verbosity == 1 ? LogLevel.Error : LogLevel.Fatal,
                LogConsole);
            Config.AddRule(LogLevel.Debug, LogLevel.Fatal, LogFile);
        }

        private static void SetLogManagerConfig()
        {
            LogManager.Configuration = Config;
        }
    }
}