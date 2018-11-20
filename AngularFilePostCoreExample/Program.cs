using System;
using System.Threading.Tasks;
using AngularFilePostCoreExample.Core;
using AngularFilePostCoreExample.Data;
using AngularFilePostCoreExample.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using static AngularFilePostCoreExample.Models.CustomEnums;
using Log = Serilog.Log;

namespace AngularFilePostCoreExample
{
    public class Program
    {
        public static void Main(string[] args)
        {


            Log.Logger = RegisterSerilogServices.AddSerilogService();
            //CreateWebHostBuilder(args).Build().Run();
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                try
                {
                   // SeedRolesAsync(services.GetRequiredService<RoleManager<ApplicationRole>>());
                    //SeedData.InitializeAsync(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseSerilog()
                .UseStartup<Startup>();

        public static async Task SeedRolesAsync(RoleManager<ApplicationRole> roleManager)
        {
            string[] roleNames = Enum.GetNames(typeof(RoleType));

            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                // creating the roles and seeding them to the database
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new ApplicationRole { Name = roleName });
                }
            }
        }
    }

   
}
