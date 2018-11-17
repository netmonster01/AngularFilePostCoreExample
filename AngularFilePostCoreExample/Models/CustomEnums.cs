using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularFilePostCoreExample.Models
{
    public class CustomEnums
    {
        public enum LogType
        {
            Error,
            Warning,
            Debug,
            Info,
            Pass,
            Fail
        }

        public enum RoleType
        {
            Admin,
            Reader,
            Publisher
        }
    }
}
