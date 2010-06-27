using System;
using System.IO;
using log4net;
using log4net.Config;
using NCommons.Logging;
using NCommons.Logging.Log4Net;

namespace LoggingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            ILoggingService loggingService = CreateLoggingService();
            loggingService.Write("this is a test");
            Console.ReadLine();
        }

        static ILoggingService CreateLoggingService()
        {
            // Create and configure a log4net logger
            ILog log = LogManager.GetLogger("Default");
            XmlConfigurator.Configure(new FileInfo("log4net.xml"));

            // Create the LoggingService
            var logger = new Log4NetLogger(new Log4NetLogWriterMap(log));
            return new LoggingService(logger);
        }
    }
}