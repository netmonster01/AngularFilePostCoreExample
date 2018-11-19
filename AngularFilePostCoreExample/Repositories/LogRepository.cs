using AngularFilePostCoreExample.Data;
using AngularFilePostCoreExample.Interfaces;
using AngularFilePostCoreExample.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularFilePostCoreExample.Repositories
{
    public  class LogRepository : ILog
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public LogRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Add(EventLog log)
        {
            // write log.
            try
            {
                _applicationDbContext.EventLogs.Add(log);
                _applicationDbContext.SaveChanges();

            }
            catch (Exception ex)
            {

            }
        }

        public async void AddAsync(EventLog log)
        {
            // write log.
            try
            {
                await _applicationDbContext.EventLogs.AddAsync(log);
                await _applicationDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

            }
        }

        public void Delete()
        {
           _applicationDbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE EventLogs");
        }

        public List<EventLog> Get()
        {
            try
            {
                List<EventLog> eventLogs = _applicationDbContext.EventLogs.ToList();
                return eventLogs;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<EventLog>> GetAsync()
        {
            try
            {
                List<EventLog> eventLogs = await _applicationDbContext.EventLogs.ToListAsync();
                return eventLogs;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
