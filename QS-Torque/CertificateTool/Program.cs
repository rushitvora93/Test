using System;
using FrameworksAndDrivers.CertificateStoreAccess;
using Microsoft.Extensions.Logging;

namespace CertificateTool
{
    class Program
    {
        static int Main(string[] args)
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole();
            });
            ILogger logger = loggerFactory.CreateLogger<Program>();

            if (args.Length > 0)
            {
                var certhandler = new HumbleCertifcateHandler(logger);

                switch (args[0].ToUpper())
                {
                    case "ADDCACERT":
                    {
                        if (args.Length > 1)
                        {
                            return certhandler.AddCaCertificateToLocalMachine(args[1])?0:1;
                        }

                        break;
                    }
                    case "ADDSERVERCERT":
                    {
                        if (args.Length > 2)
                        {
                            return certhandler.AddServerCertificateToLocalMachine(args[1],args[2]) ? 0 : 1;
                        }
                        break;
                    }
                    case "REMOVECACERT":
                    {
                        if (args.Length > 1)
                        {
                            return certhandler.RemoveCaCertificateFromLocalMachine(args[1]) ? 0 : 1;
                        }
                        break;
                    }
                   
                    case "REMOVESERVERCERT":
                    {
                        if (args.Length > 1)
                        {
                            return certhandler.RemoveServerCertificateFromLocalMachine(args[1]) ? 0 : 1;
                        }
                        break;
                    }
                }
            }
            logger.LogError("invalid argument");
            Console.WriteLine("usage:");
            Console.WriteLine("addCaCert pathToCrtFile");
            Console.WriteLine("addServerCert pathToP12File passwordForPk12File");
            Console.WriteLine("removeCaCert thumbprintFromCaCert");
            Console.WriteLine("removeServerCert thumbprintFromServerCert");
            return 1;
        }
    }
}
