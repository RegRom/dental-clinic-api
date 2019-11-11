using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DentalClinicBLL.Models;
using DentalClinicDAL.Entities;

namespace DentalClinicBLL.MapperProfiles
{
    public class ProcedureProfile : Profile
    {
        public ProcedureProfile()
        {
            CreateMap<Procedure, ProcedureDto>();
            CreateMap<ProcedureDto, Procedure>()
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Procedure, Procedure>();
            CreateMap<ProcedureDto, ProcedureDto>();
        }
    }
}
