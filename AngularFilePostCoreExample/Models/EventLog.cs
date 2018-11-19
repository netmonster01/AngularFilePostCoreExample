using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularFilePostCoreExample.Models
{
    public class EventLog
    {
        public EventLog()
        {
        }

        public EventLog(int eventId, LogLevel logLevel, string message)
        {
            EventId = eventId;
            LogLevel = logLevel.ToString();
            Message = message;
            CreatedTime = DateTime.Now;
        }

        public EventLog(int eventId, LogLevel logLevel, string message, string userName)
        {
            EventId = eventId;
            LogLevel = logLevel.ToString();
            Message = message;
            UserName = userName;
            CreatedTime = DateTime.Now;
        }


        [Key]
        public int Id { get; set; }
        public int? EventId { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string UserName { get; set; }

    }
}
