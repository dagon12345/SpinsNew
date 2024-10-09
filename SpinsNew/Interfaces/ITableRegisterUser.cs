using SpinsNew.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpinsNew.Interfaces
{
    public interface ITableRegisterUser
    {
        Task<List<RegisterModel>> DisplayRegisterModelAsync();
    }
}
