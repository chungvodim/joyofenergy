using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JOIEnergy.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace JOIEnergy
{
    public class Program
    {
        public static string Namespace = typeof(Startup).Namespace;
        public static string AppName = Namespace.Contains(".") ? Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1) : Namespace;
        public static IConfiguration Configuration;

        public static void Main(string[] args)
        {
            Configuration = GetConfiguration(args);
            Log.Logger = CreateSerilogLogger(Configuration);
            Log.Information("Configuring web host ({ApplicationContext})...", Program.AppName);
            BuildWebHost(args).Run();
            Log.Information("Starting web host ({ApplicationContext})...", Program.AppName);
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        static IConfiguration GetConfiguration(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
                .AddCommandLine(args)
                .AddEnvironmentVariables();



            return builder.Build();
        }

        static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", Program.AppName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .ReadFrom.Configuration(configuration);
            return loggerConfiguration.CreateLogger();
        }
    }
}
