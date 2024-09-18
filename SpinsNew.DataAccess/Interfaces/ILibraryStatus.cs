using SpinsNew.DataAccess.Libraries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpinsNew.DataAccess.Interfaces
{
    public interface ILibraryStatus
    {
        //Create a method to get the statuses
        Task<List<LibraryStatus>> GetLibraryStatusesAsync();
    }
}
