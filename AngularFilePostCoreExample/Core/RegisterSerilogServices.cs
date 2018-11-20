using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularFilePostCoreExample.Core
{
    public static class RegisterSerilogServices
    {
        public static IServiceCollection AddSerilogServices(this IServiceCollection services)
        {
            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Verbose()
            Log.Logger = AddSerilogService();

            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();

            return services.AddSingleton(Log.Logger);
        }

        public static ILogger AddSerilogService()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.ColoredConsole()
                .WriteTo.SQLite(Environment.CurrentDirectory + @"/data/application.sqlite")
                .CreateLogger();
        }
    }
}
