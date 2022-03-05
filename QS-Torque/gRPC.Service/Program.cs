using System;
using System.IO;
using FrameworksAndDrivers.CertificateStoreAccess;
using gRPC.Service.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace gRPC.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    using var loggerFactory = LoggerFactory.Create(builder =>
                    {
                        builder.AddLog4Net();
                        builder.AddConsole();
                    });

                    var config = new ConfigurationBuilder()
                        .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"), optional: false)
                        .Build();

                    string fingerprint = (string)config.GetValue(typeof(string),"ServerCertificateFingerPrint");

                    ILogger logger = loggerFactory.CreateLogger<Program>();
                    HumbleCertifcateHandler certifcateHandler = new HumbleCertifcateHandler(logger);

                    // Enables log4net file logging
                    webBuilder.ConfigureLogging(builder =>
                    {
                        builder.AddConsole();
                        builder.AddLog4Net();
                    });

                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.ConfigureHttpsDefaults(listenOptions =>
                        {
                            listenOptions.ServerCertificate = certifcateHandler.FindServerCertificateByThumbPrint(fingerprint);
                        });
                    });

                    webBuilder.ConfigureKestrel((context, options) =>
                    {
                       options.Limits.MaxRequestBodySize = long.MaxValue;
                    }).UseStartup<Startup>();
                });
    }
}
