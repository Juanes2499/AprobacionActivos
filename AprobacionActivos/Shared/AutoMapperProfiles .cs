using AprobacionActivos.DTOs;
using AprobacionActivos.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprobacionActivos.Shared
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AprobacionPostDTO, Aprobacion>();
        }
    }
}
