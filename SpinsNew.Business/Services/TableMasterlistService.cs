using Microsoft.EntityFrameworkCore;
using SpinsNew.DataAccess;
using SpinsNew.DataAccess.Interfaces;
using SpinsNew.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinsNew.Business.Services
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
