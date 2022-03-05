using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using log4net.Config;
using Microsoft.Extensions.Logging;

namespace gRPC.Service.Logging
{
    public class Log4NetProvider : ILoggerProvider
    {
        public const string Log4NetConfigFile = "ServerLogConfig.xml";
        private readonly ConcurrentDictionary<string, Log4NetLogger> _loggers = new();

        static Log4NetProvider()
        {
            var confFilePath = Path.Combine(AppContext.BaseDirectory, Log4NetConfigFile);
            XmlConfigurator.ConfigureAndWatch(new FileInfo(confFilePath));
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _loggers.Clear();
        }

        /// <summary>
        /// Creates a new <see cref="T:Microsoft.Extensions.Logging.ILogger" /> instance.
        /// </summary>
        /// <param name="categoryName">The category name for messages produced by the logger.</param>
        /// <returns>The instance of <see cref="T:Microsoft.Extensions.Logging.ILogger" /> that was created.</returns>
        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, x => new Log4NetLogger(x));
        }
    }
}
