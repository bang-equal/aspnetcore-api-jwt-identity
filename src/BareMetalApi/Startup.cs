using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using BareMetalApi.Models;
using BareMetalApi.Repositories;
using BareMetalApi.Migrations;
using BareMetalApi.Repositories.Interfaces;
using BareMetalApi.Security;

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
            services.AddOptions();

            services.Configure<TokenAuthOption>(options =>
            {
                options.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Security:secret_key"])), SecurityAlgorithms.HmacSha256Signature);
            });
            
            //Gets connection string from appsettings.json
            string url = Environment.GetEnvironmentVariable("DATABASE_URL");
            string[] substrings = url.Split(':');
            string user = substrings[1].Substring(2);
            string database = substrings[substrings.Length - 1].Substring(5);
            string [] substrings2 = substrings[2].Split('@');
            string password = substrings2[0];
            string host = substrings2[1];
            string connstr = $"User ID={user};Password={password};Host={host};Port=5432;Database={database};Pooling=true";
            
            services.AddDbContext<ApplicationDbContext>(
                opts => opts.UseNpgsql(connstr));

            services.AddSingleton<IBlogArticleRepository, BlogArticleRepository>();

            //Gets connection string from appsettings.json
            services.AddDbContext<ApplicationDbContext>(
                opts => opts.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddMvcCore()
                .AddAuthorization(options =>
                    {
                        options.AddPolicy("Bearer", policy => {
                            policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​);
                            policy.RequireClaim("external", "true");
                            policy.RequireAuthenticatedUser().Build();
                            }); 
                    })
                .AddJsonFormatters();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            //loggerFactory.AddDebug();

            //Identity
            app.UseIdentity();

            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    //When this line commented, got invalid token audience error
                    ValidAudience = "MyAudience",
                    ValidIssuer = "MyIssuer",
                    // When receiving a token, check that we've signed it.
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Security:secret_key"])),
                    // When receiving a token, check that it is still valid.
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    // This defines the maximum allowable clock skew - i.e. provides a tolerance on the token expiry time 
                    // when validating the lifetime. As we're creating the tokens locally and validating them on the same 
                    // machines which should have synchronised time, this can be set to zero. Where external tokens are
                    // used, some leeway here could be useful.
                    ClockSkew = TimeSpan.FromMinutes(0)
                }
            });

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
