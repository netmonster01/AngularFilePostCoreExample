using Microsoft.AspNetCore.Http;

namespace AngularFilePostCoreExample.ViewModels
{
    public class RegisterUserViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile AvatarImage { get; set; }
    }
}
