using AngularFilePostCoreExample.Interfaces;
using AngularFilePostCoreExample.ViewModels;
using AngularFilePostCoreExample.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using Serilog;

namespace AngularFilePostCoreExample.Converters
{
    public class ApplicationUserConverter : IConverter<RegisterUserViewModel, ApplicationUser>
    {
        private readonly ILogger _logger;
        public ApplicationUserConverter(ILogger logger)
        {
            _logger = logger;
            _logger.ForContext<ApplicationUserConverter>();
        }

        //public async Task<ApplicationUser> ConvertAsync(RegisterUserViewModel source_object)
        //{
        //    if (source_object == null)
        //    { return null; }

        //    try
        //    {
        //        ApplicationUser user = new ApplicationUser
        //        {
        //            Email = source_object.Email,
        //            FirstName = source_object.FirstName,
        //            LastName = source_object.LastName,
        //            UserName = source_object.UserName
        //        };

        //        // add image if it exists.
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            await source_object.AvatarImage.CopyToAsync(memoryStream);
        //            user.AvatarImage = memoryStream.ToArray();
        //        }
        //        return user;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }

        //}

        //public RegisterUserViewModel Convert(ApplicationUser source_object)
        //{
        //    throw new NotImplementedException();
        //}
        public ApplicationUser Convert(RegisterUserViewModel source_object)
        {
            return null;
        }

        public RegisterUserViewModel Convert(ApplicationUser source_object)
        {
            return null;
        }

        public async Task<ApplicationUser> ConvertAsync(RegisterUserViewModel source_object)
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
                if(source_object.AvatarImage != null)
                {
                    // add image if it exists.
                    using (var memoryStream = new MemoryStream())
                    {
                        await source_object.AvatarImage.CopyToAsync(memoryStream);
                        user.AvatarImage = memoryStream.ToArray();
                    }
                }
              
                return user;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to Convert user.");
                return null;
            }
        }

        public Task<RegisterUserViewModel> ConvertAsync(ApplicationUser source_object)
        {
            return null;
        }
    }
}
