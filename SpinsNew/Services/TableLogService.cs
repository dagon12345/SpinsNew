using Microsoft.EntityFrameworkCore;
using SpinsNew.Data;
using SpinsNew.Interfaces;
using SpinsNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinsNew.Services
{
    public class TableLogService : ITableLog
    {
        public async Task<List<LogModel>> GetLogsAsync(int masterlistId)
        {
            using(var context = new ApplicationDbContext())
            {
                var getLogs = await context.log_masterlist
                    .Include(m => m.masterListModel)
                    .Where(l => l.MasterListId == masterlistId)
                    .OrderByDescending(i => i.Id)
                    .AsNoTracking()
                    .ToListAsync();
                return getLogs;//return if it is a list.
            }

        }

        public async Task InsertLogs(int id, string currentStatus, string _username)
        {
            using (var context = new ApplicationDbContext())
            {
                //Save to logs below
                var savetoLogs = new LogModel();
                savetoLogs.MasterListId = id;
                savetoLogs.Log = currentStatus;
                savetoLogs.User = _username;
                savetoLogs.DateTimeEntry = DateTime.Now;
                context.Add(savetoLogs);
                await context.SaveChangesAsync();
            }
           
        }
    }
}
