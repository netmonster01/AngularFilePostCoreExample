using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using static AngularFilePostCoreExample.Models.CustomEnums;

namespace AngularFilePostCoreExample.Models
{
    public class Log
    {
        public Log(LogLevel level, string message, string userId, string source)
        {
            LogType = level;
            Message = message;
            UserId = userId;
            Source = source;
        }
        public Log(){}
        [Key]
        public int LogId { get; set; }
        public LogLevel LogType { get; set; }
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
