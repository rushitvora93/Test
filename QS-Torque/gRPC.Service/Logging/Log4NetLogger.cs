using log4net;
using Microsoft.Extensions.Logging;
using System;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace gRPC.Service.Logging
{
    public class Log4NetLogger : ILogger
    {
        /// <summary>
        /// Original Log4Net logger
        /// </summary>
        private readonly ILog _log;
        private readonly string _name;

        public Log4NetLogger(string name)
        {
            _name = name;
            _log = LogManager.GetLogger(_name);
        }

        /// <summary>Begins a logical operation scope.</summary>
        /// <param name="state">The identifier for the scope.</param>
        /// <typeparam name="TState">The type of the state to begin scope for.</typeparam>
        /// <returns>An <see cref="T:System.IDisposable" /> that ends the logical operation scope on dispose.</returns>
        public IDisposable BeginScope<TState>(TState state) =>
            new Log4NetNdcDisposable(Convert.ToString(state) ?? string.Empty);

        /// <summary>
        /// Checks if the given <paramref name="logLevel" /> is enabled.
        /// </summary>
        /// <param name="logLevel">Level to be checked.</param>
        /// <returns><c>true</c> if enabled.</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    return _log.IsFatalEnabled;
                case LogLevel.Error:
                    return _log.IsErrorEnabled;
                case LogLevel.Warning:
                    return _log.IsWarnEnabled;
                case LogLevel.Information:
                    return _log.IsInfoEnabled;
                case LogLevel.Debug:
                    return _log.IsDebugEnabled;
                case LogLevel.Trace:
                    return _log.IsTraceEnabled();
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }

        /// <summary>Writes a log entry.</summary>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="state">The entry to be written. Can be also an object.</param>
        /// <param name="exception">The exception related to this entry.</param>
        /// <param name="formatter">Function to create a <see cref="T:System.String" /> message of the <paramref name="state" /> and <paramref name="exception" />.</param>
        /// <typeparam name="TState">The type of the object to be written.</typeparam>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            var message = formatter(state, exception);
            switch (logLevel)
            {
                case LogLevel.Critical:
                    _log.Fatal(message);
                    break;
                case LogLevel.Error:
                    _log.Error(message);
                    break;
                case LogLevel.Warning:
                    _log.Warn(message);
                    break;
                case LogLevel.Information:
                    _log.Info(message);
                    break;
                case LogLevel.Debug:
                    _log.Debug(message);
                    break;
                case LogLevel.Trace:
                    _log.Trace(message);
                    break;
                default:
                    _log.Warn($"Encountered unknown log level {logLevel}, writing out as Info.");
                    _log.Info(message, exception);
                    break;
            }
        }
    }

    /// <summary>
    /// Simple scope-based logger-provider for log4net
    /// </summary>
    public class Log4NetNdcDisposable : IDisposable
    {
        public Log4NetNdcDisposable(string scope)
        {
            NDC.Push(scope);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            NDC.Pop();
        }
    }
}
