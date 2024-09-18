using SpinsNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinsNew.Interfaces
{
    public interface ITableLog
    {

        Task<List<LogModel>> GetLogsAsync(int masterlistId);
        Task InsertLogs(int id, string currentStatus, string _username);
    }
}
