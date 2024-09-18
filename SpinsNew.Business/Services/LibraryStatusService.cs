using Microsoft.EntityFrameworkCore;
using SpinsNew.DataAccess;
using SpinsNew.DataAccess.Interfaces;
using SpinsNew.DataAccess.Libraries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpinsNew.Business.Services
{
    public class LibraryStatusService : ILibraryStatus
    {
        public async Task<List<LibraryStatus>> GetLibraryStatusesAsync()
        {
           using (var context = new ApplicationDbContext())
            {
                var libraryStatus = await context.lib_status
                    .ToListAsync();

                return libraryStatus;
            }
        }
    }
}
