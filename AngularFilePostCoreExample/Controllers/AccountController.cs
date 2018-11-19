using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AngularFilePostCoreExample.Interfaces;
using AngularFilePostCoreExample.Models;
using AngularFilePostCoreExample.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using static AngularFilePostCoreExample.Models.CustomEnums;

namespace AngularFilePostCoreExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConverter<UserViewModel, ApplicationUser> _userConverter;
        private readonly IConverter<RegisterUserViewModel, ApplicationUser> _registerConverter;
        private readonly AppSettings _appSettings;
       private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public AccountController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<AccountController>>();
            _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            _userConverter = serviceProvider.GetRequiredService<IConverter<UserViewModel, ApplicationUser>>();
            _registerConverter = serviceProvider.GetRequiredService<IConverter<RegisterUserViewModel, ApplicationUser>>();
            _appSettings = serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value;
            _signInManager = serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>();

        }
        //[HttpPost]
        //[Route("Login")]
        //[AllowAnonymous]
        //public async Task<UserDto> Login([FromBody] LoginDto model)
        //{
        //    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

        //    if (result.Succeeded)
        //    {
        //        var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
        //        //_loggerRepository.Write(LogType.Pass, string.Format("User: {0} Logged In.", appUser.UserName));
        //        return await GenerateJwtToken(model.Email, appUser);
        //    }

        //    //_loggerRepository.Write(LogType.Fail, string.Format("User: {0} Failed to Log In.", model.Email));
        //    throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        //}

        [HttpGet]
        [Route("IsAuthenticated")]
        [AllowAnonymous]
        public IActionResult IsAuthenticated()
        {
            if (User.Identity.IsAuthenticated)
            {
             //   _loggerRepository.Write(LogType.Pass, string.Format("AuthCheck"));
            }
            return Ok(User.Identity.IsAuthenticated);
        }

        // GET: api/Account
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterUserViewModel model)
        {
            bool didCreate = false;

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                // convert dto to applicationUser.
                ApplicationUser newUser = await _registerConverter.ConvertAsync(model);


                // try to create user
                IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);

                // reget uesr
                ApplicationUser usr = _userManager.Users.Where(u => u.Email == newUser.Email).FirstOrDefault();

                if (result.Succeeded)
                {
                    // assign a default role.
                    IdentityResult roleCreate = await _userManager.AddToRoleAsync(usr, RoleType.Reader.ToString());

                    var x = _roleManager.GetRoleNameAsync(new ApplicationRole { Name = RoleType.Reader.ToString() });

                    if (roleCreate.Succeeded)
                    {
                        didCreate = true;
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(didCreate);
        }

        [HttpGet]
        [Route("Users")]
        public IEnumerable<UserViewModel> Get()
        {
            _logger.LogInformation("Get Users");
            try
            {
                List<UserViewModel> users = _userManager.Users.Include(u => u.UserRoles).ThenInclude(r => r.Role).Select(u=> _userConverter.Convert(u)).ToList();
                return users;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        [HttpGet]
        [Route("Users/{userName}")]
        public IEnumerable<UserViewModel> Get(string userName)
        {
            try
            {
                List<UserViewModel> users = _userManager.Users.Where(u=> u.UserName == userName).Include(ur => ur.UserRoles).Select(u => _userConverter.Convert(u)).ToList();
                return users;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// Generate jsonToken
        /// </summary>
        /// <param name="email"></param>
        /// <param name="appUser"></param>
        /// <returns></returns>
        private async Task<UserViewModel> GenerateJwtToken(string email, ApplicationUser appUser)
        {
            UserViewModel user = new UserViewModel
            {
                UserName = appUser.UserName
            };

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, email)
            };

            // get the users roles.
            var roles = await _userManager.GetRolesAsync(appUser);
            var u = await _userManager.FindByEmailAsync(appUser.UserName);
            if (u != null)
            {
                user.FirstName = u.FirstName;
                user.LastName = u.LastName;
            }
            if (!string.IsNullOrWhiteSpace(user.FirstName))
            {
                //            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                //    new Claim(ClaimTypes.GivenName, user.FirstName)
                //});
            }

            if (!string.IsNullOrWhiteSpace(user.LastName))
            {
                //            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                //     new Claim(ClaimTypes.Surname, user.LastName),
                //});
            }
            if (roles.Any())
            {
                claims.AddRange(roles.Select(role => new Claim("Role", role)));
                user.Roles = roles.ToList();
                user.IsAdmin = roles.Contains("Admin");
            }

            //security key
            var securityKey = Encoding.UTF8.GetBytes(_appSettings.Secret);//  Encoding.ASCII.GetBytes(_appSettings.Secret);
            //var signingKey = new SymmetricSecurityKey(securityKey);

            // SymmetricSecurityKey
            var symetricSecurityKey = new SymmetricSecurityKey(securityKey);

            // Signing Credentials
            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            // Create Token
            var token = new JwtSecurityToken(
                issuer: _appSettings.Issuer, //"netmon.in",
                audience: _appSettings.Audience, // "readers",
                expires: DateTime.Now.AddHours(7),
                signingCredentials: signingCredentials,
                claims: claims
                );

            var sToken = new JwtSecurityTokenHandler().WriteToken(token);

            user.Token = sToken;
            user.Email = appUser.UserName;

            user.Password = null;

            if (appUser.AvatarImage != null && appUser.AvatarImage.Length > 0)
            {
                user.AvatarImage = Convert.ToBase64String(appUser.AvatarImage);
            }
            user.Id = appUser.Id;
            //_loggerRepository.Write(LogType.Pass, string.Format("Generated Token {0} for User: {1} Registered.", user.Token, user.Email));
            return user;

        }
    }
}
