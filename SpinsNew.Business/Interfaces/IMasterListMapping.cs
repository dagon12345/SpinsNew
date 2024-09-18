using SpinsNew.Business.DTOs;
using SpinsNew.DataAccess.Models;

namespace SpinsNew.Business.Interfaces
{
    public interface IMasterListMapping : IReadServiceAsync<MasterListModel, MasterListModelDto>
    {
    }
}