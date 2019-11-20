using AutoMapper;
using DentalClinicBLL.Models;
using DentalClinicDAL.Entities;

namespace DentalClinicBLL.MapperProfiles
{
    public class DentistProfile : Profile
    {
        public DentistProfile()
        {
            CreateMap<Dentist, DentistDto>();
            CreateMap<DentistDto, Dentist>()
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Dentist, Dentist>();
            CreateMap<DentistDto, DentistDto>();
        }
    }
}
