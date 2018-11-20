using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using static AngularFilePostCoreExample.Models.CustomEnums;

namespace AngularFilePostCoreExample.Models
{
    public class Log
    {     
        [Key]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string Exception { get; set; }
        public string RenderedMessage { get; set; }
        public string Properties { get; set; }

    }
}
