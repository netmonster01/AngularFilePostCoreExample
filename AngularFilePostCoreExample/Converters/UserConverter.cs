using AngularFilePostCoreExample.Interfaces;
using AngularFilePostCoreExample.ViewModels;
using AngularFilePostCoreExample.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace AngularFilePostCoreExample.Converters
{
    public class UserConverter : IConverter<UserViewModel, ApplicationUser>
    {
        public UserConverter()
        {
        }

        public ApplicationUser Convert(UserViewModel source_object)
        {
            if (source_object == null)
            { return null; }

            try
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = source_object.Email,
                    FirstName = source_object.FirstName,
                    LastName = source_object.LastName,
                    UserName = source_object.UserName
                };

                // is avatar image attached?
                if (source_object.AvatarImage != null)
                {
                    // add image if it exists.
                    user.AvatarImage = System.Convert.FromBase64String(source_object.AvatarImage);
                }

                // get roles. may not want to do this in this convert as this is read only for now.
                List<ApplicationUserRole> roles = new List<ApplicationUserRole>();
                if (source_object.Roles.Any())
                {
                    foreach (var item in source_object.Roles)
                    {
                        roles.Add(new ApplicationUserRole { Role = new ApplicationRole() { Name = item } });
                    }

                    if (roles.Any())
                    {
                        user.UserRoles = roles;
                    }

                }

                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public UserViewModel Convert(ApplicationUser source_object)
        {
            if (source_object == null)
            { return null; }

            try
            {
                UserViewModel user = new UserViewModel
                {
                    Email = source_object.Email,
                    FirstName = source_object.FirstName,
                    LastName = source_object.LastName,
                    UserName = source_object.UserName,
                    Roles = source_object.UserRoles.Select(r => r.Role.Name).ToList()
                };

                // is avatar image attached?
                if (source_object.AvatarImage != null)
                {
                    // add image if it exists.
                    user.AvatarImage = string.Format("data:image/png;base64,{0}", System.Convert.ToBase64String(source_object.AvatarImage));
                }

                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<ApplicationUser> ConvertAsync(UserViewModel source_object)
        {
           
            throw new NotImplementedException();
        }

        public Task<UserViewModel> ConvertAsync(ApplicationUser source_object)
        {
            throw new NotImplementedException();
        }
    }
}
