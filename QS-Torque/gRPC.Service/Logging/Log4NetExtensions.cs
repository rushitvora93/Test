using log4net.Core;
using Microsoft.Extensions.Logging;
using System;
using log4net;

namespace gRPC.Service.Logging
{
    public static class Log4NetExtensions
    {
        /// <summary>
        /// Adds the Log4Net services to the MS logging framework. Enables file logging with MS logging.
        /// </summary>
        public static ILoggingBuilder AddLog4Net(this ILoggingBuilder builder)
        {
            builder.AddProvider(new Log4NetProvider());
            return builder;
        }

        public static bool IsTraceEnabled(this ILog log)
        {
            return log.Logger.IsEnabledFor(Level.Trace);
        }

        public static void Trace(this ILog log, string message)
        {
            Trace(log, message, null!);
        }

        public static void Trace(this ILog log, string message, Exception exception)
        {
            log.Logger.Log(null, Level.Trace, message, exception);
        }
    }
}
