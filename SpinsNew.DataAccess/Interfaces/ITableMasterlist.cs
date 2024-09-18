using SpinsNew.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpinsNew.DataAccess.Interfaces
{
    public interface ITableMasterlist
    {
        Task<List<MasterListModel>> GetMasterListModelsAsync(List<int> municipalities);
    }
}
