using AutoMapper;
using Business.DTOs;
using SpinsNew.Libraries;
using SpinsNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mappings
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
