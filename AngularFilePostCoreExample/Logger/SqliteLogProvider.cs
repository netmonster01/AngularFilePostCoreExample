using AngularFilePostCoreExample.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularFilePostCoreExample.Logger
{
    public class SqliteLogProvider : ILoggerProvider
    {
        private readonly SqliteLoggerConfiguration _config;
        private readonly ConcurrentDictionary<string, SqliteLogger> _loggers = new ConcurrentDictionary<string, SqliteLogger>();
        private readonly ILog _loggerRepository;
        //public ILogger CreateLogger(string categoryName)
        //{
        //    return new Logger(categoryName, _repo);
        //}


        public SqliteLogProvider(ILog loggerRepository)
        {
            _loggerRepository = loggerRepository;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new SqliteLogger(categoryName, _loggerRepository);  //_loggers.GetOrAdd(categoryName, name => new SqliteLogger(name, _config, _serviceProvider));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
