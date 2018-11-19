using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularFilePostCoreExample.Logger
{
    public static class SqliteLoggerExtensions
    {
        public static ILoggerFactory AddSqliteLogger(this ILoggerFactory loggerFactory, SqliteLoggerConfiguration config)
        {
            loggerFactory.AddProvider(new SqliteLoggerProvider(config));
            return loggerFactory;
        }
        public static ILoggerFactory AddSqliteConsoleLogger(this ILoggerFactory loggerFactory)
        {
            var config = new SqliteLoggerConfiguration();
            return loggerFactory.AddSqliteLogger(config);
        }
        public static ILoggerFactory AddSqliteConsoleLogger(this ILoggerFactory loggerFactory, Action<SqliteLoggerConfiguration> configure)
        {
            var config = new SqliteLoggerConfiguration();
            configure(config);
            return loggerFactory.AddSqliteLogger(config);
        }
    }
}
