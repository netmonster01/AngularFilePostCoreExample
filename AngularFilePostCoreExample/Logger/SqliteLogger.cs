using AngularFilePostCoreExample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AngularFilePostCoreExample.Logger
{
    public class SqliteLogger : ILogger
    {
        private readonly string _name;
        private readonly SqliteLoggerConfiguration _config;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public SqliteLogger(string name, SqliteLoggerConfiguration config)
        {
            _name = name;
            _config = config;
            //_httpContextAccessor = httpContextAccessor;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == _config.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (_config.EventId == 0 || _config.EventId == eventId.Id)
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = _config.Color;
                Console.WriteLine($"{logLevel.ToString()} - {eventId.Id} - {_name} - {formatter(state, exception)}");
                Console.ForegroundColor = color;
                // save to db.
                //var user = User.FindFirstValue(ClaimTypes.NameIdentifier) //_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                //if (user == null)
                //{
                //    user = "ANONYMOUS_USER";

                //}
                Log l = new Log(logLevel, formatter(state, exception), "", "");

            }
        }
    }
}
