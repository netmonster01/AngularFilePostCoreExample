using AngularFilePostCoreExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularFilePostCoreExample.Interfaces
{
    public interface ILog
    {
        Task<List<EventLog>> GetAsync();
        List<EventLog> Get();
        void Delete();
        void AddAsync(EventLog log);
        void Add(EventLog log);

    }
}
