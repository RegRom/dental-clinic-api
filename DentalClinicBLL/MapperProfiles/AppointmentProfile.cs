using AutoMapper;
using DentalClinicBLL.Models;
using DentalClinicDAL.Entities;
using System;

namespace DentalClinicBLL.MapperProfiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.DentistId, opt => opt.MapFrom(src => src.Dentist.Id))
                .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.Patient.Id))
                .ForMember(dest => dest.ProcedureId, opt => opt.MapFrom(src => src.Procedure.Id));
            CreateMap<AppointmentDto, Appointment>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Dentist, opt => opt.Ignore())
                .ForMember(x => x.Patient, opt => opt.Ignore())
                .ForMember(x => x.Procedure, opt => opt.Ignore())
                .ForMember(x => x.Date, opt => opt.MapFrom(src => DateTime.ParseExact(src.Date, "yyyy-MM-dd HH:mm", null)));
            CreateMap<Appointment, Appointment>();
            CreateMap<AppointmentDto, AppointmentDto>();
        }
    }
}
