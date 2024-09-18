using AutoMapper;
using Business.DTOs;
using Business.Services;
using DataAccess.Interfaces;
using SpinsNew.Models;

namespace Business.Mappings
{
    public class MasterListMapping : ReadServiceAsync<MasterListModel, MasterListModelDto>, IMasterListMapping
    {
        public MasterListMapping(IGenericRepository<MasterListModel> genericRepository, IMapper mapper) : base(genericRepository, mapper)
        {

        }
    }
}
