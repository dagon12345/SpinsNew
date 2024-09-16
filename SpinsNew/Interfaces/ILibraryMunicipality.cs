using SpinsNew.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinsNew.Interfaces
{
    public interface ILibraryMunicipality
    {
        //Create a method to get the municipalities
        Task<List<LibraryMunicipality>> GetMunicipalitiesAsync();
    }
}
