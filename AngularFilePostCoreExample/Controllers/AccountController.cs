using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AngularFilePostCoreExample.Interfaces;
using AngularFilePostCoreExample.Models;
using AngularFilePostCoreExample.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

        public AccountController(IServiceProvider serviceProvider)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            _userConverter = serviceProvider.GetRequiredService<IConverter<UserViewModel, ApplicationUser>>();
            _registerConverter = serviceProvider.GetRequiredService<IConverter<RegisterUserViewModel, ApplicationUser>>();
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

    }
}
