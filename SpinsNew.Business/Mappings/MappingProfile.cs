using AutoMapper;
using SpinsNew.Business.DTOs;
using SpinsNew.DataAccess.Libraries;
using SpinsNew.DataAccess.Models;

namespace SpinsNew.Business.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MasterListModel, MasterListModelDto>().ReverseMap();
            CreateMap<LibrarySex, LibrarySexDto>().ReverseMap();
        }
    }
}
