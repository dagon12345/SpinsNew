using Microsoft.EntityFrameworkCore;
using SpinsNew.DataAccess;
using SpinsNew.DataAccess.Interfaces;
using SpinsNew.DataAccess.Libraries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpinsNew.Business.Services
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
