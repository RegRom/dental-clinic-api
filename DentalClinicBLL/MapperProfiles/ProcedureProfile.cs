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
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Cost, opt => opt.MapFrom(src => decimal.Parse(src.Cost)));
            CreateMap<Procedure, Procedure>();
            CreateMap<ProcedureDto, ProcedureDto>();
        }
    }
}
