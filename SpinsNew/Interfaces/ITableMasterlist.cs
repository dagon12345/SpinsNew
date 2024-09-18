using SpinsNew.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpinsNew.Interfaces
{
    public interface ITableMasterlist
    {
        Task<List<MasterListViewModel>> GetMasterListModelsAsync(List<int> municipalities, List<int> status);
        Task UpdateAsync(int id, int statusId,
            string dateDeceased, string remarks,
            string exclusionBatch, DateTime? exclusionDate,
            DateTime? inclusionDate);
        Task SoftDeleteAsync(int Id, DateTime? dateDeleted, string _username);
        Task VerificationUpdateAsync(int Id, bool verify);
    }
}
