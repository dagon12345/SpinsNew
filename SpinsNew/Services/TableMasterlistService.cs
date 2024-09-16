using Microsoft.EntityFrameworkCore;
using SpinsNew.Data;
using SpinsNew.Interfaces;
using SpinsNew.Libraries;
using SpinsNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinsNew.Services
{
    public class TableMasterlistService : ITableMasterlist
    {
        public async Task<List<MasterListModel>> GetMasterListModelsAsync(List<int> municipalities)
        {
            using (var context = new ApplicationDbContext())
            {
                var masterList = await context.tbl_masterlist
                    .Where(m => municipalities.Contains(m.PSGCCityMun))
                    .ToListAsync();

                return masterList;
            }
        }

    }
}
