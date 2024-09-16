using Microsoft.EntityFrameworkCore;
using SpinsNew.Data;
using SpinsNew.Interfaces;
using SpinsNew.Libraries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpinsNew.Services
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
