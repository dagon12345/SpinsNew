using SpinsNew.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinsNew.Interfaces
{
    public interface ILibraryStatus
    {
        //Create a method to get the statuses
        Task<List<LibraryStatus>> GetLibraryStatusesAsync();
    }
}
