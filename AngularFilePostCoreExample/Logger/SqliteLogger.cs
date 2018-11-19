using AngularFilePostCoreExample.Interfaces;
using AngularFilePostCoreExample.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace AngularFilePostCoreExample.Logger
{
    public class SqliteLogger : ILogger
    {
        private readonly string _name;
       // private readonly IServiceProvider _serviceProvider;
        private readonly ILog _logRepository;

        public SqliteLogger(string name, ILog logRepository)
        {
            _name = name;
            _logRepository = logRepository;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new NoopDisposable();
        }
        private class NoopDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            EventLog eventLog = new EventLog(eventId.Id, logLevel, formatter(state, exception));
            _logRepository.Add(eventLog);  
        }
    }
}
