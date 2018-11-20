using AngularFilePostCoreExample.Converters;
using AngularFilePostCoreExample.Data;
using AngularFilePostCoreExample.Interfaces;
using AngularFilePostCoreExample.Models;
using AngularFilePostCoreExample.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Text;
using Serilog;
using AngularFilePostCoreExample.Core;

namespace AngularFilePostCoreExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // get app settings
            IConfigurationSection appSettingsSection = Configuration.GetSection("AppSettings");

            //services.AddSingleton<ILogger, SqliteLogger>();
            // configure DI for application services
            services.Configure<AppSettings>(appSettingsSection);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // add our custom converter.services.AddScoped<IConverter<Vote, VoteDto>, VoteConverter>();
            services.AddScoped<IConverter<RegisterUserViewModel, ApplicationUser>, ApplicationUserConverter>();
            services.AddScoped<IConverter<UserViewModel, ApplicationUser>, UserConverter>();

            // set up database
            services.AddDbContext<ApplicationDbContext>(options =>
                            options.UseSqlite(Configuration.GetConnectionString("IdentityConnection")), ServiceLifetime.Transient);

            // add custom identity
            services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

            // configure jwt authentication
            AppSettings appSettings = appSettingsSection.Get<AppSettings>();
            byte[] securityKey = Encoding.UTF8.GetBytes(appSettings.Secret);
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(securityKey);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = appSettings.Audience,
                    ValidIssuer = appSettings.Issuer,
                    IssuerSigningKey = signingKey,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            // swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            // add logging
            services.AddSerilogServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        { 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }


        //private IServiceProvider ConfigureLogging(IServiceCollection services)
        //{

        //    //services.AddTransient<ISomeDependency, SomeDependency>();
        //    services.AddSingleton<ILogger, SqliteLogger>();
        //    IServiceProvider serviceProvider = services.BuildServiceProvider();

        //    var loggerFactory = new LoggerFactory();
        //    loggerFactory.AddProvider(new SqliteLogProvider(new SqliteLoggerConfiguration(), serviceProvider));

        //    return serviceProvider;
        //}
    }
}
