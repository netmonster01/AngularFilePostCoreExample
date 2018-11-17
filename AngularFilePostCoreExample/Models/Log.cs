using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static AngularFilePostCoreExample.Models.CustomEnums;

namespace AngularFilePostCoreExample.Models
{
    public class Log
    {
        [Key]
        public int LogId { get; set; }
        public LogType LogType { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        public string Source { get; set; }
        public DateTime DateCreated { get; set; }

        public bool Checked
        {
            get; set;
        }
    }
}
