//osya/ASPNetCoreAngular2YoExample
//https://github.com/osya/ASPNetCoreAngular2YoExample/blob/master/SimpleTokenProvider/CustomJwtDataFormat.cs

using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BareMetalApi.SimpleTokenProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using BareMetalApi.Models;

namespace BareMetalApi
{
    public partial class Startup
    {
        // The secret key every token will be signed with.
        // Keep this safe on the server!
        private const string SecretKey = "mysupersecret_secretkey!123";

        private static void ConfigureAuth(IApplicationBuilder app)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
            
            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = "ExampleIssuer",

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = "http://localhost:41224/",

                // Validate the token expiry
                ValidateLifetime = true,
                
                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero,
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            var _userManager = app.ApplicationServices.GetService<UserManager<ApplicationUser>>();

            app.UseSimpleTokenProvider(new TokenProviderOptions
            {
                Path = "/blog/account/login",
                Audience = "http://localhost:41224/",
                Issuer = "ExampleIssuer",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                IdentityResolver = (username, password) => GetIdentity(_userManager, username, password)
        });
        }

        private static async Task<ClaimsIdentity> GetIdentity(UserManager<ApplicationUser> userManager, string email, string password)
        {
            //Get exception here when trying to add connection string to DbContext in Startup
            //exception becuse usermanager is null when it is sent by delegate
            //originally I found this to be a problem with Identity sharing database with model data
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }
            var result = await userManager.CheckPasswordAsync(user, password);
            //Need to return a real identity with real claims
            return result ? new ClaimsIdentity(new GenericIdentity(email, "Token"), new[] { new Claim("user_name", user.UserName), new Claim("user_id", user.Id) }) : null;
        }
    }
}