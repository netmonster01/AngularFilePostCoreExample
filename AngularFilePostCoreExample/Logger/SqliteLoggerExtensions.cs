using AngularFilePostCoreExample.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularFilePostCoreExample.Logger
{
    public static class SqliteLoggerExtensions
    {
        public static ILoggerFactory AddSqliteLogger(this ILoggerFactory loggerFactory,  ILog logRepository)
        {
            loggerFactory.AddProvider(new SqliteLogProvider(logRepository));
            return loggerFactory;
        }
        //public static ILoggerFactory AddSqliteLogger(this ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        //{
        //    var config = new SqliteLoggerConfiguration();
        //    return loggerFactory.AddSqliteLogger(config, serviceProvider);
        //}
        //public static ILoggerFactory AddSqliteLogger(this ILoggerFactory loggerFactory, Action<SqliteLoggerConfiguration> configure, IServiceProvider serviceProvider)
        //{
        //    var config = new SqliteLoggerConfiguration();
        //    configure(config);
        //    return loggerFactory.AddSqliteLogger(config, serviceProvider);
        //}
    }
}
