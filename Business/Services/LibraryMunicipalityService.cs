using Microsoft.EntityFrameworkCore;
using SpinsNew.Interfaces;
using SpinsNew.Libraries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpinsNew.Services
{
    public class LibraryMunicipalityService : ILibraryMunicipality
    {
        public async Task<List<LibraryMunicipality>> GetMunicipalitiesAsync()
        {
            using(var context = new ApplicationDbContext())
            {
                var municipalityLists = await context.lib_city_municipality
               .Include(p => p.LibraryProvince)
               .AsNoTracking()
               .ToListAsync();

                return municipalityLists;
            }
            
          

        }
    }
}
