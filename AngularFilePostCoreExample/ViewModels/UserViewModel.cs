using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularFilePostCoreExample.ViewModels
{
    public class UserViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarImage { get; set; }
        public List<string> Roles { get; set; }
    }
}
