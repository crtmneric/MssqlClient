using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace MssqlClient.Classes.Beans
{
    class Logger
    {
        public static void Setup()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout() { ConversionPattern = "%date [%thread] %-5level %logger - %message%newline" };
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender { AppendToFile = true, StaticLogFileName = true, File = @"Logs\Log.log", Layout = patternLayout, MaxSizeRollBackups = 5, MaximumFileSize = "5MB", RollingStyle = RollingFileAppender.RollingMode.Size };
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            MemoryAppender memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            hierarchy.Root.Level = Level.All;
            BasicConfigurator.Configure(hierarchy);
            hierarchy.Configured = true;
        }
    }
}
