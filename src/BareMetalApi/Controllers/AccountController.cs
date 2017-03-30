// Identity Example from Microsoft Github
//https://github.com/aspnet/Identity/blob/dev/samples/IdentitySample.Mvc/Controllers/AccountController.cs
//TODO  *Implement External Login (fb,google,github)  *Email verification

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BareMetalApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace BareMetalApi.Controllers
{
    [Route("blog/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

         // POST: /Account/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<dynamic> Login([FromBody] ApplicationUser model)
        {
                
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.PasswordHash, isPersistent: true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                
                    return Ok();
                }
                return BadRequest();




                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                //if (req.Email == null || req.Password == null) return new { authenticated = false };
                //var result = await _signInManager.PasswordSignInAsync(req.Email, req.Password, isPersistent: true, lockoutOnFailure: false);
                //if (result.Succeeded)
                //{
                    //DateTime? expires = DateTime.UtcNow.AddMinutes(2);
                    //var token = GetToken(req.Email, expires);
                    //return new { authenticated = true, entityId = 1, token = token, tokenExpires = expires };
                //}

                //return new { authenticated = false };
        }



        // POST: /Account/register
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<dynamic> Register([FromBody] ApplicationUser model)
        {              
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                user.Claims.Add( new IdentityUserClaim<string>
                    {
                        ClaimType="external",
                        ClaimValue= "true"
                    });
                var result = await _userManager.CreateAsync(user, model.PasswordHash);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                
                    return Ok();
                }
                return BadRequest();
        }

          //
        // POST: /Account/LogOff
        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return Ok();
        }
    }
}
