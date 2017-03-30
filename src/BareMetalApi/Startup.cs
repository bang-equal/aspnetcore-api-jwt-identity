using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using BareMetalApi.Models;
using BareMetalApi.Repositories;
using BareMetalApi.Migrations;
using BareMetalApi.Repositories.Interfaces;

namespace BareMetalApi
{
    public partial class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IBlogArticleRepository, BlogArticleRepository>();

            //Gets connection string from appsettings.json
            string x = Environment.GetEnvironmentVariable("DATABASE_URL");
            string[] substrings = x.Split(':');
            string user = substrings[1].Substring(2);
            string database = substrings[substrings.Length - 1].Substring(5);
            string [] substrings2 = substrings[2].Split('@');
            string password = substrings2[0];
            string host = substrings2[1];
            string y = $"User ID={user};Password={password};Host={host};Port=5432;Database={database};Pooling=true";
            Console.WriteLine(y);
            services.AddDbContext<ApplicationDbContext>(
                opts => opts.UseNpgsql(y));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddMvcCore()
                .AddAuthorization(auth =>
                    {
                        // Enable the use of an [Authorize("Bearer")] attribute on methods and classes to protect.
                        auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                            .RequireAuthenticatedUser().Build());
                    })
                .AddJsonFormatters();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            loggerFactory.AddConsole();
            //loggerFactory.AddDebug();

            //Identity
            app.UseIdentity();

            //JWT
            ConfigureAuth(app);

            app.UseMvc();

            //Create DB on startup
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.Migrate();
                context.EnsureSeedData();
            }
        }
    }
}
