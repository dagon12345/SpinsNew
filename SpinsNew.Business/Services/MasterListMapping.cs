using AutoMapper;
using SpinsNew.DataAccess.Interfaces;
using SpinsNew.Business.DTOs;
using SpinsNew.DataAccess.Models;
using SpinsNew.Business.Interfaces;

namespace SpinsNew.Business.Services
{
    public class MasterListMapping : ReadServiceAsync<MasterListModel, MasterListModelDto>, IMasterListMapping
    {
        public MasterListMapping(IGenericRepository<MasterListModel> genericRepository, IMapper mapper) : base(genericRepository, mapper)
        {

        }
    }
}
