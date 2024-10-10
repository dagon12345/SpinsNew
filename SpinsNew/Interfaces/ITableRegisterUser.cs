using SpinsNew.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpinsNew.Interfaces
{
    public interface ITableRegisterUser
    {
        Task<List<RegisterUsersViewModel>> DisplayRegisterModelAsync();
    }
}
