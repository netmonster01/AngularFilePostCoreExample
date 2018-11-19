using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularFilePostCoreExample.Logger
{
    public class SqliteLoggerProvider : ILoggerProvider
    {
        private readonly SqliteLoggerConfiguration _config;
        private readonly ConcurrentDictionary<string, SqliteLogger> _loggers = new ConcurrentDictionary<string, SqliteLogger>();

        public SqliteLoggerProvider(SqliteLoggerConfiguration config)
        {
            _config = config;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new SqliteLogger(name, _config));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
