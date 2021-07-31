using GenericLogger.Enums;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System;
using System.IO;

namespace GenericLogger
{
    public static class Logger
    {
        private static ILogger _log;
        private static IConfiguration _configuration;
        public static void Configure()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json",
                    optional: false)
                .Build();

            GenericErrorLogConfigure();
        }

        private static void GenericErrorLogConfigure()
        {
            LoggerConfiguration lc = new LoggerConfiguration()
                .WriteTo.File(_configuration.GetValue<string>("ErrorLogConfig:RollingFilePath"),
                outputTemplate: "{Timestamp:yyyy/MM/dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                rollingInterval: RollingInterval.Day,
                fileSizeLimitBytes: _configuration.GetValue<int>("ErrorLogConfig:FileSizeLimit"),
                rollOnFileSizeLimit: true);

            SetLogLevel(lc);

            _log = lc.CreateLogger();
        }

        private static void SetLogLevel(LoggerConfiguration lc)
        {
            EnumLogLevel logLevel = _configuration.GetValue<EnumLogLevel>("Logging:LogLevel:Default");

            switch (logLevel)
            {
                case EnumLogLevel.Debug: 
                    lc.MinimumLevel.Debug(); 
                    break;
                case EnumLogLevel.Information: 
                    lc.MinimumLevel.Information(); 
                    break;
                case EnumLogLevel.Warning: 
                    lc.MinimumLevel.Warning(); 
                    break;
                case EnumLogLevel.Error: 
                    lc.MinimumLevel.Error(); 
                    break;
                default:
                    lc.MinimumLevel.Information();
                    break;
            }
        }

        /// <summary>
        /// Error - Any exception occurs during any process
        /// </summary>
        /// <param name="message">Custom message to print</param>
        /// <param name="ex">Any Exception</param>
        public static void WriteError(string message, Exception ex)
        {
            _log.Write(LogEventLevel.Error, ex, message);
        }

        /// <summary>
        /// Error - Any exception occurs during any process
        /// </summary>
        /// <param name="message">Custom message to print</param>
        public static void WriteError(string message)
        {
            _log.Write(LogEventLevel.Error, message);
        }

        /// <summary>
        /// Warning - For any doubtful cases or any possible issues or degradation of any service/functionality
        /// </summary>
        /// <param name="message">Custom message to print</param>
        /// <param name="ex">Any Exception</param>
        public static void WriteWarning(string message, Exception ex)
        {
            _log.Write(LogEventLevel.Warning, ex, message);
        }

        /// <summary>
        /// Warning - For any doubtful cases or any possible issues or degradation of any service/functionality
        /// </summary>
        /// <param name="message">Custom message to print</param>
        public static void WriteWarning(string message)
        {
            _log.Write(LogEventLevel.Warning, message);
        }

        /// <summary>
        /// Information - Highlevel information which could be useful to debug/dry run
        /// </summary>
        /// <param name="message">Custom message to print</param>
        /// <param name="ex">Any Exception</param>
        public static void WriteInformation(string message, Exception ex)
        {
            _log.Write(LogEventLevel.Information, ex, message);
        }

        /// <summary>
        /// Information - Highlevel information which could be useful to debug/dry run
        /// </summary>
        /// <param name="message">Custom message to print</param>
        public static void WriteInformation(string message)
        {
            _log.Write(LogEventLevel.Information, message);
        }

        /// <summary>
        /// Debug - More detail information which could be useful to debug/dry run
        /// </summary>
        /// <param name="ex">Any Exception</param>
        /// <param name="message">Custom message to print</param>
        public static void WriteDebug(string message, Exception ex)
        {
            _log.Write(LogEventLevel.Debug, ex, message);
        }

        /// <summary>
        /// Debug - More detail information which could be useful to debug/dry run
        /// </summary>
        /// <param name="message">Custom message to print</param>
        public static void WriteDebug(string message)
        {
            _log.Write(LogEventLevel.Debug, message);
        }
    }
}
