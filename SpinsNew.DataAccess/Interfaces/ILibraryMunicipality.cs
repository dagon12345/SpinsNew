using SpinsNew.DataAccess.Libraries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpinsNew.DataAccess.Interfaces
{
    public interface ILibraryMunicipality
    {
        //Create a method to get the municipalities
        Task<List<LibraryMunicipality>> GetMunicipalitiesAsync();
    }
}
